using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Quartz;
using System.Threading.Tasks;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using WeatherForecast.Parser.Service;

namespace WeatherForecast.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            JobScheduler jobScheduler = new JobScheduler();
            jobScheduler.Start();

            Console.ReadLine();
        }

    }
}