using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommandManagement.Controllers
{
    [RoutePrefix("api/command")]
    public class CommandController : ApiController
    {
        [HttpGet]
        [Route("{sensorId:string}")]
        public IHttpActionResult GetCommand(string sensorId)
        {
            try
            {
                var account = GetAccountSettings(sensorId);
                var sensorValue = GetSensorMeasurement(sensorId);

                string result = "";

                if (sensorId.ToLower().Contains("temp"))
                {
                    //temperature sensor
                    result =
                        String.Format("Account Temp: {0} vs Sensor Temp: {1}", account.Temperature, sensorValue)
                            .ToString();
                }
                else
                {
                    //switch sensor
                    SetSensorMeasurement(sensorId, sensorValue == 0 ? 1 : 0);
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private dynamic GetAccountSettings(string sensorId)
        {
            return new
            {
                Temperature = 12,
                SleepHour = 3,
                SleepMinute = 4
            };
        }

        private int GetSensorMeasurement(string sensorId)
        {
            return 12;
        }

        private void SetSensorMeasurement(string sensorId, int value)
        {
            
        }
    }
}
