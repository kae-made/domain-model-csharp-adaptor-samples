using Kae.DomainModel.Csharp.Framework.Adaptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIAppUIDomainAdaptor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainModelController : ControllerBase
    {
        private readonly WebAPILogger _logger;

        public DomainModelController(ILogger<DomainModelController> logger)
        {
            _logger = new WebAPILogger(logger);
        }



        // GET: api/<DomainOperationsController>
        [HttpGet]
        public string Get()
        {
            var domainModelAdaptor = Program.GetDomainModelAdaptor(_logger);
            
            return domainModelAdaptor.GetDomainOperationsSpec();
        }

        // GET api/<DomainOperationsController>/5
        [HttpGet("{classKeyLetter}")]
        public string Get(string  classKeyLetter)
        {
            var domainModelAdaptor = Program.GetDomainModelAdaptor(_logger);
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
            var domainModelAdaptor = Program.GetDomainModelAdaptor(_logger);
            return domainModelAdaptor.InvokeDomainOperation(value.Name, value);
        }

        // POST api/<DomainOperationsController>
        [HttpPost("{classKeyLett}")]
        public string Post(string classKeyLett, [FromBody] RequestingParameters value)
        {
            var domainModelAdaptor = Program.GetDomainModelAdaptor(_logger);
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
            var domainModelAdaptor = Program.GetDomainModelAdaptor(_logger);
            return domainModelAdaptor.UpdateClassProperties(classKeyLett, value);
        }

    }

    public class WebAPILogger : Kae.Utility.Logging.Logger
    {
        ILogger<DomainModelController> logger;

        public WebAPILogger(ILogger<DomainModelController> logger)
        {
            this.logger = logger;
        }

        protected override Task LogInternal(Level level, string log, string timestamp)
        {
            throw new System.NotImplementedException();
        }
    }
}
