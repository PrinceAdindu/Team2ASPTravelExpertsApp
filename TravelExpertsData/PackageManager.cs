using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class PackageManager
    {
        public static List<Package> GetPackages()
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                return db.Packages.ToList();
            }
        }

        public static Package? GetPackageByID(int packageID)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                return db.Packages.Find(packageID);
            }
        }
    }
}
