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
using Kae.Utility.Logging;
using Kae.DomainModel.Csharp.Framework;

namespace ProcessManagement
{
    public class InstanceRepositoryInMemory : InstanceRepository
    {
        private Logger logger;

        public InstanceRepositoryInMemory(Logger logger)
        {
            this.logger = logger;
        }

        public override void UpdateState(DomainClassDef instance, IDictionary<string, object> chnaged)
        {
            // Do nothing.
        }

        public override void LoadState(IDictionary<string, IList<IDictionary<string, object>>> instances)
        {
            foreach (var className in instances.Keys)
            {
                foreach (var states in instances[className])
                {
                    DomainClassDef newInstance = null;
                    switch (className)
                    {
                        case "OS":
                            newInstance = DomainClassOSBase.CreateInstance(this, logger);
                            break;
                        case "REQ":
                            newInstance = DomainClassREQBase.CreateInstance(this, logger);
                            break;
                        case "RES":
                            newInstance = DomainClassRESBase.CreateInstance(this, logger);
                            break;
                        case "RA":
                            newInstance = DomainClassRABase.CreateInstance(this, logger);
                            break;
                        case "P":
                            newInstance = DomainClassPBase.CreateInstance(this, logger);
                            break;
                        case "PS":
                            newInstance = DomainClassPSBase.CreateInstance(this, logger);
                            break;
                        case "IW":
                            newInstance = DomainClassIWBase.CreateInstance(this, logger);
                            break;
                        default:
                            if (logger != null) logger.LogError($"{className} is not right domain class.");
                            break;
                    }
                    if (newInstance != null)
                    {
                        newInstance.Restore(states);
                    }
                }
            }
        }

        public override void UpdateCInstance(CInstanceChagedState instanceState)
        {
            // Do nothing
        }

        public override void UpdateCLink(CLinkChangedState linkState)
        {
            // To nothing
        }

        public override IEnumerable<T> SelectInstances<T>(string className, IDictionary<string, object> conditionPropertyValues, Func<T, IDictionary<string, object>, bool> compare)
        {
            var resultSet = new List<T>();
            var candidates = domainInstances[className].Where(i => { return compare((T)i, conditionPropertyValues); });
            foreach (var ci in candidates)
            {
                resultSet.Add((T)ci);
            }
            return resultSet;
        }

    }

}