using System;
using System.Threading;

namespace MikrotikAutoLimitter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rnd = new Random();
            while (true)
            {
                
                
                MakeLimit();

                var sleepTime = rnd.Next(60000, 300000); //от 1 минуты до 5 минут
                Console.WriteLine("Sleeping: " + sleepTime);
                Thread.Sleep(sleepTime);
            }
        }

        private static void MakeLimit()
        {
            var lim = Limitter.GetInstance();
            var limCmd = lim.GetLimitCommand();
            var limLength = lim.GetLimitLength();

            Console.WriteLine("Enabling limit...");
            ExcecLimitCommand(limCmd);

            Console.WriteLine("Waiting, milisec: " + limLength);
            Thread.Sleep(limLength);

            Console.WriteLine("Disabling limit...");
            ExcecLimitCommand(new string[] { "/queue/tree/set", "=disabled=yes", "=max-limit=256k", "=.id=aaLock" });




            //var cmdEnabled = "no";
            //if (enabled)
            //{
            //    cmdEnabled = "yes";
            //    Console.WriteLine("Disabling limit");
            //}
            // else
            //{
            //    Console.WriteLine("Enabling limit");
            //    
            //}

           
            // /queue tree set aaLock disabled=yes
            //mk.Send("/queue/tree/set");
            //mk.Send("=disabled=" + cmdEnabled);
            //mk.Send("=.id=aaLock", true);

            
        }

        private static void ExcecLimitCommand(string[] limCmd){
            var mk = new MK("XXX.XXX.XXX.XXX");
            if (!mk.Login("admin", "password"))
            {
                Console.WriteLine("Cannot login");
            }   
            
            string cmdBuf = "";
            foreach (var lc in limCmd){
                mk.Send(lc);
                cmdBuf += lc;
            } 
            mk.Send("", true);

            Console.WriteLine("Execuded command: " + cmdBuf + ", result: ");

            foreach (string h in mk.Read())
            {
                Console.WriteLine(h);
            }
        }
    }
}