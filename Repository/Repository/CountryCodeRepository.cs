using Interface.Repository;
using Model.Context;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CountryCodeRepository : RepositoryBase<CountryCode>, ICountryCodeRepository
    {
        public CountryCodeRepository(MittoContext context) : base(context)
        {

        }
    }
}
