// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 0.1.0
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
    public partial class DomainClassRABase : DomainClassRA
    {
        protected static readonly string className = "RA";
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;

        public static DomainClassRABase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassRABase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RA(RA_ID={newInstance.Attr_RA_ID}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassRABase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            attr_RA_ID = Guid.NewGuid().ToString();
            stateMachine = new DomainClassRAStateMachine(this, instanceRepository, logger);
        }
        protected string attr_RA_ID;
        protected bool stateof_RA_ID = false;

        protected DomainClassRAStateMachine stateMachine;
        protected bool stateof_current_state = false;

        protected string attr_Name;
        protected bool stateof_Name = false;

        public string Attr_RA_ID { get { return attr_RA_ID; } set { attr_RA_ID = value; stateof_RA_ID = true; } }
        public int Attr_current_state { get { return stateMachine.CurrentState; } }
        public string Attr_Name { get { return attr_Name; } set { attr_Name = value; stateof_Name = true; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassRA instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "RA_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_RA_ID)
                        {
                            result = false;
                        }
                        break;
                    case "Name":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Name)
                        {
                            result = false;
                        }
                        break;
                }
                if (result== false)
                {
                    break;
                }
            }
            return result;
        }

        public IEnumerable<DomainClassRES> LinkedR6()
        {
            var result = new List<DomainClassRES>();
            var candidates = instanceRepository.GetDomainInstances("RES").Where(inst=>(this.Attr_RA_ID==((DomainClassRES)inst).Attr_RA_ID));
            foreach (var c in candidates)
            {
                result.Add((DomainClassRES)c);
            }
            return result;
        }



        public void TakeEvent(EventData domainEvent, bool selfEvent=false)
        {
            if (selfEvent)
            {
                stateMachine.ReceivedSelfEvent(domainEvent).Wait();
            }
            else
            {
                stateMachine.ReceivedEvent(domainEvent).Wait();
            }
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RA(RA_ID={this.Attr_RA_ID}):takeEvent({domainEvent.EventNumber})");
        }

        
        public bool Validate()
        {
            bool isValid = true;
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RA(RA_ID={this.Attr_RA_ID}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            attr_RA_ID = (string)propertyValues["RA_ID"];
            stateof_RA_ID = false;
            stateMachine.ForceUpdateState((int)propertyValues["current_state"]);
            attr_Name = (string)propertyValues["Name"];
            stateof_Name = false;
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_RA_ID)
            {
                results.Add("RA_ID", attr_RA_ID);
                stateof_RA_ID = false;
            }
            results.Add("current_state", stateMachine.CurrentState);

            if (stateof_Name)
            {
                results.Add("Name", attr_Name);
                stateof_Name = false;
            }

            return results;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("RA_ID", attr_RA_ID);
            results.Add("current_state", stateMachine.CurrentState);
            results.Add("Name", attr_Name);

            return results;
        }

#if false
        List<ChangedState> changedStates = new List<ChangedState>();

        public IList<ChangedState> ChangedStates()
        {
            List<ChangedState> results = new List<ChangedState>();
            results.AddRange(changedStates);
            results.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Update, Target = this, ChangedProperties = ChangedProperties() });
            changedStates.Clear();

            return results;
        }
#endif
    }
}