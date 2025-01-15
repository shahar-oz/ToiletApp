using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToiletApp.Models
{
    public class CurrentToiletInfo
    {
        public int ToiletId { get; set; }
        public string? Tlocation { get; set; }
        public bool? Accessibility { get; set; }
        public double? Price { get; set; }
        //public RateInfo? Rate { get; set; }
        //public ReviewInfo? Review { get; set; }
        public CurrentToiletInfo()
        {
            Photos = new List<CurrentToiletPhotoInfo>();
        }
        public List<CurrentToiletPhotoInfo> Photos { get; set; }

    }
    public class CurrentToiletPhotoInfo
    {
        public int PhotoId { get; set; }
        public string PhotoUrlPath { get; set; }

        public CurrentToiletPhotoInfo() { }

    }
}
