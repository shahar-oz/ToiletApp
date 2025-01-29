using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToiletApp.Services;

namespace ToiletApp.Models
{
    public class CurrentToiletInfo
    {
        public int ToiletId { get; set; }
        public string? Tlocation { get; set; }
        public bool? Accessibility { get; set; }
        public double? Price { get; set; }
        public int? StatusID { get; set; }
        //public RateInfo? Rate { get; set; }
        //public ReviewInfo? Review { get; set; }
        public CurrentToiletInfo()
        {
            Photos = new List<CurrentToiletPhotoInfo>();
        }
        public List<CurrentToiletPhotoInfo> Photos { get; set; }

        public string PhotoURL
        {
            get
            {
                if (Photos != null && Photos.Count > 0)
                    return Photos[0].FullURL;
                return "";
            }
        }

    }
    public class CurrentToiletPhotoInfo
    {
        public int PhotoId { get; set; }
        public string PhotoUrlPath { get; set; }
        public string FullURL
        {
            get
            {
                return ToiletAppWebAPIProxy.ImageBaseAddress + PhotoUrlPath;
            }
        }


        public CurrentToiletPhotoInfo() { }

    }
}
