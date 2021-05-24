using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Model
{
    public class StatisticsResponse
    {
        public StatisticsResponse()
        {

        }
        public StatisticsResponse(int _totalNumber, decimal? _price, string _dateTime, string _mcc)
        {
            TotalNumber = TotalNumber;
            price = _price;
            dateTime = _dateTime;
            mcc = _mcc;
        }
        public int TotalNumber { get; set; }
        public decimal? price { get; set; }
        public string dateTime { get; set; }
        public string mcc { get; set; }
    }
}
