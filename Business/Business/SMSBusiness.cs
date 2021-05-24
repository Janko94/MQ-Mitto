using Interface.Business;
using Interface.Repository;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class SMSBusiness : ISMSBusiness
    {
        ISMSRepository _repository;
        public SMSBusiness(ISMSRepository repository)
        {
            _repository = repository;
        }
        public async Task<object> GetSMSWithParams(DateTime datefrom, DateTime dateto, int skip, int take)
        {
            try
            {
                var arrayResult = await _repository.GetSMSWithParams(datefrom, dateto, skip, take);
                var recordsCount = arrayResult?.Count() ?? 0;
                return new SMSResponse(recordsCount, arrayResult?.ToArray());
                //return new { recordsCount, arrayResult }; xml serialize problem
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<StatisticsResponse>> GetStatistics(DateTime datefrom, DateTime dateto, IEnumerable<string> mcclist)
        {
            try
            {
                return await _repository.GetStatistics(datefrom, dateto, mcclist);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Post(SMS sms)
        {
            try
            {
                _repository.Create(sms);
                _repository.Commit();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
