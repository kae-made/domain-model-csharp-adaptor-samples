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
    public partial class DomainClassPBase : DomainClassP
    {
        protected static readonly string className = "P";
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;

        public static DomainClassPBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassPBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={newInstance.Attr_Process_ID}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassPBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            stateMachine = new DomainClassPStateMachine(this, instanceRepository, logger);
            attr_Process_ID = Guid.NewGuid().ToString();
        }
        protected DomainClassPStateMachine stateMachine;
        protected bool stateof_current_state = false;

        protected string attr_Requester_ID;
        protected bool stateof_Requester_ID = false;

        protected string attr_Resource_ID;
        protected bool stateof_Resource_ID = false;

        protected string attr_Process_ID;
        protected bool stateof_Process_ID = false;

        protected string attr_firstProcessSpec_ID;
        protected bool stateof_firstProcessSpec_ID = false;

        protected string attr_currentProcessSpec_ID;
        protected bool stateof_currentProcessSpec_ID = false;

        public int Attr_current_state { get { return stateMachine.CurrentState; } }
        public string Attr_Requester_ID { get { return attr_Requester_ID; } }
        public string Attr_Resource_ID { get { return attr_Resource_ID; } }
        public string Attr_Process_ID { get { return attr_Process_ID; } set { attr_Process_ID = value; stateof_Process_ID = true; } }
        public string Attr_firstProcessSpec_ID { get { return attr_firstProcessSpec_ID; } }
        public string Attr_currentProcessSpec_ID { get { return attr_currentProcessSpec_ID; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassP instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "Requester_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Requester_ID)
                        {
                            result = false;
                        }
                        break;
                    case "Resource_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_Resource_ID)
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
                    case "firstProcessSpec_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_firstProcessSpec_ID)
                        {
                            result = false;
                        }
                        break;
                    case "currentProcessSpec_ID":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_currentProcessSpec_ID)
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
        protected LinkedInstance relR1REQIsUsedBy;
        // private DomainClassREQ relR1REQIsUsedBy;
        protected LinkedInstance relR1RESIsUserOf;
        // private DomainClassRES relR1RESIsUserOf;
        protected LinkedInstance relR3PSFirstStep;
        protected LinkedInstance relR7PSCurrentStep;
        public bool LinkR1(DomainClassREQ oneInstanceIsUsedBy, DomainClassRES otherInstanceIsUserOf, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR1REQIsUsedBy == null && relR1RESIsUserOf == null)
            {
                this.attr_Requester_ID = oneInstanceIsUsedBy.Attr_Requester_ID;
                this.attr_Resource_ID = otherInstanceIsUserOf.Attr_Resource_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):link[One(REQ(Requester_ID={oneInstanceIsUsedBy.Attr_Requester_ID})),Other(RES(Resource_ID={otherInstanceIsUserOf.Attr_Resource_ID}))]");

                result = (LinkedR1OneIsUsedBy()!=null) && (LinkedR1OtherIsUserOf()!=null);
                if (result)
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR1REQIsUsedBy });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR1RESIsUserOf });
                    }
                }
            }
            return result;
        }
        
        public bool UnlinkR1(DomainClassREQ oneInstanceIsUsedBy, DomainClassRES otherInstanceIsUserOf, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR1REQIsUsedBy != null && relR1RESIsUserOf != null)
            {
                if ((this.Attr_Requester_ID==oneInstanceIsUsedBy.Attr_Requester_ID) && (this.Attr_Resource_ID==otherInstanceIsUserOf.Attr_Resource_ID))
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR1REQIsUsedBy });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR1RESIsUserOf });
                    }
        
                    this.attr_Requester_ID = null;
                    this.attr_Resource_ID = null;
                    relR1REQIsUsedBy = null;
                    relR1RESIsUserOf = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):unlink[REQ(Requester_ID={oneInstanceIsUsedBy.Attr_Requester_ID})]");

                    result = true;
                }
            }
            return result;
        }
        
        public DomainClassREQ LinkedR1OneIsUsedBy()
        {
            if (relR1REQIsUsedBy == null)
            {
                var candidates = instanceRepository.GetDomainInstances("REQ").Where(inst=>(this.Attr_Requester_ID==((DomainClassREQ)inst).Attr_Requester_ID));
                relR1REQIsUsedBy = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R1", Phrase = "IsUsedBy" };
                // (DomainClassREQ)candidates.FirstOrDefault();
            }
            return relR1REQIsUsedBy.GetDestination<DomainClassREQ>();
        }
        
        public DomainClassRES LinkedR1OtherIsUserOf()
        {
            if (relR1RESIsUserOf == null)
            {
                var candidates = instanceRepository.GetDomainInstances("RES").Where(inst=>(this.Attr_Resource_ID==((DomainClassRES)inst).Attr_Resource_ID));
                relR1RESIsUserOf = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R1", Phrase = "IsUserOf" };
                // (DomainClassRES)candidates.FirstOrDefault();
            }
            return relR1RESIsUserOf.GetDestination<DomainClassRES>();
        }

        public DomainClassPS LinkedR3FirstStep()
        {
            if (relR3PSFirstStep == null)
            {
           var candidates = instanceRepository.GetDomainInstances("PS").Where(inst=>(this.Attr_firstProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID));
           relR3PSFirstStep = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R3", Phrase = "FirstStep" };

            }
            return relR3PSFirstStep.GetDestination<DomainClassPS>();
        }

        public bool LinkR3FirstStep(DomainClassPS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR3PSFirstStep == null)
            {
                this.attr_firstProcessSpec_ID = instance.Attr_ProcessSpec_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):link[PS(ProcessSpec_ID={instance.Attr_ProcessSpec_ID})]");

                result = (LinkedR3FirstStep()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR3PSFirstStep });
                }
            }
            return result;
        }

        public bool UnlinkR3FirstStep(DomainClassPS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR3PSFirstStep != null && ( this.Attr_firstProcessSpec_ID==instance.Attr_ProcessSpec_ID ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR3PSFirstStep });
        
                this.attr_firstProcessSpec_ID = null;
                relR3PSFirstStep = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):unlink[PS(ProcessSpec_ID={instance.Attr_ProcessSpec_ID})]");


                result = true;
            }
            return result;
        }
        public DomainClassPS LinkedR7CurrentStep()
        {
            if (relR7PSCurrentStep == null)
            {
           var candidates = instanceRepository.GetDomainInstances("PS").Where(inst=>(this.Attr_currentProcessSpec_ID==((DomainClassPS)inst).Attr_ProcessSpec_ID));
           relR7PSCurrentStep = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R7", Phrase = "CurrentStep" };

            }
            return relR7PSCurrentStep.GetDestination<DomainClassPS>();
        }

        public bool LinkR7CurrentStep(DomainClassPS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR7PSCurrentStep == null)
            {
                this.attr_currentProcessSpec_ID = instance.Attr_ProcessSpec_ID;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):link[PS(ProcessSpec_ID={instance.Attr_ProcessSpec_ID})]");

                result = (LinkedR7CurrentStep()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR7PSCurrentStep });
                }
            }
            return result;
        }

        public bool UnlinkR7CurrentStep(DomainClassPS instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR7PSCurrentStep != null && ( this.Attr_currentProcessSpec_ID==instance.Attr_ProcessSpec_ID ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR7PSCurrentStep });
        
                this.attr_currentProcessSpec_ID = null;
                relR7PSCurrentStep = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):unlink[PS(ProcessSpec_ID={instance.Attr_ProcessSpec_ID})]");


                result = true;
            }
            return result;
        }

        public IEnumerable<DomainClassPS> LinkedR2()
        {
            var result = new List<DomainClassPS>();
            var candidates = instanceRepository.GetDomainInstances("PS").Where(inst=>(this.Attr_Process_ID==((DomainClassPS)inst).Attr_Process_ID));
            foreach (var c in candidates)
            {
                result.Add((DomainClassPS)c);
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
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):takeEvent({domainEvent.EventNumber})");
        }

        
        public bool Validate()
        {
            bool isValid = true;
            if (relR1REQIsUsedBy == null)
            {
                isValid = false;
            }
            if (relR1RESIsUserOf == null)
            {
                isValid = false;
            }
            if (relR3PSFirstStep == null)
            {
                isValid = false;
            }
            if (relR7PSCurrentStep == null)
            {
                isValid = false;
            }
            if (this.LinkedR2().Count() == 0)
            {
                isValid = false;
            }

            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:P(Process_ID={this.Attr_Process_ID}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            stateMachine.ForceUpdateState((int)propertyValues["current_state"]);
            attr_Requester_ID = (string)propertyValues["Requester_ID"];
            stateof_Requester_ID = false;
            attr_Resource_ID = (string)propertyValues["Resource_ID"];
            stateof_Resource_ID = false;
            attr_Process_ID = (string)propertyValues["Process_ID"];
            stateof_Process_ID = false;
            attr_firstProcessSpec_ID = (string)propertyValues["firstProcessSpec_ID"];
            stateof_firstProcessSpec_ID = false;
            attr_currentProcessSpec_ID = (string)propertyValues["currentProcessSpec_ID"];
            stateof_currentProcessSpec_ID = false;
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            results.Add("current_state", stateMachine.CurrentState);

            if (stateof_Requester_ID)
            {
                results.Add("Requester_ID", attr_Requester_ID);
                stateof_Requester_ID = false;
            }
            if (stateof_Resource_ID)
            {
                results.Add("Resource_ID", attr_Resource_ID);
                stateof_Resource_ID = false;
            }
            if (stateof_Process_ID)
            {
                results.Add("Process_ID", attr_Process_ID);
                stateof_Process_ID = false;
            }
            if (stateof_firstProcessSpec_ID)
            {
                results.Add("firstProcessSpec_ID", attr_firstProcessSpec_ID);
                stateof_firstProcessSpec_ID = false;
            }
            if (stateof_currentProcessSpec_ID)
            {
                results.Add("currentProcessSpec_ID", attr_currentProcessSpec_ID);
                stateof_currentProcessSpec_ID = false;
            }

            return results;
        }

        public string GetIdentities()
        {
            string identities = $"Process_ID={this.Attr_Process_ID}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("current_state", stateMachine.CurrentState);
            if (!onlyIdentity) results.Add("Requester_ID", attr_Requester_ID);
            if (!onlyIdentity) results.Add("Resource_ID", attr_Resource_ID);
            results.Add("Process_ID", attr_Process_ID);
            if (!onlyIdentity) results.Add("firstProcessSpec_ID", attr_firstProcessSpec_ID);
            if (!onlyIdentity) results.Add("currentProcessSpec_ID", attr_currentProcessSpec_ID);

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
