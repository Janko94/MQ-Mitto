using Autofac;
using Common.Enum;
using DotNetCore.CAP;
using Interface.Business;
using Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Model;
using Serilog;
using Service.IoC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace CAP.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SMSController : ControllerBase
    {
        const string topic = "demo.topic";
        const string comma = ",";
        IConfiguration configuration = null;
        private readonly ICapPublisher capPublisher;
        ISMSBusiness business;
        public SMSController(IConfiguration configuration, ICapPublisher capPublisher, ISMSBusiness business)
        {
            this.configuration = configuration;
            this.capPublisher = capPublisher;
            this.business = business;
        }

        [HttpGet("statistics.json")]
        public async Task<IActionResult> GetStatisticsAsJson(string dateFrom, string dateTo, string mccList)
        {
            try
            {
                
                 DateTime from;
                DateTime to;
                if (DateTime.TryParse(dateFrom, CultureInfo.InvariantCulture, DateTimeStyles.None, out from)
                    && DateTime.TryParse(dateTo, CultureInfo.InvariantCulture, DateTimeStyles.None, out to))
                {
                    var model = await business.GetStatistics(from, to, mccList?.Split(comma));
                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                }
                throw new FormatException("Input was not in correct format");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpGet("statistics.xml")]
        public async Task<IActionResult> GetStatisticsAsXml(string dateFrom, string dateTo, string mccList)
        {
            try
            {

                //var model = await business.GetStatistics(new DateTime(2018, 6, 20), new DateTime(2022, 6, 20), new List<string> { "232", "260" });
                //return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(model));

                DateTime from;
                DateTime to;
                if (DateTime.TryParse(dateFrom, CultureInfo.InvariantCulture, DateTimeStyles.None, out from)
                    && DateTime.TryParse(dateTo, CultureInfo.InvariantCulture, DateTimeStyles.None, out to))
                {
                    var model = await business.GetStatistics(from, to, mccList?.Split(comma));
                    return Ok(Helper.CreateXmlResponse<List<StatisticsResponse>>(model));
                }
                throw new FormatException("Input was not in correct format");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpGet("sent.json")]
        public async Task<IActionResult> GetSMSWithParamsAsJson(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            try
            {
                DateTime dateFrom;
                DateTime dateTo;
                if (DateTime.TryParse(dateTimeFrom, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom) 
                    && DateTime.TryParseExact(dateTimeTo, configuration["DateTimeFormat"], CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo))
                {
                    var model = await business.GetSMSWithParams(dateFrom, dateTo, skip, take);
                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                }
                throw new FormatException("Date time from or to in not correct format");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }
        [HttpGet("sent.xml")]
        public async Task<IActionResult> GetSMSWithParamsAsXml(string dateTimeFrom, string dateTimeTo, int skip, int take)
        {
            try
            {
                DateTime dateFrom;
                DateTime dateTo;
                if (DateTime.TryParse(dateTimeFrom, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom)
                    && DateTime.TryParseExact(dateTimeTo, configuration["DateTimeFormat"], CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo))
                {
                    var model = await business.GetSMSWithParams(dateFrom, dateTo, skip, take);
                    return Ok(Helper.CreateXmlResponse<SMSResponse>(model));
                }
                throw new FormatException("Date time from or to is not in correct format");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }


        [HttpPost("publishtest")]
        public async Task<IActionResult> PublishTest([FromBody]SMS sms)
        {
            try
            {
                if (sms != null)
                {
                    var m = new Message<SMS>()
                    {
                        Value = sms
                    };
                    await capPublisher.PublishAsync($"{topic}", m);

                    Log.Information("SMS created and message is published.");
                    return Ok(ResultState.SUCCESS);
                }
                Log.Information("SMS not sent.");
                return Ok(ResultState.FAILED);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpGet("send.json")]
        public async Task<IActionResult> PublishTestAsJson(string to, string from, string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(text))
                {
                    var m = new Message<SMS>()
                    {
                        Value = new SMS()
                        {
                            To = to,
                            From = from,
                            Text = text,
                            dateTime = DateTime.Now
                        }
                    };
                    await capPublisher.PublishAsync($"{topic}", m);

                    Log.Information("SMS created and message is published.");
                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(ResultState.SUCCESS));
                }
                Log.Information("SMS not sent.");
                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(ResultState.FAILED));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpGet("send.xml")]
        public async Task<IActionResult> PublishTestAsXML(string to, string from, string text)
        {
            try
            {
                if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(text))
                {
                    var m = new Message<SMS>()
                    {
                        Value = new SMS()
                        {
                            To = to,
                            From = from,
                            Text = text,
                            dateTime = DateTime.Now
                        }
                    };
                    await capPublisher.PublishAsync($"{topic}", m);

                    Log.Information("SMS created and message is published.");
                    return Ok(Helper.CreateXmlResponse<ResultState>(ResultState.SUCCESS));
                }
                Log.Information("SMS not sent.");

                return Ok(Helper.CreateXmlResponse<ResultState>(ResultState.FAILED));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [CapSubscribe(topic)]
        public IActionResult ReceiveMessage(Message<SMS> sms)
        {
            try
            {
                var param = Helper.GetServiceParam(configuration["MessageService"]);
                object service;
                if (IoCContainer.AutofacBuilder.TryResolveKeyed(param, typeof(IMessagingServiceProvider<SMS>), out service))
                {
                    ((IMessagingServiceProvider<SMS>)service).Save(sms?.Value);
                    Log.Information("Message received");
                    return Ok();
                }
                return Ok("Message not processed");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex);
            }

        }
    }
}
