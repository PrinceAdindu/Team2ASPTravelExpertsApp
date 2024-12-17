using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.DbManagers
{
    public class AgentsManager
    {
        private TravelExpertsContext _context;

        //Constructor
        public AgentsManager(TravelExpertsContext context)
        {
            _context = context;
        }

        public List<Agent> GetAgents() {
            var agents = _context.Agents.OrderBy(a=> a.AgtFirstName).ToList();
            return agents ?? new List<Agent>();
        }
    }
}
