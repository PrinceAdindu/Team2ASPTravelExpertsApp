using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly TravelExpertsContext _context;

        public ContactController(TravelExpertsContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var agents = from agent in _context.Agents
                         join agency in _context.Agencies
                         on agent.AgencyId equals agency.AgencyId
                         select new AgentAgencyDTO
                         {
                             AgtFirstName = agent.AgtFirstName,
                             AgtLastName = agent.AgtLastName,
                             AgtPosition = agent.AgtPosition,
                             AgtBusPhone = agent.AgtBusPhone,
                             AgtEmail = agent.AgtEmail,
                             AgncyAddress = agency.AgncyAddress,
                             AgncyCity = agency.AgncyCity,
                             AgncyProv = agency.AgncyProv,
                             AgncyPostal = agency.AgncyPostal,
                             AgncyCountry = agency.AgncyCountry
                         };

            return View(agents.ToList());
        }
    }
}
