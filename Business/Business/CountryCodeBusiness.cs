using Interface.Business;
using Interface.Repository;
using Model.Model;
using System.Threading.Tasks;

namespace Business
{
    public class CountryCodeBusiness : ICountryCodeBusiness
    {
        ICountryCodeRepository _repository;
        public CountryCodeBusiness(ICountryCodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<CountryCode[]> GetAll()
        {
            return await _repository.GetAllArray();
        }
    }
}
