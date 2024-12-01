using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workloopz.Data;
using Workloopz.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Workloopz.Controllers
{
    [Produces("application/json")]
    [Route("api/link")]
    public class LinkController : Controller
    {
        private readonly NexTasksContext _context;
        public LinkController(NexTasksContext context)
        {
            _context = context;
        }

        // GET api/Link
        [HttpGet]
        public IEnumerable<LinkVM> Get()
        {
            
                return _context.Links
                   .ToList()
                   .Select(t => (LinkVM)t);
            
        }

        // GET api/Link/5
        [HttpGet("{id}")]
        public Link? Get(int id)
        {
            return _context
                .Links
                .Find(id);
        }

        // POST api/Link
        [HttpPost]
        public ObjectResult Post(LinkVM apiLink)
        {
            var newLink = (Link)apiLink;

            _context.Links.Add(newLink);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newLink.Id,
                action = "inserted"
            });
        }


        // PUT api/Link/5
        [HttpPut("{id}")]
        public ObjectResult Put(int id, LinkVM linkVM)
        {
            var updatedLink = (Link)linkVM;
            updatedLink.Id = id;
            _context.Entry(updatedLink).State = EntityState.Modified;


            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/Link/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteLink(int id)
        {
            var Link = _context.Links.Find(id);
            if (Link != null)
            {
                _context.Links.Remove(Link);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}
