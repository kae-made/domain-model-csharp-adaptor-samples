// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Azure.Identity;
using Kae.DomainModel.Csharp.Framework;
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kae.DomainModel.CSharp.Utility.Application.WebAPIAppDomainModelViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainModelController : ControllerBase
    {

        public DomainModelController(ILogger<DomainModelController> logger)
        {
            InitializeDomainModelAdaptor(logger);
        }



        // GET: api/<DomainOperationsController>
        [HttpGet]
        public string Get()
        {
            return domainModelAdaptor.GetDomainModelSpec();
        }

        // GET api/<DomainOperationsController>/5
        [HttpGet("{classKeyLetter}")]
        public string Get(string classKeyLetter)
        {
            string result = "{}";
            switch (classKeyLetter)
            {
                case "class-spec":
                    result = domainModelAdaptor.GetClassesSpec();
                    break;
                default:
                    result = domainModelAdaptor.GetInstances(classKeyLetter);
                    break;
            }
            return result;
        }

        // POST api/<DomainOperationsController>
        [HttpPost]
        public string Post([FromBody] RequestingParameters value)
        {
            return domainModelAdaptor.InvokeDomainOperation(value.Name, value);
        }

        // POST api/<DomainOperationsController>
        [HttpPost("{classKeyLett}")]
        public string Post(string classKeyLett, [FromBody] RequestingParameters value)
        {
            string result = "{}";
            switch (value.OpType)
            {
                case "instance":
                    result = domainModelAdaptor.GetInstance(classKeyLett, value.Identities);
                    break;
                case "operation":
                    result = domainModelAdaptor.InvokeDomainClassOperation(classKeyLett, value.Name, value);
                    break;
                case "event":
                    result = domainModelAdaptor.SendEvent(classKeyLett, value.Name, value);
                    break;
                case "linked":
                    result = domainModelAdaptor.GetLinkedInstances(classKeyLett, value.Identities, value.Name);
                    break;
            }
            return result;
        }

        [HttpPatch("{classKeyLett}")]
        public string Patch(string classKeyLett, RequestingParameters value)
        {
            return domainModelAdaptor.UpdateClassProperties(classKeyLett, value);
        }

        public static string DomainModelAdaptorDllPath { get; set; }

        protected static DomainModelAdaptor domainModelAdaptor;
        protected static WebAPILogger _logger;
        protected void InitializeDomainModelAdaptor(ILogger<DomainModelController> logger)
        {
            if (domainModelAdaptor == null)
            {
                _logger = new WebAPILogger(logger);
                if (!string.IsNullOrEmpty(DomainModelAdaptorDllPath))
                {
                    var adaptorAssembly = Assembly.LoadFrom(DomainModelAdaptorDllPath);
                    var loadedModules = adaptorAssembly.GetLoadedModules();
                    if (loadedModules.Length > 0)
                    {
                        var adaptorModule = loadedModules[0];
                        string nsOfAdaptor = adaptorModule.Name.Substring(0, adaptorModule.Name.LastIndexOf(".dll"));
                        var typeOfAdaptorEntry = adaptorModule.GetType($"{nsOfAdaptor}.Adaptor.DomainModelAdaptorEntry");
                        if (typeOfAdaptorEntry != null)
                        {
                            var methodOfGetAdaptor = typeOfAdaptorEntry.GetMethod("GetAdaptor");
                            if (methodOfGetAdaptor != null)
                            {
                                domainModelAdaptor = methodOfGetAdaptor.Invoke(null, new object[] { _logger }) as DomainModelAdaptor;
                                if (domainModelAdaptor != null)
                                {
                                    var configForDomainModel = new Dictionary<string, IDictionary<string, object>>();
                                    var domainModelConfigKeys = domainModelAdaptor.ConfigurationKeys();
                                    foreach (var eeKey in domainModelConfigKeys.Keys)
                                    {
                                        configForDomainModel.Add(eeKey, new Dictionary<string, object>());
                                        if (eeKey == "AzureDigitalTwins")
                                        {
                                            string adtInstanceUriKey = "ADTInstanceUri";
                                            configForDomainModel[eeKey].Add(adtInstanceUriKey, AppConfig.GetConnectionString(adtInstanceUriKey));
                                            string adtCredentialKey = "ADTCredential";
                                            configForDomainModel[eeKey].Add(adtCredentialKey, new DefaultAzureCredential());
                                        }
                                        else
                                        {
                                            foreach (var ckey in domainModelConfigKeys[eeKey])
                                            {
                                                configForDomainModel[eeKey].Add(ckey, AppConfig.GetConnectionString(ckey));
                                            }
                                        }
                                    }
                                    domainModelAdaptor.Initialize(configForDomainModel);


                                    domainModelAdaptor.RegisterUpdateHandler(classPropretiesUpdated, relationshipUpdated);
                                }
                            }
                        }
                    }

                }
            }
        }

        public static string HubUrl { get; set; }

        public static ConfigurationManager AppConfig { get; set; }
        protected static HubConnection? GetHubConnection()
        {
            HubConnection hubConnection = null;
            if (!string.IsNullOrEmpty(HubUrl))
            {
                hubConnection = new HubConnectionBuilder().WithUrl(HubUrl).WithAutomaticReconnect().Build();
            }
            return hubConnection;
        }
        private static void relationshipUpdated(object sender, RelationshipUpdatedEventArgs e)
        {
            GetHubConnection()?.InvokeAsync("RelationshipUpdated", e.RelationshipId, e.Phrase, e.SourceClassKeyLetter, e.SourceIdentities, e.DestinationClassKeyLetter, e.DestinationIdentities).Wait();
        }

        private static void classPropretiesUpdated(object sender, ClassPropertiesUpdatedEventArgs e)
        {
            GetHubConnection()?.InvokeAsync("InstanceUpdated", e.ClassKeyLetter, e.Operation, e.Identities, e.Properties);
        }
    }
}
