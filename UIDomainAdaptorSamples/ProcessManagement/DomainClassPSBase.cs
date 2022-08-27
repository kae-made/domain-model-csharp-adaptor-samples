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
    public partial class DomainClassPSBase : DomainClassPS
    {
        protected static readonly string className = "PS";
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;

        public static DomainClassPSBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassPSBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={newInstance.Attr_ProcessSpec_ID}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassPSBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            attr_ProcessSpec_ID = Guid.NewGuid().ToString();
            stateMachine = new DomainClassPSStateMachine(this, instanceRepository, logger);
        }
        protected string attr_ProcessSpec_ID;
        protected bool stateof_ProcessSpec_ID = false;

        protected string attr_Order_ID;
        protected bool stateof_Order_ID = false;

        protected int attr_Number;
        protected bool stateof_Number = false;

        protected string attr_Process_ID;
        protected bool stateof_Process_ID = false;

        protected bool attr_Finished;
        protected bool stateof_Finished = false;

        protected DomainClassPSStateMachine stateMachine;
        protected bool stateof_current_state = false;

        public string Attr_ProcessSpec_ID { get { return attr_ProcessSpec_ID; } set { attr_ProcessSpec_ID = value; stateof_ProcessSpec_ID = true; } }
        public string Attr_Order_ID { get { return attr_Order_ID; } }
        public int Attr_Number { get { return attr_Number; } set { attr_Number = value; stateof_Number = true; } }
        public string Attr_Process_ID { get { return attr_Process_ID; } }
        public bool Attr_Finished { get { return attr_Finished; } set { attr_Finished = value; stateof_Finished = true; } }
        public int Attr_current_state { get { return stateMachine.CurrentState; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassPS instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "ProcessSpec_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_ProcessSpec_ID)
                        {
                            result = false;
                        }
                        break;
                    case "Order_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Order_ID)
                        {
                            result = false;
                        }
                        break;
                    case "Number":
                        if ((int)conditionPropertyValues[propertyName] != instance.Attr_Number)
                        {
                            result = false;
                        }
                        break;
                    case "Process_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Process_ID)
                        {
                            result = false;
                        }
                        break;
                    case "Finished":
                        if ((bool)conditionPropertyValues[propertyName] != instance.Attr_Finished)
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
        protected LinkedInstance relR4OS;
        protected LinkedInstance relR2P;
        public DomainClassOS LinkedR4()
        {
            if (relR4OS == null)
            {
           var candidates = instanceRepository.GetDomainInstances("OS").Where(inst=>(this.Attr_Order_ID==((DomainClassOS)inst).Attr_Order_ID));
           relR4OS = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R4", Phrase = "" };

            }
            return relR4OS.GetDestination<DomainClassOS>();
        }

        public bool LinkR4(DomainClassOS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR4OS == null)
            {
                this.attr_Order_ID = instance.Attr_Order_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):link[OS(Order_ID={instance.Attr_Order_ID})]");

                result = (LinkedR4()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR4OS });
                }
            }
            return result;
        }

        public bool UnlinkR4(DomainClassOS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR4OS != null && ( this.Attr_Order_ID==instance.Attr_Order_ID ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR4OS });
        
                this.attr_Order_ID = null;
                relR4OS = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):unlink[OS(Order_ID={instance.Attr_Order_ID})]");


                result = true;
            }
            return result;
        }
        public DomainClassP LinkedR2()
        {
            if (relR2P == null)
            {
           var candidates = instanceRepository.GetDomainInstances("P").Where(inst=>(this.Attr_Process_ID==((DomainClassP)inst).Attr_Process_ID));
           relR2P = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R2", Phrase = "" };

            }
            return relR2P.GetDestination<DomainClassP>();
        }

        public bool LinkR2(DomainClassP instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR2P == null)
            {
                this.attr_Process_ID = instance.Attr_Process_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):link[P(Process_ID={instance.Attr_Process_ID})]");

                result = (LinkedR2()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR2P });
                }
            }
            return result;
        }

        public bool UnlinkR2(DomainClassP instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR2P != null && ( this.Attr_Process_ID==instance.Attr_Process_ID ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR2P });
        
                this.attr_Process_ID = null;
                relR2P = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):unlink[P(Process_ID={instance.Attr_Process_ID})]");


                result = true;
            }
            return result;
        }
        public DomainClassIW LinkedR5OtherPredecessor()
        {
            var candidates = instanceRepository.GetDomainInstances("IW").Where(inst=>(this.Attr_ProcessSpec_ID==((DomainClassIW)inst).Attr_predecessorProcessSpec_ID && this.Attr_ProcessSpec_ID==((DomainClassIW)inst).Attr_successorProcessSpec_ID));
            return (DomainClassIW)candidates.FirstOrDefault();
        }


        public DomainClassIW LinkedR5OneSuccessor()
        {
            var candidates = instanceRepository.GetDomainInstances("IW").Where(inst=>(this.Attr_ProcessSpec_ID==((DomainClassIW)inst).Attr_predecessorProcessSpec_ID && this.Attr_ProcessSpec_ID==((DomainClassIW)inst).Attr_successorProcessSpec_ID));
            return (DomainClassIW)candidates.FirstOrDefault();
        }

        public DomainClassP LinkedR3()
        {
            var candidates = instanceRepository.GetDomainInstances("P").Where(inst=>(this.Attr_ProcessSpec_ID==((DomainClassP)inst).Attr_firstProcessSpec_ID));
            return (DomainClassP)candidates.FirstOrDefault();
        }
        public DomainClassP LinkedR7()
        {
            var candidates = instanceRepository.GetDomainInstances("P").Where(inst=>(this.Attr_ProcessSpec_ID==((DomainClassP)inst).Attr_currentProcessSpec_ID));
            return (DomainClassP)candidates.FirstOrDefault();
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
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):takeEvent({domainEvent.EventNumber})");
        }

        
        public bool Validate()
        {
            bool isValid = true;
            if (relR4OS == null)
            {
                isValid = false;
            }
            if (relR2P == null)
            {
                isValid = false;
            }
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:PS(ProcessSpec_ID={this.Attr_ProcessSpec_ID}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            attr_ProcessSpec_ID = (string)propertyValues["ProcessSpec_ID"];
            stateof_ProcessSpec_ID = false;
            attr_Order_ID = (string)propertyValues["Order_ID"];
            stateof_Order_ID = false;
            attr_Number = (int)propertyValues["Number"];
            stateof_Number = false;
            attr_Process_ID = (string)propertyValues["Process_ID"];
            stateof_Process_ID = false;
            attr_Finished = (bool)propertyValues["Finished"];
            stateof_Finished = false;
            stateMachine.ForceUpdateState((int)propertyValues["current_state"]);
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_ProcessSpec_ID)
            {
                results.Add("ProcessSpec_ID", attr_ProcessSpec_ID);
                stateof_ProcessSpec_ID = false;
            }
            if (stateof_Order_ID)
            {
                results.Add("Order_ID", attr_Order_ID);
                stateof_Order_ID = false;
            }
            if (stateof_Number)
            {
                results.Add("Number", attr_Number);
                stateof_Number = false;
            }
            if (stateof_Process_ID)
            {
                results.Add("Process_ID", attr_Process_ID);
                stateof_Process_ID = false;
            }
            if (stateof_Finished)
            {
                results.Add("Finished", attr_Finished);
                stateof_Finished = false;
            }
            results.Add("current_state", stateMachine.CurrentState);


            return results;
        }

        public string GetIdentities()
        {
            string identities = $"ProcessSpec_ID={this.Attr_ProcessSpec_ID}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("ProcessSpec_ID", attr_ProcessSpec_ID);
            if (!onlyIdentity) results.Add("Order_ID", attr_Order_ID);
            results.Add("Number", attr_Number);
            if (!onlyIdentity) results.Add("Process_ID", attr_Process_ID);
            if (!onlyIdentity) results.Add("Finished", attr_Finished);
            results.Add("current_state", stateMachine.CurrentState);

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
