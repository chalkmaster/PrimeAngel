using System;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Actuators;
using PrimeAngel.Actuators.PrimeBuilder;
using PrimeAngel.Perceptions;
using PrimeAngel.Sensors;
using PrimeAngel.Sensors.PrimeBuilder.Business;
using PrimeAngel.Sensors.System;

namespace PrimeAngel
{
    class Program
    {
        static void Main(string[] args)
        {
            string query;

            Console.WriteLine("Hi duddy, can I help you?");
            var processor = new CommandProcessor();

            while (processor.ByeExpressions(query = Console.ReadLine().ToLower()))
            {
                processor.Process(query);
                Console.WriteLine("Something else?");
            }
            Console.WriteLine("OK! See you!");
            Console.ReadKey();
        }
    }

    class CommandProcessor
    {
        private static string[] _exitCommands = {"exit", "no", "nope", "nops"};

        public bool ByeExpressions(string query)
        {
            return !_exitCommands.Intersect(query.Replace(",","").Replace("!","").Replace(".","").Split(' ')).Any();
        }

        public void Process(string query)
        {
            switch (query)
            {
                case "show me all":
                case "run":
                case "percept":
                    AllPerceptionsRunner();
                    break;
                case "teste":
                    var a = new[] { 1.0, 2.0, 3.0, 12.0, 2.0, 3.0, 3.0, 2.0, 2.0, 3.0, 2.0, 2.0, 8.0 };
                    var b = a.PopulationStandardDeviation();
                    var m = a.Mean();
                    Console.WriteLine(b);
                    Console.WriteLine(m);
                    a.Where(n => m + b < n || m - b > n).ToList().ForEach(Console.WriteLine);
                    break;
                default:
                    Console.WriteLine("I don't understand. Should you repleat please?");
                    break;
            }
        }

        private void AllPerceptionsRunner()
        {
            var perceptions = new List<IPerception>();

            var sensors = new List<ISensor>
            {
                new TimeSensor(),
                new InstanceUseSensor()
            };

            sensors.AsParallel().ForAll(s => perceptions.AddRange(s.Percept()));

            var actuators = new List<IActuator>
            {
                new ActiveInstanceWithoutUseActuator("console")
            };

            actuators.AsParallel().ForAll(act => act.Do(perceptions));
        }
    }
}
