using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities.News;
using System;

namespace Project.Persistence.Configuration
{
    public class SettingSeedingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasData(new Setting
            {
                Id = 1,
                Key = "SiteName",
                Value = "فرشوگاه آنلاین",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });

            builder.HasData(new Setting
            {
                Id = 2,
                Key = "MetaDescription",
                Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 3,
                Key = "MetaKeyWord",
                Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 4,
                Key = "ShortcutIcon",
                Value = "/assets/images/icons/favicon.ico",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 5,
                Key = "HeaderIcon",
                Value = "/assets/images/demos/demo-3/logo.png",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 6,
                Key = "FooterIcon",
                Value = "/assets/images/demos/demo-3/logo.png",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 7,
                Key = "TopIcon",
                Value = "/images/58fb25ef5ef526b9c362c81a839dd931eafb6a46_1707298697.gif",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 25,
                Key = "TopIconLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 8,
                Key = "TopIconBool",
                Value = "on",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });

            builder.HasData(new Setting
            {
                Id = 9,
                Key = "BigBanner",
                Value = "/images/1690875299-esv04wsL6tPNeV3u.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 26,
                Key = "BigBannerLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 10,
                Key = "FirstBanner",
                Value = "/images/20cfa2fef7747b0fde8ba6875a0788325f5d081d_1706101976.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 27,
                Key = "FirstBannerLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 11,
                Key = "SecondBanner",
                Value = "/images/b596a2fd4ae4dd03bfb424ca83bd703af01ca85c_1706351348.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 28,
                Key = "SecondBannerLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 12,
                Key = "ThirdBanner",
                Value = "/images/61ed3d9edfbf5ca49898b5a3f7b32293a5c2c26f_1683011290.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 29,
                Key = "ThirdBannerLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 13,
                Key = "AmazingOffersBanner",
                Value = "/images/1.jpg",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 14,
                Key = "Step2Banner1",
                Value = "/Images/0007575b0b65e5d07cf2abb9e9501880690c664e_1674463984.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 30,
                Key = "Step2Banner1Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 15,
                Key = "Step2Banner2",
                Value = "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 31,
                Key = "Step2Banner2Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 16,
                Key = "Step3Banner1",
                Value = "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 32,
                Key = "Step3Banner1Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 55,
                Key = "Step3Banner2",
                Value = "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 33,
                Key = "Step3Banner2Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 17,
                Key = "Step3Banner3",
                Value = "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 344,
                Key = "Step3Banner3Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 18,
                Key = "Step3Banner4",
                Value = "/Images/6140b398a0438d11623c340df8654dc3e6b1bc41_1674462746.webp",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 34,
                Key = "Step3Banner4Link",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 19,
                Key = "Step4Banner",
                Value = "/Images/media-643e9c17da34f.jpg",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 35,
                Key = "Step4BannerLink",
                Value = "#",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 20,
                Key = "Telegram",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 21,
                Key = "Instagram",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 22,
                Key = "Whatsapp",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 23,
                Key = "Email",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 244,
                Key = "Telphone",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 24,
                Key = "PhoneNumber",
                Value = "",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 36,
                Key = "FooterText",
                Value = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم.",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 333,
                Key = "SendPrice",
                Value = "20000",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
            builder.HasData(new Setting
            {
                Id = 37,
                Key = "Adress",
                Value = "مازندران-ساری",
                CreatedBy = "SYSTEM",
                UpdatedBy = "SYSTEM",
                CreatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2022, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
        }
    }
}
