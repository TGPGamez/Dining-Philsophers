using System;
using Dining_Philsophers.Lib;

namespace Dining_Philsophers
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();

            //Assign events
            foreach (Philosopher philosopher in manager.Philosophers)
            {
                philosopher.PhilosopherStateChanged += OnPhilosopherStateChanged;
                philosopher.PhilosopherFinishedEating += OnPhilosopherFinishEating;
            }

            manager.StartDinining();
        }

        private static void OnPhilosopherFinishEating(Philosopher philosopher)
        {
            Console.WriteLine(philosopher.Name + " finished eating.");
        }

        private static void OnPhilosopherStateChanged(Philosopher philosopher)
        {
            switch (philosopher.State)
            {
                case State.Eating:
                    Console.WriteLine(philosopher.Name + " is eating with " + philosopher.LeftFork.Name + " and " + philosopher.RightFork.Name);
                    break;
                case State.Thinking:
                    Console.WriteLine(philosopher.Name + " is thinking...");
                    break;
                case State.Waiting:
                    Console.WriteLine(philosopher.Name + " is waiting...");
                    break;
            }
        }
    }
}