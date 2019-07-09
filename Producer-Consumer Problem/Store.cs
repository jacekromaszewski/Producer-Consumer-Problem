using System;
using System.Threading;

namespace Producer_Consumer_Problem
{
    class Store
    {
        readonly Random Random = new Random();
        private readonly object _Monitor = new object();
        private int _Capacity = 1000;
        private int _Current = 0;
        private readonly int _ProducerValue;
        public Store(int ProducerValue)
        {
            _ProducerValue = ProducerValue;
        }
        public Store(int ProducerValue,int Capacity, int Current)
        {
            _ProducerValue = ProducerValue;
            _Capacity = Capacity;
            _Current = Current;

        }
        public void StoreUsage(int i)
        {
            if (i > 0)
                if ((i + _Current) > _Capacity)
                    _Current = _Capacity;
                else _Current += i;
            if (i < 0)
                if ((i + _Current) < 0)
                    _Current = 0;
                else _Current += i;
        }
        public void ProducerThings()
        {
            while (true)
                lock (_Monitor)
                {
                    while (_Current == _Capacity)
                        Monitor.Wait(_Monitor);
                    StoreUsage(_ProducerValue);
                    Console.WriteLine("P store: {0}", _Current);
                    Thread.Sleep(500);
                    Monitor.PulseAll(_Monitor);
                }
        }
        public void ConsumerThings()
        {
            while (true)
                lock (_Monitor)
                {
                    while (_Current == 0)
                        Monitor.Wait(_Monitor);
                    StoreUsage(-Random.Next(_ProducerValue));
                    Console.WriteLine("C store: {0}", _Current);
                    Thread.Sleep(500);
                    Monitor.PulseAll(_Monitor);
                }
        }
    }
}
