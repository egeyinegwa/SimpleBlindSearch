using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blinds.com.Repositories
{
    public interface IRepository<T> 
    {
        IQueryable<T> Get();
    }
}
