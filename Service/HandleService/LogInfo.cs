using Interface.Service;
using Microsoft.Extensions.Logging;
using Model;
using Serilog;
using System;

namespace Service.HandleService
{
    public class LogInfo : IMessagingServiceProvider<SMS>
    {
        public void Save(SMS sms)
        {
            try
            {
                if (sms != null)
                {
                    Log.Information($"Message sent from: {sms.From} to: {sms.To} with text: {sms.Text}");
                }
                else
                {
                    Log.Information("Message not saved");
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
