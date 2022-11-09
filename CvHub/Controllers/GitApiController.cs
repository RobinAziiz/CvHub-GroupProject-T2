using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Data;
using Data.Models;
using Data.Repositories;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using Shared.Models;
using Microsoft.Owin.Host.SystemWeb;
using Newtonsoft.Json;

namespace CvHub.Controllers
{
    [RoutePrefix("api/git")]
    public class GitApiController : ApiController
    {
        private CvHelper cvHelper
        {
            get
            { 
                return new CvHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        [HttpGet]
        [Route("getrepos")]
        public async Task<IHttpActionResult> GetRepos(GitModel model)
        {
            var context = Request.GetOwinContext().Get<ApplicationDbContext>();
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            if (model.Username == null) return BadRequest();
            var username = model.Username;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            string response = await client.GetStringAsync("https://api.github.com/users/" + username + "/repos");
            List<GitRepo> gitrepo = JsonConvert.DeserializeObject<List<GitRepo>>(response);
            if (gitrepo.Count == 0) return BadRequest();
            if(CV.Repos.Count > 0) CV.Repos.Clear();
            foreach (var repo in gitrepo)
            {
                CV.Repos.Add(repo);
                context.SaveChanges();
            }

            return Ok();
        }

    }
}