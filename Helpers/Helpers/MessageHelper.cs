using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Data.Repositories;
using Shared.Models;

namespace Helpers
{
    public class MessageHelper
    {
        private readonly ApplicationDbContext _context;

        private ProfessionRepository ProfessionRepository
        {
            get { return new ProfessionRepository(_context ?? new ApplicationDbContext()); }
        }

        private CvHelper cvHelper
        {
            get { return new CvHelper(_context ?? new ApplicationDbContext()); }
        }

        public MessageHelper()
        {
        }

        public MessageHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public MessagesViewModel CreateMessagesViewModel(string creator)
        {
            MessagesViewModel viewModel = new MessagesViewModel();
            CV cv = cvHelper.GetCvOnCreator(creator);
            viewModel.Messages = cv.Messages.ToList();
            viewModel.RecieverId = cv.Id;
            return viewModel;
        }
        public int CountUnreadMessages(string creator)
        {
            try
            {
                int count = 0;
                var CV = cvHelper.GetCvOnCreator(creator);
                if (CV == null) return 0;
                List<Message> messages = CV.Messages.ToList();
                foreach (var message in messages)
                {
                    if (!message.Read)
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
