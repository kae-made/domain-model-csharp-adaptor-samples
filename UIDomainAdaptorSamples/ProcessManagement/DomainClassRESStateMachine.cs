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
    public partial class DomainClassRESStateMachine : StateMachineBase, ITransition
    {
        public enum Events
        {
            RES1 = 0,     // Freed
            RES2 = 1    // Assigned
        }

        public enum States
        {
            _NoState_ = 0,
            Free = 1,
            Busy = 2
        }

        public class RES1_Freed : EventData
        {
            DomainClassRES reciever;

            public RES1_Freed(DomainClassRES reciever) : base((int)Events.RES1)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static RES1_Freed Create(DomainClassRES receiver, bool sendNow)
            {
                var newEvent = new RES1_Freed(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent);
                    }
                }

                return newEvent;
            }
        }

        public class RES2_Assigned : EventData
        {
            DomainClassRES reciever;

            public RES2_Assigned(DomainClassRES reciever) : base((int)Events.RES2)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static RES2_Assigned Create(DomainClassRES receiver, bool sendNow)
            {
                var newEvent = new RES2_Assigned(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent);
                    }
                }

                return newEvent;
            }
        }

        protected DomainClassRES target;

        protected InstanceRepository instanceRepository;

        public DomainClassRESStateMachine(DomainClassRES target, InstanceRepository instanceRepository, Logger logger) : base(1, logger)
        {
            this.target = target;
            this.stateTransition = this;
            this.logger = logger;
            this.instanceRepository = instanceRepository;
        }

        protected int[,] stateTransitionTable = new int[2, 2]
            {
                { (int)ITransition.Transition.CantHappen, (int)States.Busy }, 
                { (int)States.Free, (int)ITransition.Transition.CantHappen }
            };

        public int GetNextState(int currentState, int eventNumber)
        {
            return stateTransitionTable[currentState, eventNumber];
        }

        private List<ChangedState> changedStates;

        protected override void RunEntryAction(int nextState, EventData eventData)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={target.Attr_Resource_ID}):entering[current={CurrentState},event={eventData.EventNumber}");


            changedStates = new List<ChangedState>();

            switch (nextState)
            {
            case (int)States.Free:
                ActionFree();
                break;
            case (int)States.Busy:
                ActionBusy();
                break;
            }
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={target.Attr_Resource_ID}):entered[current={CurrentState},event={eventData.EventNumber}");


            instanceRepository.SyncChangedStates(changedStates);
        }
    }
}
