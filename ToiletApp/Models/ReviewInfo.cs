using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToiletApp.Models
{
    public class ReviewInfo
    {
        public string? Review1 { get; set; }
        public int ToiletId { get; set; }
        public virtual CurrentToiletInfo Toilet { get; set; } = null!;
    }
}
