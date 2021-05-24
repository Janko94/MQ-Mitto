using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SMS
    {
        public int AUID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public int? CC_From { get; set; }
        public int? CC_To { get; set; }
        public bool State { get; set; }
        public DateTime dateTime { get; set; }
    }
}
