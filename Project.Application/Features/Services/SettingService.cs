using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.News;
using Project.Application.DTOs.Setting;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using Project.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IFileStorageService _storageService;
        private readonly string container;

        public SettingService(ISettingRepository settingRepository, IFileStorageService storageService)
        {
            _settingRepository = settingRepository;
            _storageService = storageService;
            container = "setting";

        }
        public async Task<SettingDTO> GetSetting()
        {
            var data = await _settingRepository.GetAllQueryable().ToListAsync();

            var model = new SettingDTO();
            model.SiteName = data.FirstOrDefault(w => w.Key == "SiteName").Value;
            model.MetaDescription = data.FirstOrDefault(w => w.Key == "MetaDescription").Value;
            model.MetaKeyWord = data.FirstOrDefault(w => w.Key == "MetaKeyWord").Value;
            model.ShortcutIcon = data.FirstOrDefault(w => w.Key == "ShortcutIcon").Value;
            model.HeaderIcon = data.FirstOrDefault(w => w.Key == "HeaderIcon").Value;
            model.FooterIcon = data.FirstOrDefault(w => w.Key == "FooterIcon").Value;
            model.TopIcon = data.FirstOrDefault(w => w.Key == "TopIcon").Value;
            model.TopIconLink = data.FirstOrDefault(w => w.Key == "TopIconLink").Value;
            model.TopIconBool = data.FirstOrDefault(w => w.Key == "TopIconBool").Value;
            model.BigBanner = data.FirstOrDefault(w => w.Key == "BigBanner").Value;
            model.BigBannerLink = data.FirstOrDefault(w => w.Key == "BigBannerLink").Value;
            model.FirstBanner = data.FirstOrDefault(w => w.Key == "FirstBanner").Value;
            model.FirstBannerLink = data.FirstOrDefault(w => w.Key == "FirstBannerLink").Value;
            model.SecondBanner = data.FirstOrDefault(w => w.Key == "SecondBanner").Value;
            model.SecondBannerLink = data.FirstOrDefault(w => w.Key == "SecondBannerLink").Value;
            model.ThirdBanner = data.FirstOrDefault(w => w.Key == "ThirdBanner").Value;
            model.ThirdBannerLink = data.FirstOrDefault(w => w.Key == "ThirdBannerLink").Value;
            model.AmazingOffersBanner = data.FirstOrDefault(w => w.Key == "AmazingOffersBanner").Value;
            model.Step2Banner1 = data.FirstOrDefault(w => w.Key == "Step2Banner1").Value;
            model.Step2Banner1Link = data.FirstOrDefault(w => w.Key == "Step2Banner1Link").Value;
            model.Step2Banner2 = data.FirstOrDefault(w => w.Key == "Step2Banner2").Value;
            model.Step2Banner2Link = data.FirstOrDefault(w => w.Key == "Step2Banner2Link").Value;
            model.Step3Banner1 = data.FirstOrDefault(w => w.Key == "Step3Banner1").Value;
            model.Step3Banner1Link = data.FirstOrDefault(w => w.Key == "Step3Banner1Link").Value;
            model.Step3Banner2 = data.FirstOrDefault(w => w.Key == "Step3Banner2").Value;
            model.Step3Banner2Link = data.FirstOrDefault(w => w.Key == "Step3Banner2Link").Value;
            model.Step3Banner3 = data.FirstOrDefault(w => w.Key == "Step3Banner3").Value;
            model.Step3Banner3Link = data.FirstOrDefault(w => w.Key == "Step3Banner3Link").Value;
            model.Step3Banner4 = data.FirstOrDefault(w => w.Key == "Step3Banner4").Value;
            model.Step3Banner4Link = data.FirstOrDefault(w => w.Key == "Step3Banner4Link").Value;
            model.Step4Banner = data.FirstOrDefault(w => w.Key == "Step4Banner").Value;
            model.Step4BannerLink = data.FirstOrDefault(w => w.Key == "Step4BannerLink").Value;
            model.Telegram = data.FirstOrDefault(w => w.Key == "Telegram").Value;
            model.Instagram = data.FirstOrDefault(w => w.Key == "Instagram").Value;
            model.Whatsapp = data.FirstOrDefault(w => w.Key == "Whatsapp").Value;
            model.Email = data.FirstOrDefault(w => w.Key == "Email").Value;
            model.Telphone = data.FirstOrDefault(w => w.Key == "Telphone").Value;
            model.PhoneNumber = data.FirstOrDefault(w => w.Key == "PhoneNumber").Value;
            model.FooterText = data.FirstOrDefault(w => w.Key == "FooterText").Value;

            return model;
        }
        public async Task<double> GetSendPrice()
        {
            var data = await _settingRepository.GetAllQueryable().ToListAsync();

            return Convert.ToDouble(data.FirstOrDefault(w => w.Key == "SendPrice").Value);

        }

        public async Task UpdateSettings(UpsertSetting input)
        {
            var getall = _settingRepository.GetAllQueryable().ToList();
            var update = getall.FirstOrDefault(i => i.Key == "SiteName");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "SiteName",
                    Value = input.SiteName
                });
            }
            else
            {
                update.Value = input.SiteName;
               await _settingRepository.Update(update);
            }
            update = getall.FirstOrDefault(i => i.Key == "MetaDescription");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "MetaDescription",
                    Value = input.MetaDescription
                });
            }
            else
            {
                update.Value = input.MetaDescription;
               await _settingRepository.Update(update);
            }
            update = getall.FirstOrDefault(i => i.Key == "MetaKeyWord");
            if (update == null)
            {
                await _settingRepository.Add(new Setting() { Key = "MetaKeyWord", Value = input.MetaKeyWord });
            }
            else
            {
                update.Value = input.MetaKeyWord;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "ShortcutIcon");
            if (update == null)
            {
                var ShortcutIcon = "";
                if (input.ShortcutIcon != null && input.ShortcutIcon.Length > 0)
                {
                    ShortcutIcon = await _storageService.SaveFile(container, input.ShortcutIcon);
                }
                await _settingRepository.Add(new Setting() { Key = "ShortcutIcon", Value = ShortcutIcon });
            }
            else
            {
                if (input.ShortcutIcon != null && input.ShortcutIcon.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.ShortcutIcon);
                }
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "HeaderIcon");
            if (update == null)
            {
                var HeaderIcon = "";
                if (input.HeaderIcon != null && input.HeaderIcon.Length > 0)
                {
                    HeaderIcon = await _storageService.SaveFile(container, input.HeaderIcon);
                }
                await _settingRepository.Add(new Setting() { Key = "HeaderIcon", Value = HeaderIcon });
            }
            else
            {
                if (input.HeaderIcon != null && input.HeaderIcon.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.HeaderIcon);
                }
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "FooterIcon");
            if (update == null)
            {
                var FooterIcon = "";
                if (input.FooterIcon != null && input.FooterIcon.Length > 0)
                {
                    FooterIcon = await _storageService.SaveFile(container, input.FooterIcon);
                }
                await _settingRepository.Add(new Setting() { Key = "FooterIcon", Value = FooterIcon });
            }
            else
            {
                if (input.FooterIcon != null && input.FooterIcon.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.FooterIcon);
                }
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "TopIcon");
            if (update == null)
            {
                var TopIcon = "";
                if (input.TopIcon != null && input.TopIcon.Length > 0)
                {
                    TopIcon = await _storageService.SaveFile(container, input.TopIcon);
                }
                await _settingRepository.Add(new Setting() { Key = "TopIcon", Value = TopIcon });
            }
            else
            {
                if (input.TopIcon != null && input.TopIcon.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.TopIcon);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "TopIconLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "TopIconLink",
                    Value = input.TopIconLink
                });
            }
            else
            {
                update.Value = input.TopIconLink;
                await _settingRepository.Update(update);
            }
            update = getall.FirstOrDefault(i => i.Key == "TopIconBool");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "TopIconBool",
                    Value = input.TopIconBool
                });
            }
            else
            {
                update.Value = input.TopIconBool;
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "BigBanner");
            if (update == null)
            {
                var BigBanner = "";
                if (input.BigBanner != null && input.BigBanner.Length > 0)
                {
                    BigBanner = await _storageService.SaveFile(container, input.BigBanner);
                }
                await _settingRepository.Add(new Setting() { Key = "BigBanner", Value = BigBanner });
            }
            else
            {
                if (input.BigBanner != null && input.BigBanner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.BigBanner);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "BigBannerLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "BigBannerLink",
                    Value = input.BigBannerLink
                });
            }
            else
            {
                update.Value = input.BigBannerLink;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "FirstBanner");
            if (update == null)
            {
                var FirstBanner = "";
                if (input.FirstBanner != null && input.FirstBanner.Length > 0)
                {
                    FirstBanner = await _storageService.SaveFile(container, input.FirstBanner);
                }
                await _settingRepository.Add(new Setting() { Key = "FirstBanner", Value = FirstBanner });
            }
            else
            {
                if (input.FirstBanner != null && input.FirstBanner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.FirstBanner);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "FirstBannerLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "FirstBannerLink",
                    Value = input.FirstBannerLink
                });
            }
            else
            {
                update.Value = input.FirstBannerLink;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "SecondBanner");
            if (update == null)
            {
                var SecondBanner = "";
                if (input.SecondBanner != null && input.SecondBanner.Length > 0)
                {
                    SecondBanner = await _storageService.SaveFile(container, input.SecondBanner);
                }
                await _settingRepository.Add(new Setting() { Key = "SecondBanner", Value = SecondBanner });
            }
            else
            {
                if (input.SecondBanner != null && input.SecondBanner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.SecondBanner);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "SecondBannerLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "SecondBannerLink",
                    Value = input.SecondBannerLink
                });
            }
            else
            {
                update.Value = input.SecondBannerLink;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "ThirdBanner");
            if (update == null)
            {
                var ThirdBanner = "";
                if (input.ThirdBanner != null && input.ThirdBanner.Length > 0)
                {
                    ThirdBanner = await _storageService.SaveFile(container, input.ThirdBanner);
                }
                await _settingRepository.Add(new Setting() { Key = "ThirdBanner", Value = ThirdBanner });
            }
            else
            {
                if (input.ThirdBanner != null && input.ThirdBanner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.ThirdBanner);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "ThirdBannerLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "ThirdBannerLink",
                    Value = input.ThirdBannerLink
                });
            }
            else
            {
                update.Value = input.ThirdBannerLink;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "AmazingOffersBanner");
            if (update == null)
            {
                var AmazingOffersBanner = "";
                if (input.AmazingOffersBanner != null && input.AmazingOffersBanner.Length > 0)
                {
                    AmazingOffersBanner = await _storageService.SaveFile(container, input.AmazingOffersBanner);
                }
                await _settingRepository.Add(new Setting() { Key = "AmazingOffersBanner", Value = AmazingOffersBanner });
            }
            else
            {
                if (input.AmazingOffersBanner != null && input.AmazingOffersBanner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.AmazingOffersBanner);
                }
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step2Banner1");
            if (update == null)
            {
                var Step2Banner1 = "";
                if (input.Step2Banner1 != null && input.Step2Banner1.Length > 0)
                {
                    Step2Banner1 = await _storageService.SaveFile(container, input.Step2Banner1);
                }
                await _settingRepository.Add(new Setting() { Key = "Step2Banner1", Value = Step2Banner1 });
            }
            else
            {
                if (input.Step2Banner1 != null && input.Step2Banner1.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step2Banner1);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step2Banner1Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step2Banner1Link",
                    Value = input.Step2Banner1Link
                });
            }
            else
            {
                update.Value = input.Step2Banner1Link;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step2Banner2");
            if (update == null)
            {
                var Step2Banner2 = "";
                if (input.Step2Banner2 != null && input.Step2Banner2.Length > 0)
                {
                    Step2Banner2 = await _storageService.SaveFile(container, input.Step2Banner2);
                }
                await _settingRepository.Add(new Setting() { Key = "Step2Banner2", Value = Step2Banner2 });
            }
            else
            {
                if (input.Step2Banner2 != null && input.Step2Banner2.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step2Banner2);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step2Banner2Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step2Banner2Link",
                    Value = input.Step2Banner2Link
                });
            }
            else
            {
                update.Value = input.Step2Banner2Link;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step3Banner1");
            if (update == null)
            {
                var Step3Banner1 = "";
                if (input.Step3Banner1 != null && input.Step3Banner1.Length > 0)
                {
                    Step3Banner1 = await _storageService.SaveFile(container, input.Step3Banner1);
                }
                await _settingRepository.Add(new Setting() { Key = "Step3Banner1", Value = Step3Banner1 });
            }
            else
            {
                if (input.Step3Banner1 != null && input.Step3Banner1.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step3Banner1);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step3Banner1Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step3Banner1Link",
                    Value = input.Step3Banner1Link
                });
            }
            else
            {
                update.Value = input.Step3Banner1Link;
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step3Banner2");
            if (update == null)
            {
                var Step3Banner2 = "";
                if (input.Step3Banner2 != null && input.Step3Banner2.Length > 0)
                {
                    Step3Banner2 = await _storageService.SaveFile(container, input.Step3Banner2);
                }
                await _settingRepository.Add(new Setting() { Key = "Step3Banner2", Value = Step3Banner2 });
            }
            else
            {
                if (input.Step3Banner2 != null && input.Step3Banner2.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step3Banner2);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step3Banner2Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step3Banner2Link",
                    Value = input.Step3Banner2Link
                });
            }
            else
            {
                update.Value = input.Step3Banner2Link;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step3Banner3");
            if (update == null)
            {
                var Step3Banner3 = "";
                if (input.Step3Banner3 != null && input.Step3Banner3.Length > 0)
                {
                    Step3Banner3 = await _storageService.SaveFile(container, input.Step3Banner3);
                }
                await _settingRepository.Add(new Setting() { Key = "Step3Banner3", Value = Step3Banner3 });
            }
            else
            {
                if (input.Step3Banner3 != null && input.Step3Banner3.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step3Banner3);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step3Banner3Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step3Banner3Link",
                    Value = input.Step3Banner3Link
                });
            }
            else
            {
                update.Value = input.Step3Banner3Link;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step3Banner4");
            if (update == null)
            {
                var Step3Banner4 = "";
                if (input.Step3Banner4 != null && input.Step3Banner4.Length > 0)
                {
                    Step3Banner4 = await _storageService.SaveFile(container, input.Step3Banner4);
                }
                await _settingRepository.Add(new Setting() { Key = "Step3Banner4", Value = Step3Banner4 });
            }
            else
            {
                if (input.Step3Banner4 != null && input.Step3Banner4.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step3Banner4);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step3Banner4Link");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step3Banner4Link",
                    Value = input.Step3Banner4Link
                });
            }
            else
            {
                update.Value = input.Step3Banner4Link;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Step4Banner");
            if (update == null)
            {
                var Step4Banner = "";
                if (input.Step4Banner != null && input.Step4Banner.Length > 0)
                {
                    Step4Banner = await _storageService.SaveFile(container, input.Step4Banner);
                }
                await _settingRepository.Add(new Setting() { Key = "Step4Banner", Value = Step4Banner });
            }
            else
            {
                if (input.Step4Banner != null && input.Step4Banner.Length > 0)
                {
                    update.Value = await _storageService.SaveFile(container, input.Step4Banner);
                }
                await _settingRepository.Update(update);
            }


            update = getall.FirstOrDefault(i => i.Key == "Step4BannerLink");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Step4BannerLink",
                    Value = input.Step4BannerLink
                });
            }
            else
            {
                update.Value = input.Step4BannerLink;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Telegram");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Telegram",
                    Value = input.Telegram
                });
            }
            else
            {
                update.Value = input.Telegram;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Instagram");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Instagram",
                    Value = input.Instagram
                });
            }
            else
            {
                update.Value = input.Instagram;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Whatsapp");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Whatsapp",
                    Value = input.Whatsapp
                });
            }
            else
            {
                update.Value = input.Whatsapp;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Email");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Email",
                    Value = input.Email
                });
            }
            else
            {
                update.Value = input.Email;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "Telphone");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "Telphone",
                    Value = input.Telphone
                });
            }
            else
            {
                update.Value = input.Telphone;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "PhoneNumber");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "PhoneNumber",
                    Value = input.PhoneNumber
                });
            }
            else
            {
                update.Value = input.PhoneNumber;
                await _settingRepository.Update(update);
            }

            update = getall.FirstOrDefault(i => i.Key == "FooterText");
            if (update == null)
            {
                await _settingRepository.Add(new Setting()
                {
                    Key = "FooterText",
                    Value = input.FooterText
                });
            }
            else
            {
                update.Value = input.FooterText;
                await _settingRepository.Update(update);
            }


        }

    }
}
