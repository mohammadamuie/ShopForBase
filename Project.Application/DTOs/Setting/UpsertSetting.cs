using Microsoft.AspNetCore.Http;

namespace Project.Application.DTOs.Setting
{
    public class UpsertSetting
    {
        public string SiteName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWord { get; set; }
        public IFormFile ShortcutIcon { get; set; }
        public IFormFile HeaderIcon { get; set; }
        public IFormFile FooterIcon { get; set; }
        public IFormFile TopIcon { get; set; }
        public string TopIconLink { get; set; }
        public string TopIconBool { get; set; }
        public IFormFile BigBanner { get; set; }
        public string BigBannerLink { get; set; }
        public IFormFile FirstBanner { get; set; }
        public string FirstBannerLink { get; set; }
        public IFormFile SecondBanner { get; set; }
        public string SecondBannerLink { get; set; }
        public IFormFile ThirdBanner { get; set; }
        public string ThirdBannerLink { get; set; }
        public IFormFile AmazingOffersBanner { get; set; }
        public IFormFile Step2Banner1 { get; set; }
        public string Step2Banner1Link { get; set; }
        public IFormFile Step2Banner2 { get; set; }
        public string Step2Banner2Link { get; set; }
        public IFormFile Step3Banner1 { get; set; }
        public string Step3Banner1Link { get; set; }
        public IFormFile Step3Banner2 { get; set; }
        public string Step3Banner2Link { get; set; }
        public IFormFile Step3Banner3 { get; set; }
        public string Step3Banner3Link { get; set; }
        public IFormFile Step3Banner4 { get; set; }
        public string Step3Banner4Link { get; set; }
        public IFormFile Step4Banner { get; set; }
        public string Step4BannerLink { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public string Telphone { get; set; }
        public string PhoneNumber { get; set; }
        public string FooterText { get; set; }
    }
}
