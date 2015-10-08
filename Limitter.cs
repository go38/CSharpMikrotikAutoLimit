using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikrotikAutoLimitter
{
    class Limitter
    {
        private static Limitter _instance;

        private Limitter()
        {

        }

        public static Limitter GetInstance(){
            return _instance ?? (_instance = new Limitter());
        }

        /// <summary>
        /// 0 - limit is off
        /// 1 - limit is on 16k
        /// 2 - limit is on 32k
        /// 3 - limit is on 64k
        /// </summary>
        public int GetLimitAction(){ 
            var r = new Random();
            return r.Next(0, 3);
        }

        /// <summary>
        /// Interval, for enabling limit
        /// </summary>
        /// 
        public int GetLimitLength(){
            var r = new Random();
            return r.Next(30000, 90000);
        }

        /// <summary>
        /// Generate command for limit
        /// </summary>
        /// <returns></returns>
        public string[] GetLimitCommand(){
            var la = this.GetLimitAction();
            string[] cmd;
            switch(la){
                case 0:     //limit is off 
                    cmd = new string[]{"/queue/tree/set", "=disabled=yes", "=.id=aaLock"};
                    break;
                case 1:     //limit is 16k
                    cmd =  new string[]{"/queue/tree/set", "=disabled=no", "=max-limit=64k", "=.id=aaLock"};
                    break;
                case 2:     //limit is 32k
                    cmd = new string[] { "/queue/tree/set", "=disabled=no", "=max-limit=32k", "=.id=aaLock"};
                    break;
                case 3:     //limit is 32k
                    cmd = new string[] { "/queue/tree/set", "=disabled=no", "=max-limit=128k", "=.id=aaLock"};
                    break;
                default:
                    throw new Exception("Nonexpected limit action: " + la + "!");
                    break;
            }
            return cmd;
        }
    }
}
