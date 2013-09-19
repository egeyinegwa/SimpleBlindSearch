using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.ViewModels
{
    public class BlindDetailVM
    {
        public string  Name  { get; set; }
        [JsonIgnore]
        public decimal Price { get; set; }

        public string  FormattedPrice
        {
            get { return "$" + Price.ToString("N2"); }
        }
    }
}