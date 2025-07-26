using Newtonsoft.Json;
using System;
using DNTPersianUtils.Core;

namespace Project.Application.DTOs.Base
{
    public class BaseDTO
    {
        [JsonProperty(Order = -2)]
        public int Id { get; set; }

        [JsonProperty(Order = 100)]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(Order = 100)]
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
        [JsonProperty(Order = 100)]
        public string CreatedAtFormatted
        {
            get
            {
                return this.CreatedAt.ToPersianDateTextify(true);
            }
        }
        [JsonProperty(Order = 100)]
        public string UpdatedAtFormatted
        {
            get
            {
                return this.UpdatedAt.ToPersianDateTextify(true);
            }
        }
    }
}
