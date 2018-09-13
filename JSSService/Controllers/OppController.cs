using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JSSService;

/// <summary>
/// Stubbed out with "Web API 2 Controller with read/write actions"
/// </summary>
namespace JSSService.Controllers
{
    public class OppController : ApiController
    {
        // GET: api/Opp
        public IEnumerable<Opp> Get()
        {
            return new Opp[] 
			{
				new Opp (),
				new Opp ()
			};
        }

        // GET: api/Opp/5
        public Opp Get(int id)
        {
			return new Opp();
		}

		// POST: api/Opp
		public void Post([FromBody]Opp value)
        {
        }

        // PUT: api/Opp/5
        public void Put(int id, [FromBody]Opp value)
        {
        }

        // DELETE: api/Opp/5
        public void Delete(int id)
        {
        }
    }
}
