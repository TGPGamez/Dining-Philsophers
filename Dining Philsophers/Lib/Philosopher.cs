using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Philsophers.Lib
{
    public delegate void PhilosopherEvent(Philosopher philosopher);

    public class Philosopher
    {
        public PhilosopherEvent PhilosopherStateChanged { get; set; }
        public PhilosopherEvent PhilosopherFinishedEating { get; set; }

        public Fork LeftFork { get; private set; }
        public Fork RightFork { get; private set; }
        private State state;

        public State State
        {
            get { return state; }
            set 
            {
                PhilosopherStateChanged.Invoke(this);
                state = value;
            }
        }
        public string Name { get; set; }

        public void TakeFork()
        {
            while (true) 
            {
                //Tries to enter RightFork
                if (Monitor.TryEnter(RightFork))
                {
                    //Tries to enter LeftFork
                    if (Monitor.TryEnter(LeftFork))
                    {
                        //Philosopher starts eating
                        BeginEating();

                        //Notify other threads that lock state of RightFork and LeftFork is changing 
                        Monitor.Pulse(RightFork);
                        Monitor.Pulse(LeftFork);

                        //Releases the lock for RightFork and LeftFork
                        Monitor.Exit(RightFork);
                        Monitor.Exit(LeftFork);

                        State = State.Thinking;
                    }
                    else
                    {
                        Monitor.Exit(RightFork);
                    }
                }
                if (State == State.Thinking)
                {
                    Thread.Sleep(new Random().Next(1000, 4000));
                    State = State.Waiting;
                }
            }
        }

        public Philosopher(string name, Fork leftFork, Fork rightFork)
        {
            Name = name;
            LeftFork = leftFork;
            RightFork = rightFork;
            state = State.Waiting;
        }

        private void BeginEating()
        {
            State = State.Eating;
            Thread.Sleep(new Random().Next(1500, 3000));
            PhilosopherFinishedEating.Invoke(this);
        }
    }
}
