using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blinds.com.Models;
using Blinds.com.Data;

namespace Blinds.com.ControllersWebAPI
{
    public class BlindLookupController : ApiController
    {
        //autocomplete
        [HttpGet]
        public IQueryable SearchBlinds(string term, int total)
        {
            var blinds = BlindData.GetBlinds(50);

            var result = blinds
                .Where(c => (c.Name.Contains(term) || term == null))
                .Take(total)
                .Distinct();

            return result;
        }
    }
}
