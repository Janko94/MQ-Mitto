using Model;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Business
{
    public interface ISMSBusiness
    {
        Task<object> GetSMSWithParams(DateTime datefrom, DateTime dateto, int skip, int take);
        Task<IEnumerable<StatisticsResponse>> GetStatistics(DateTime datefrom, DateTime dateto, IEnumerable<string> mcclist);
        void Post(SMS sms);
    }
}
