using System;
using System.Text;
using EMS.Contracts.Workers;
using EMS.Worker;

namespace EMS.Worker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new Worker();

            worker.JobAssigned += (s, e) =>
            {
//                var job = worker.GetJob() as ReadJob<T>;
            };

            System.Console.ReadLine();
        }
    }
}