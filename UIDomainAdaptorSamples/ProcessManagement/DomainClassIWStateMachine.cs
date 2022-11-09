// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 1.0.0
//  
//     Updates this file cause incorrect behavior 
//     and will be lost when the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Kae.StateMachine;
using Kae.Utility.Logging;
using Kae.DomainModel.Csharp.Framework;

namespace ProcessManagement
{
    public partial class DomainClassIWStateMachine : StateMachineBase, ITransition
    {
        public enum Events
        {
            IW1 = 0,     // Start
            IW2 = 1    // Done
        }

        public enum States
        {
            _NoState_ = 0,
            Ready = 1,
            Working = 2,
            Done = 3
        }

        public class IW1_Start : EventData
        {
            DomainClassIW reciever;

            public IW1_Start(DomainClassIW reciever) : base((int)Events.IW1)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static IW1_Start Create(DomainClassIW receiver, bool isSelfEvent, bool sendNow)
            {
                var newEvent = new IW1_Start(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent, isSelfEvent);
                    }
                }
                else
                {
                    if (sendNow)
                    {
                        newEvent = null;
                    }
                }

                return newEvent;
            }

            public override IDictionary<string, object> GetSupplementalData()
            {
                var supplementalData = new Dictionary<string, object>();


                return supplementalData;
            }
        }

        public class IW2_Done : EventData
        {
            DomainClassIW reciever;

            public IW2_Done(DomainClassIW reciever) : base((int)Events.IW2)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static IW2_Done Create(DomainClassIW receiver, bool isSelfEvent, bool sendNow)
            {
                var newEvent = new IW2_Done(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent, isSelfEvent);
                    }
                }
                else
                {
                    if (sendNow)
                    {
                        newEvent = null;
                    }
                }

                return newEvent;
            }

            public override IDictionary<string, object> GetSupplementalData()
            {
                var supplementalData = new Dictionary<string, object>();


                return supplementalData;
            }
        }

        protected DomainClassIW target;

        protected InstanceRepository instanceRepository;

        protected string DomainName { get { return target.DomainName; } }

        // Constructor
        public DomainClassIWStateMachine(DomainClassIW target, bool synchronousMode, InstanceRepository instanceRepository, Logger logger) : base(1, synchronousMode, logger)
        {
            this.target = target;
            this.stateTransition = this;
            this.logger = logger;
            this.instanceRepository = instanceRepository;
        }

        protected int[,] stateTransitionTable = new int[3, 2]
            {
                { (int)States.Working, (int)ITransition.Transition.CantHappen }, 
                { (int)ITransition.Transition.CantHappen, (int)States.Done }, 
                { (int)ITransition.Transition.CantHappen, (int)ITransition.Transition.CantHappen }
            };

        public int GetNextState(int currentState, int eventNumber)
        {
            return stateTransitionTable[currentState, eventNumber];
        }

        private List<ChangedState> changedStates;

        protected override void RunEntryAction(int nextState, EventData eventData)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={target.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={target.Attr_successorProcessSpec_ID}):entering[current={CurrentState},event={eventData.EventNumber}");


            changedStates = new List<ChangedState>();

            switch (nextState)
            {
            case (int)States.Ready:
                ActionReady();
                break;
            case (int)States.Working:
                ActionWorking();
                break;
            case (int)States.Done:
                ActionDone();
                break;
            }
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={target.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={target.Attr_successorProcessSpec_ID}):entered[current={CurrentState},event={eventData.EventNumber}");


            instanceRepository.SyncChangedStates(changedStates);
        }
    }
}
