using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Model
{
    public class SMSResponse
    {
        public SMSResponse()
        {

        }
        public SMSResponse(int _totalNumber, SMS[] _result)
        {
            TotalNumber = _totalNumber;
            result = _result;
        }
        public int TotalNumber { get; set; }
        public SMS[] result { get; set; }
    }
}
