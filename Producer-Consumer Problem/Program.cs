using System;
using System.Threading;

namespace Producer_Consumer_Problem
{
    
        class Program
        {
            static void Main(string[] args)
            {
                Console.Write("Hom many producer will produce each time: ");
                int ProducerValue = Int32.Parse(Console.ReadLine());
                Console.Write("Amount of consumers: ");
                int ConsumerCount = Int32.Parse(Console.ReadLine());

                var Store = new Store(ProducerValue);

                Thread Producer = new Thread(new ThreadStart(Store.ProducerThings));
                Producer.Start();
                for (int i = 0; i < ConsumerCount; i++)
                {
                    Thread Consumer = new Thread(new ThreadStart(Store.ConsumerThings));
                    Consumer.Start();
                }

                while (true) { }
            }
        }
    }