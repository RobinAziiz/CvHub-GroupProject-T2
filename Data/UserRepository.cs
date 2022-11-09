using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IdentityUser GetUser(int id)
        {
            var ctx = new ApplicationDbContext();
            List<IdentityUser> list = ctx.Users.ToList();
            IdentityUser user = list.FirstOrDefault(x => x.ApplicationUser == User.Identity.GetUserId());
            int id = enprofil.ProfileId;
            return id;
        }

        public IdentityUser GetLoggedInUser()
        {

        }
    }
}
