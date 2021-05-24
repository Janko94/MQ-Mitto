using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Business
{
    public interface ICountryCodeBusiness
    {
        Task<CountryCode[]> GetAll();
    }
}
