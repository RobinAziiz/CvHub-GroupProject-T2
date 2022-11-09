using System.Data.Entity;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Data;
using Data.Models;
using Data.Repositories;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using Shared.Models;
using Microsoft.Owin.Host.SystemWeb;

namespace CvHub.Controllers
{
    [RoutePrefix("api/cv")]
    public class CvVisistsApiController : ApiController
    {
        private CvHelper cvHelper
        {
            get
            {
                return new CvHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        [HttpGet]
        [Route("visitors")]
        public int Visitors()
        {
                var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
                var visits = CV.Visits;
                if (visits == null) visits = 0;
                return visits;
            
        }
        [HttpPost]
        [Route("newView")]
        public void IncrementVisitCount(CVmodel model)
        {
                var context = Request.GetOwinContext().Get<ApplicationDbContext>();
                var cv = cvHelper.GetCvOnId(model.cvID);
                cv.Visits++;
                context.Entry(cv).State = EntityState.Modified;
                context.SaveChanges();
            
        }
    }
}
