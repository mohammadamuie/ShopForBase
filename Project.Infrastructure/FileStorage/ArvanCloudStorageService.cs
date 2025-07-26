using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Exceptions;
using Project.Application.Models;

namespace Project.Infrastructure.FileStorage
{
    public class ArvanCloudStorageService : IFileStorageService
    {
        private ArvanCloudSettings _settings;
        private static IAmazonS3 _s3Client;
        private readonly IWebHostEnvironment env;
        public ArvanCloudStorageService(IOptions<ArvanCloudSettings> settings, IWebHostEnvironment env)
        {
            _settings = settings.Value;

            var awsCredentials = new BasicAWSCredentials(_settings.AccessKey, _settings.SecretKey);
            var config = new AmazonS3Config { ServiceURL = _settings.EndpointUrl };
            _s3Client = new AmazonS3Client(awsCredentials, config);
            this.env = env;
        }
        public async Task DeleteFile(string containerName, string fileRoute)
        {
            containerName = $"hishop{containerName}";

            if (await HeadBucketAsync(containerName) == false)
            {
                await CreateBucket(containerName);
            }

            await DeleteObjectHelper(containerName, GetObjectNameFromFileRoute(fileRoute));
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFile(containerName, fileRoute);
            return await SaveFile(containerName, file);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            containerName = $"hishop{containerName}";

            if (await HeadBucketAsync(containerName) == false)
            {
                await CreateBucket(containerName);
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            var stream = file.OpenReadStream();
            return await UploadObjectFromFileAsync(containerName, fileName, stream);
        }

        private string GetFileUrl(string bucketName, string objectName)
        {
            return $"{_s3Client.Config.ServiceURL.Substring(0, 8)}{bucketName}.{_s3Client.Config.ServiceURL.Substring(8)}/{objectName}";
        }

        private string GetObjectNameFromFileRoute(string fileRoute)
        {
            return fileRoute.Substring(fileRoute.LastIndexOf('/') + 1);
        }

        private async Task CreateBucket(string bucketName)
        {
            try
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                var putBucketResponse = await _s3Client.PutBucketAsync(putBucketRequest);

            }
            catch (AmazonS3Exception ex)
            {
                throw new ArvanCloudException($"Error creating bucket: '{ex.Message}'");
            }
        }

        private async Task DeleteBucket(string bucketName)
        {
            try
            {
                await _s3Client.DeleteBucketAsync(bucketName);
            }
            catch (AmazonS3Exception ex)
            {
                throw new ArvanCloudException($"Error deleting bucket: '{ex.Message}'");
            }
        }

        private async Task<bool> HeadBucketAsync(string bucketName)
        {
            return await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        }

        private async Task<ListBucketsResponse> GetBuckets()
        {
            return await _s3Client.ListBucketsAsync();
        }

        private async Task<string> UploadObjectFromFileAsync(string bucketName, string objectName, Stream stream)
        {
            try
            {
                await _s3Client.UploadObjectFromStreamAsync(bucketName, objectName, stream, null);

                await PutObjectAclAsync(bucketName, objectName, false);

                var res = GetFileUrl(bucketName, objectName);

                return res;
            }
            catch (AmazonS3Exception e)
            {
                throw new ArvanCloudException($"Error uploading file: '{e.Message}'");
            }
            catch (Exception ex)
            {
                throw new ArvanCloudException($"Error uploading file: '{ex.Message}'");
            }
        }

        private async Task PutObjectAclAsync(string bucketName, string objectName, bool isPrivate)
        {
            try
            {
                PutACLResponse response = await _s3Client.PutACLAsync(new PutACLRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                    CannedACL = isPrivate ? S3CannedACL.Private : S3CannedACL.PublicRead,
                });
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new ArvanCloudException("An AmazonS3Exception was thrown. Exception: " + amazonS3Exception.ToString());
            }
            catch (Exception e)
            {
                throw new ArvanCloudException("Exception: " + e.ToString());
            }
        }

        private async Task DeleteObjectHelper(string bucketName, string objectName)
        {
            try
            {
                DeleteObjectsResponse response = await _s3Client.DeleteObjectsAsync(new DeleteObjectsRequest
                {
                    BucketName = bucketName,
                    Objects = new List<KeyVersion> { new KeyVersion() { Key = objectName, VersionId = null } }
                });

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                throw new ArvanCloudException("An AmazonS3Exception was thrown. Exception: " + amazonS3Exception.ToString());
            }
            catch (Exception e)
            {
                throw new ArvanCloudException("Exception: " + e.ToString());
            }
        }
    }
}