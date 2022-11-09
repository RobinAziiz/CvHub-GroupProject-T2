using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public class CVRepository
    {
        private readonly ApplicationDbContext _context;

        public CVRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CV> ExcludeDeactivatedAccounts(List<CV> list)
        {
            var returnList = new List<CV>();
            foreach (var cv in list)
            {
                if (!cv.Deactivated) returnList.Add(cv);
            }

            return returnList;
        }
        public List<CV> GetAllCvs()
        {
            return _context.CVs.ToList();
        }
        public List<CV> GetRandomPublicCVs(int quantity)
        {
            Random rnd = new Random();
            List<int> usedIndex = new List<int>();
            var CVList = new List<CV>();
            var query = from CV in _context.CVs where !CV.Private && !CV.Deactivated select CV;
            CVList = query.ToList();

            var returnList = new List<CV>();

            int listLength = CVList.Count();
            if (quantity > listLength)
            {
                return CVList;
            }

            for (int i = 0; i < quantity; i++)
            {
                var index = rnd.Next(listLength - 1);
                while (usedIndex.Contains(index))
                {
                    index = rnd.Next(listLength);
                }

                usedIndex.Add(index);
                returnList.Add(CVList[index]);
            }



            return returnList;
        }

        public List<CV> GetRandomCVs(int quantity)
        {
            Random rnd = new Random();
            List<int> usedIndex = new List<int>();
            var CVList = new List<CV>();
            var query = from CV in _context.CVs where !CV.Deactivated select CV;
            CVList = query.ToList();

            var returnList = new List<CV>();

            int listLength = CVList.Count();
            if (quantity > listLength)
            {
                return CVList;
            }

            for (int i = 0; i < quantity; i++)
            {
                var index = rnd.Next(listLength - 1);
                while (usedIndex.Contains(index))
                {
                    index = rnd.Next(listLength);
                }

                usedIndex.Add(index);
                returnList.Add(CVList[index]);
            }



            return returnList;
        }

        public List<CV> RemovePrivateAndDeactivatedCvs(List<CV> list)
        {
            var returnlist = new List<CV>();
            foreach (var cv in list)
            {
                if(!cv.Deactivated && !cv.Private)
                {
                    returnlist.Add(cv);
                }
            }

            return returnlist;
        }

        public CV GetCvOnId(int id)
        {
            return _context.CVs
                .FirstOrDefault(x => x.Id == id);
        }
        public CV GetCVCreator(string creator)
        {
            return _context.CVs
                .FirstOrDefault(x => x.Creator == creator);
        }
        public void Create(CV cV)
        {
            _context.CVs.Add(cV);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var cvDB = _context.CVs.FirstOrDefault(x => x.Id == id);
            if (cvDB != null)
            {
                _context.CVs.Remove(cvDB);
                _context.SaveChanges();
            }

        }

        public List<CV> GetCvsOnProfession(int professionId)
        {
            var returnList = new List<CV>();
            foreach (var cv in ExcludeDeactivatedAccounts(GetAllCvs()))
            {
                if (cv.Profession == professionId)
                {
                    returnList.Add(cv);
                }
            }

            return returnList;
        }

        public List<CV> ExcludeCvInList(List<CV> list, CV cv)
        {
            var returnList = new List<CV>();
            foreach (var CV in list)
            {
                if(CV.Id != cv.Id) returnList.Add(CV);
            }

            return returnList;
        }

    }
}