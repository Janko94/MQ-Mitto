using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Message<T>
    {
        //public EventType EventType { get; set; }
        public T Value { get; set; }
    }
}
