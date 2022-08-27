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
    public partial class DomainClassIWBase : DomainClassIW
    {
        protected static readonly string className = "IW";
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;

        public static DomainClassIWBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassIWBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={newInstance.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={newInstance.Attr_successorProcessSpec_ID}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassIWBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            stateMachine = new DomainClassIWStateMachine(this, instanceRepository, logger);
        }
        protected string attr_predecessorProcessSpec_ID;
        protected bool stateof_predecessorProcessSpec_ID = false;

        protected string attr_successorProcessSpec_ID;
        protected bool stateof_successorProcessSpec_ID = false;

        protected DomainClassIWStateMachine stateMachine;
        protected bool stateof_current_state = false;

        public string Attr_predecessorProcessSpec_ID { get { return attr_predecessorProcessSpec_ID; } }
        public string Attr_successorProcessSpec_ID { get { return attr_successorProcessSpec_ID; } }
        public int Attr_current_state { get { return stateMachine.CurrentState; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassIW instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "predecessorProcessSpec_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_predecessorProcessSpec_ID)
                        {
                            result = false;
                        }
                        break;
                    case "successorProcessSpec_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_successorProcessSpec_ID)
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
        protected LinkedInstance relR5PSSuccessor;
        // private DomainClassPS relR5PSSuccessor;
        protected LinkedInstance relR5PSPredecessor;
        // private DomainClassPS relR5PSPredecessor;
        public bool LinkR5(DomainClassPS oneInstanceSuccessor, DomainClassPS otherInstancePredecessor, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR5PSSuccessor == null && relR5PSPredecessor == null)
            {
                this.attr_predecessorProcessSpec_ID = oneInstanceSuccessor.Attr_ProcessSpec_ID;
                this.attr_successorProcessSpec_ID = otherInstancePredecessor.Attr_ProcessSpec_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={this.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={this.Attr_successorProcessSpec_ID}):link[One(PS(ProcessSpec_ID={oneInstanceSuccessor.Attr_ProcessSpec_ID})),Other(PS(ProcessSpec_ID={otherInstancePredecessor.Attr_ProcessSpec_ID}))]");

                result = (LinkedR5OneSuccessor()!=null) && (LinkedR5OtherPredecessor()!=null);
                if (result)
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR5PSSuccessor });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR5PSPredecessor });
                    }
                }
            }
            return result;
        }
        
        public bool UnlinkR5(DomainClassPS oneInstanceSuccessor, DomainClassPS otherInstancePredecessor, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR5PSSuccessor != null && relR5PSPredecessor != null)
            {
                if ((this.Attr_predecessorProcessSpec_ID==oneInstanceSuccessor.Attr_ProcessSpec_ID && this.Attr_successorProcessSpec_ID==oneInstanceSuccessor.Attr_ProcessSpec_ID) && (this.Attr_predecessorProcessSpec_ID==otherInstancePredecessor.Attr_ProcessSpec_ID && this.Attr_successorProcessSpec_ID==otherInstancePredecessor.Attr_ProcessSpec_ID))
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR5PSSuccessor });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR5PSPredecessor });
                    }
        
                    this.attr_predecessorProcessSpec_ID = null;
                    this.attr_successorProcessSpec_ID = null;
                    relR5PSSuccessor = null;
                    relR5PSPredecessor = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={this.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={this.Attr_successorProcessSpec_ID}):unlink[PS(ProcessSpec_ID={oneInstanceSuccessor.Attr_ProcessSpec_ID})]");

                    result = true;
                }
            }
            return result;
        }
        
        public DomainClassPS LinkedR5OneSuccessor()
        {
            if (relR5PSSuccessor == null)
            {
                var candidates = instanceRepository.GetDomainInstances("PS").Where(inst=>(this.Attr_predecessorProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID && this.Attr_successorProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID));
                relR5PSSuccessor = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R5", Phrase = "Successor" };
                // (DomainClassPS)candidates.FirstOrDefault();
            }
            return relR5PSSuccessor.GetDestination<DomainClassPS>();
        }
        
        public DomainClassPS LinkedR5OtherPredecessor()
        {
            if (relR5PSPredecessor == null)
            {
                var candidates = instanceRepository.GetDomainInstances("PS").Where(inst=>(this.Attr_predecessorProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID && this.Attr_successorProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID));
                relR5PSPredecessor = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R5", Phrase = "Predecessor" };
                // (DomainClassPS)candidates.FirstOrDefault();
            }
            return relR5PSPredecessor.GetDestination<DomainClassPS>();
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
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={this.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={this.Attr_successorProcessSpec_ID}):takeEvent({domainEvent.EventNumber})");
        }

        
        public bool Validate()
        {
            bool isValid = true;
            if (relR5PSSuccessor == null)
            {
                isValid = false;
            }
            if (relR5PSPredecessor == null)
            {
                isValid = false;
            }
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:IW(predecessorProcessSpec_ID={this.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={this.Attr_successorProcessSpec_ID}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            attr_predecessorProcessSpec_ID = (string)propertyValues["predecessorProcessSpec_ID"];
            stateof_predecessorProcessSpec_ID = false;
            attr_successorProcessSpec_ID = (string)propertyValues["successorProcessSpec_ID"];
            stateof_successorProcessSpec_ID = false;
            stateMachine.ForceUpdateState((int)propertyValues["current_state"]);
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_predecessorProcessSpec_ID)
            {
                results.Add("predecessorProcessSpec_ID", attr_predecessorProcessSpec_ID);
                stateof_predecessorProcessSpec_ID = false;
            }
            if (stateof_successorProcessSpec_ID)
            {
                results.Add("successorProcessSpec_ID", attr_successorProcessSpec_ID);
                stateof_successorProcessSpec_ID = false;
            }
            results.Add("current_state", stateMachine.CurrentState);


            return results;
        }

        public string GetIdentities()
        {
            string identities = $"predecessorProcessSpec_ID={this.Attr_predecessorProcessSpec_ID},successorProcessSpec_ID={this.Attr_successorProcessSpec_ID}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            if (!onlyIdentity) results.Add("predecessorProcessSpec_ID", attr_predecessorProcessSpec_ID);
            if (!onlyIdentity) results.Add("successorProcessSpec_ID", attr_successorProcessSpec_ID);
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
