using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class FeatureToggleAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Models.FeatureToggle> Get()
        {
            var db = new Models.MusicStoreEntities();
            return  db.FeatureToggles.ToList();
            ;
        }

        // GET api/<controller>/5
        public async Task<Models.FeatureToggle> Get(Guid id)
        {
            
            if (Guid.Empty.Equals(id))
            {
                return null ;
            }
            var db = new Models.MusicStoreEntities();
            Models.FeatureToggle featureTogle = await db.FeatureToggles.FindAsync(id);
            if (featureTogle == null)
            {
                return null;
            }

            return featureTogle;
                
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public async void Put(Guid id, [FromUri]bool value)
        {
            if (Guid.Empty.Equals(id))
            {
                return ;
            }

            var db = new Models.MusicStoreEntities();
            Models.FeatureToggle featureToggle = await db.FeatureToggles.FindAsync(id);
            if (featureToggle == null)
            {
                return ;
            }

            featureToggle.Enabled = value;
            db.Entry(featureToggle).State = EntityState.Modified;
            await db.SaveChangesAsync();

        }
    }
}