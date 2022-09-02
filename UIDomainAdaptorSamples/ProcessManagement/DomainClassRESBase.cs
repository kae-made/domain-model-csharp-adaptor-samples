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
    public partial class DomainClassRESBase : DomainClassRES
    {
        protected static readonly string className = "RES";
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;

        public static DomainClassRESBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassRESBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={newInstance.Attr_Resource_ID}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassRESBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            attr_Resource_ID = Guid.NewGuid().ToString();
            stateMachine = new DomainClassRESStateMachine(this, instanceRepository, logger);
        }
        protected string attr_Resource_ID;
        protected bool stateof_Resource_ID = false;

        protected DomainClassRESStateMachine stateMachine;
        protected bool stateof_current_state = false;

        protected string attr_Name;
        protected bool stateof_Name = false;

        protected string attr_RA_ID;
        protected bool stateof_RA_ID = false;

        public string Attr_Resource_ID { get { return attr_Resource_ID; } set { attr_Resource_ID = value; stateof_Resource_ID = true; } }
        public int Attr_current_state { get { return stateMachine.CurrentState; } }
        public string Attr_Name { get { return attr_Name; } set { attr_Name = value; stateof_Name = true; } }
        public string Attr_RA_ID { get { return attr_RA_ID; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassRES instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "Resource_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Resource_ID)
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
                    case "RA_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_RA_ID)
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
        protected LinkedInstance relR6RA;
        public DomainClassRA LinkedR6()
        {
            if (relR6RA == null)
            {
           var candidates = instanceRepository.GetDomainInstances("RA").Where(inst=>(this.Attr_RA_ID==((DomainClassRA)inst).Attr_RA_ID));
           relR6RA = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R6", Phrase = "" };

            }
            return relR6RA.GetDestination<DomainClassRA>();
        }

        public bool LinkR6(DomainClassRA instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR6RA == null)
            {
                this.attr_RA_ID = instance.Attr_RA_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={this.Attr_Resource_ID}):link[RA(RA_ID={instance.Attr_RA_ID})]");

                result = (LinkedR6()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR6RA });
                }
            }
            return result;
        }

        public bool UnlinkR6(DomainClassRA instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR6RA != null && ( this.Attr_RA_ID==instance.Attr_RA_ID ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR6RA });
        
                this.attr_RA_ID = null;
                relR6RA = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={this.Attr_Resource_ID}):unlink[RA(RA_ID={instance.Attr_RA_ID})]");


                result = true;
            }
            return result;
        }

        public DomainClassP LinkedR1OneIsUsedBy()
        {
            var candidates = instanceRepository.GetDomainInstances("P").Where(inst=>(this.Attr_Resource_ID==((DomainClassP)inst).Attr_Resource_ID));
            return (DomainClassP)candidates.FirstOrDefault();
        }


        public IEnumerable<DomainClassREQ> LinkedR8()
        {
            var result = new List<DomainClassREQ>();
            var candidates = instanceRepository.GetDomainInstances("REQ").Where(inst=>(this.Attr_Resource_ID==((DomainClassREQ)inst).Attr_RequestingResource_ID));
            foreach (var c in candidates)
            {
                result.Add((DomainClassREQ)c);
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
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={this.Attr_Resource_ID}):takeEvent({domainEvent.EventNumber})");
        }

        
        public bool Validate()
        {
            bool isValid = true;
            if (relR6RA == null)
            {
                isValid = false;
            }
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:RES(Resource_ID={this.Attr_Resource_ID}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            attr_Resource_ID = (string)propertyValues["Resource_ID"];
            stateof_Resource_ID = false;
            stateMachine.ForceUpdateState((int)propertyValues["current_state"]);
            attr_Name = (string)propertyValues["Name"];
            stateof_Name = false;
            attr_RA_ID = (string)propertyValues["RA_ID"];
            stateof_RA_ID = false;
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_Resource_ID)
            {
                results.Add("Resource_ID", attr_Resource_ID);
                stateof_Resource_ID = false;
            }
            results.Add("current_state", stateMachine.CurrentState);

            if (stateof_Name)
            {
                results.Add("Name", attr_Name);
                stateof_Name = false;
            }
            if (stateof_RA_ID)
            {
                results.Add("RA_ID", attr_RA_ID);
                stateof_RA_ID = false;
            }

            return results;
        }

        public string GetIdentities()
        {
            string identities = $"Resource_ID={this.Attr_Resource_ID}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("Resource_ID", attr_Resource_ID);
            results.Add("current_state", stateMachine.CurrentState);
            results.Add("Name", attr_Name);
            if (!onlyIdentity) results.Add("RA_ID", attr_RA_ID);

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
