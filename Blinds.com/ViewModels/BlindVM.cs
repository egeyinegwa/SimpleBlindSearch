
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.ViewModels
{
    [Serializable]
    public class BlindVM
    {        
        public string                      Category { get; set; }
        public IEnumerable<BlindDetailVM>  Blinds   { get; set; }
    }
}