using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Philsophers.Lib
{
    class Manager
    {
        public Fork[] Forks { get; private set; }
        public Philosopher[] Philosophers { get; private set; }

        public Manager()
        {
            Forks = new Fork[]
            {
                new Fork("F1"),
                new Fork("F2"),
                new Fork("F3"),
                new Fork("F4"),
                new Fork("F5"),
            };
            Philosophers = new Philosopher[]
            {
                new Philosopher("Tobias", Forks[0], Forks[1]),
                new Philosopher("Marcus", Forks[1], Forks[2]),
                new Philosopher("Sebastian", Forks[1], Forks[2]),
                new Philosopher("Philip", Forks[3], Forks[4]),
                new Philosopher("Patrick", Forks[4], Forks[0]),
            };
        }

        public void StartDinining()
        {
            for (int i = 0; i < Philosophers.Length; i++)
            {
                Thread thread = new Thread(Philosophers[i].TakeFork);
                thread.Start();
            }
        }
    }
}
