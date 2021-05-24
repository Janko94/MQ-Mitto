using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Context;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class SMSRepository : RepositoryBase<SMS>, ISMSRepository
    {
        MittoContext _context;
        const string dash = "-";
        public SMSRepository(MittoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SMS>> GetSMSWithParams(DateTime datefrom, DateTime dateto, int skip, int take)
        {
            try
            {
                return await _context.Set<SMS>()
                            .Where(x => x.dateTime > datefrom && x.dateTime < dateto)
                            .OrderByDescending(x => x.dateTime)
                            .Skip(skip).Take(take).ToArrayAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<StatisticsResponse>> GetStatistics(DateTime datefrom, DateTime dateto, IEnumerable<string> mcclist)
        {
            try
            {
                var model = from s in _context.Set<SMS>()
                            join sa in _context.Set<CountryCode>() on s.CC_From equals sa.AUID
                            let dt = s.dateTime.Year.ToString() + dash + s.dateTime.Month.ToString() + dash + s.dateTime.Day.ToString()
                            where s.dateTime > datefrom && s.dateTime < dateto &&
                            (mcclist != null && mcclist.Any() ? mcclist.Contains(sa.mcc) : 1 == 1)
                            group new { s, sa } by new { dt, sa.mcc } into g
                            select new
                            {
                                date = g.Key.dt,
                                totalnumber = g.Count(),
                                price = g.Sum(x => x.sa.pricePerSMS),
                                mcc = g.Key.mcc
                            };

                return await model.Select(x => new StatisticsResponse(x.totalnumber, x.price, x.date, x.mcc)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
