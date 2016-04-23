using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiPracticProject.Models
{
    public class AccountModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        //list of sensor ids
        public List<string> Sensors { get; set; }
        //user settings:
        public int Temperature { get; set; }
        public int SleepHour { get; set; }

        public int SleepMinute { get; set; }
    }
}