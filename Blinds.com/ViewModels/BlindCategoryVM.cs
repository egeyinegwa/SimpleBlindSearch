using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.ViewModels
{
    public class BlindCategoryVM
    {
        public string                     Category { get; set; }
        public IEnumerable<BlindDetailVM> Blinds   { get; set; }
    }
}