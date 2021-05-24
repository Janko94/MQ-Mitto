using Business;
using Interface.Business;
using Interface.Service;
using Model;
using Serilog;
using System;

namespace Service.HandleService
{
    class ProcessMessage : IMessagingServiceProvider<SMS>
    {
        ISMSBusiness _business;
        public ProcessMessage(ISMSBusiness business)
        {
            _business = business;
        }
        public void Save(SMS sms)
        {
            try
            {
                _business.Post(sms);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }
    }
}
