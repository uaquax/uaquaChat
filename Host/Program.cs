using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(uaquaChat.ServiceChat)))
            {
                host.Open();

                Console.WriteLine("Host has been started...");
                Console.ReadLine();
            }
        }
    }
}
