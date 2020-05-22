using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Repository
{
    public class CountriesRepository : RepositoryBase<Country>
    {
        public CountriesRepository(SMSContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
