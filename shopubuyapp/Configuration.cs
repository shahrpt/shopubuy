using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopubuyapp
{
    public class Configuration
    {
        public int Delay 
        { 
            get; set; 
        }
        public string X_ECG_UDID
        {
            get; set;
        }

        public  string Authorization
        {
            get; set;
        }

        public string AccountId
        {
            get; set;
        }

        public string Token
        {
            get; set;
        }

        public string MachineId
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string SessionId
        {
            get; set;
        }
    }
}
