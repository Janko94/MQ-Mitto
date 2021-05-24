using Model;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interface.Repository
{
    public interface ISMSRepository : IRepositoryBase<SMS>
    {
        Task<IEnumerable<SMS>> GetSMSWithParams(DateTime datefrom, DateTime dateto, int skip, int take);
        Task<IEnumerable<StatisticsResponse>> GetStatistics(DateTime datefrom, DateTime dateto, IEnumerable<string> mcclist);
    }
}
