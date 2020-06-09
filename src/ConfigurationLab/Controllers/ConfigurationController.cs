using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using ConfigurationLab.Models;

namespace ConfigurationLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IConfiguration _configuration;
        private readonly Person _person;

        public ConfigurationController(Person person, IConfiguration configurationRoot, IConfiguration configuration)
        {
            _person = person;
            _configurationRoot = (IConfigurationRoot)configurationRoot;
            _configuration = configuration;
        }

        // GET api/configuration
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> GetConfigurationProviders()
        {
            var providers = new List<string>();

            foreach (var provider in _configurationRoot.Providers.ToList())
            {
                providers.Add(provider.ToString());
            }
            return providers;
        }

        // GET api/configuration
        [HttpGet("GetConfigurationByModel")]
        public ActionResult<Person> GetConfigurationByModel()
        {
            var person = new Person()
            {
                Id = int.Parse(_configuration["Person:Id"]),
                Name = _configuration["Person:Name"].ToString(),
            };

            return person;
        }

        // GET api/configuration
        // Using option pattern
        /* Rules 
         -Must be non-abstract with a public parameterless constructor.
         -All public read-write properties of the type are bound.
         -Fields are not bound. In the preceding code, Position is not bound. The Position property is used
        */
        [HttpGet("GetConfigurationByOptionPattern")]
        public ActionResult<Person> GetConfigurationByOptionPattern()
        {
            var person = new Person();
            _configuration.GetSection(Person.PersonOption).Bind(person);
            return person;
        }


        // Using  Get<T> better than bind
        [HttpGet("GetConfigurationByGetType")]
        public ActionResult<Person> GetConfigurationByGetType()
        {
            var person = _configuration.GetSection(Person.PersonOption).Get<Person>();
            return person;
        }

        // Getting configuration type with dependency injection
        [HttpGet("GetConfigurationWithDI")]
        public ActionResult<Person> GetConfigurationWithDI()
        {
            //Check it is not working
            //Unable to resolve service for type 'ConfigurationLab.Controllers.Person' while attempting to activate 'ConfigurationLab.Controllers.ConfigurationController'.
            return _person;
        }

    }

    public class Person
    {
        public const string PersonOption = "Person";

        public int Id { get; set; }
        public string Name { get; set; }
    }
}