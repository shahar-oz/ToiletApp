using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToiletApp.Models
{
    public class RateInfo
    {
        public int? Rate1 { get; set; }
        public int ToiletId { get; set; }
        public virtual CurrentToiletInfo Toilet { get; set; } = null!;
    }
}
