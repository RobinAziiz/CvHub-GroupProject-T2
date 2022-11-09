using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    [RoutePrefix("api/message")]
    public class MessageApiController : ApiController
    {
        private CvHelper cvHelper
        {
            get
            { 
                return new CvHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        private MessageHelper messageHelper
        {
            get
            {
                return new MessageHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        [HttpGet]
        [Route("unreadmessages")]
        public int UnreadMessages()
        {
            var creator = User.Identity.GetUserId();
            var count = messageHelper.CountUnreadMessages(creator);
            return count;
        }
        [HttpPost]
        [Route("send")]
        public IHttpActionResult SendMessage(Message model)
        {
            var context = Request.GetOwinContext().Get<ApplicationDbContext>();
            Message message = new Message();
            message.MessageText = model.MessageText;
            if (model.Sender == null && model.SenderId == 0) return BadRequest();
            if (model.MessageText == null) return BadRequest();
            if (model.Sender == null)
            {
                message.Sender = cvHelper.GetCvOnId(model.SenderId).FirstName + " " +
                                 cvHelper.GetCvOnId(model.SenderId).LastName;
            }
            else{
                message.Sender = model.Sender;
            }
            message.RecieverID = model.RecieverID;
            message.DateTime = DateTime.Now;
            CV cv = cvHelper.GetCvOnId(message.RecieverID);
            cv.Messages.Add(message);
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        
        [HttpDelete]
        [Route("delete")]
        public void DeleteMessage(Message model)
        {
            var context = Request.GetOwinContext().Get<ApplicationDbContext>();
            var cv = cvHelper.GetCvOnId(model.RecieverID);
            var message = cv.Messages.FirstOrDefault(x=> x.Id == model.Id);
            cv.Messages.Remove(message);
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
        }
        [HttpPost]
        [Route("read")]
        public void ReadMessage(Message model)
        {
            var context = Request.GetOwinContext().Get<ApplicationDbContext>();
            var cv = cvHelper.GetCvOnId(model.RecieverID);
            var message = cv.Messages.FirstOrDefault(x => x.Id == model.Id);
            message.Read = model.Read;
            context.Entry(cv).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}