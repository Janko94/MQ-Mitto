using Common.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CAP
{
    public static class Helper
    {
        public static MessageServiceType? GetServiceParam(string param)
        {
            try
            {
                MessageServiceType result;
                if (!string.IsNullOrEmpty(param))
                {
                    Enum.TryParse(param, out result);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string CreateXmlResponse<T>(object obj)
        {
            try
            {
                if (obj != null)
                {
                    XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                    var xml = "";

                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            xsSubmit.Serialize(writer, obj);
                            xml = sww.ToString(); // Your XML
                        }
                    }
                    return xml;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       
    }
}
