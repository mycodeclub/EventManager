using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using Newtonsoft.Json;

namespace EventManager.Dto
{
    public class Guest : Contact
    {
        //public string? QrCodeImageUri { get; set; } = string.Empty;
        //[NotMapped]
        //public string BarCodeUrl { get { return "/GeneratedQRCode/" + UniqueId.ToString() + ".png"; } }

        public string AllowedSubEventIdsJson { get; set; } = string.Empty;

        [NotMapped]
        public List<Guid>? AllowedSubEvents
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AllowedSubEventIdsJson))
                    return null;
                else return JsonConvert.DeserializeObject<List<Guid>>(AllowedSubEventIdsJson);
            }
        }
    }
}
