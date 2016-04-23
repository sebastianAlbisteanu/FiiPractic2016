using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FiiPracticProject.DataStore;
using FiiPracticProject.Models;

namespace FiiPracticProject.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("addAccount")]
        public IHttpActionResult AddAccount(AccountModel account)
        {
            try
            {
                if (account == null)
                    return BadRequest();

                var allAccounts = DataStoreUtil.ReadModels() ?? new List<AccountModel>();

                allAccounts.Add(account);

                DataStoreUtil.SaveModels(allAccounts);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("addSensor")]
        public IHttpActionResult AddSensor(string account, string sensorId)
        {
            try
            {
                var allAccounts = DataStoreUtil.ReadModels();

                var currentAccount = allAccounts.FirstOrDefault(a => a.Name.Equals(account));

                if (currentAccount == null)
                    return NotFound();

                if(currentAccount.Sensors == null)
                    currentAccount.Sensors = new List<string>();
                currentAccount.Sensors.Add(sensorId);

                DataStoreUtil.SaveModels(allAccounts);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("getSettings")]
        public IHttpActionResult GetSettingsBySensorId(string sensorId)
        {
            try
            {
                var allAccounts = DataStoreUtil.ReadModels();

                var currentAccount = allAccounts.FirstOrDefault(a => a.Sensors != null && a.Sensors.Contains(sensorId));

                if (currentAccount == null)
                    return NotFound();

                return Ok(new
                {
                    Temperature = currentAccount.Temperature,
                    SleepHour = currentAccount.SleepHour,
                    SleepMinute = currentAccount.SleepMinute
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
