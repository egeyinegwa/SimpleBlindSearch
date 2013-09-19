using Blinds.com.Data;
using Blinds.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.Repositories
{
    public class BlindRepository: IRepository<Blind>
    {
        public IQueryable<Blind> Get()
        {
            return BlindData.GetBlinds(500);
        }
    }
}