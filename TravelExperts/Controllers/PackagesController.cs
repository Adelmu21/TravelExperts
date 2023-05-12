using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperts.Models;
using TravelExpertsData;

namespace TravelExperts.Controllers
{
    public class PackagesController : Controller
    {

        private readonly TravelExpertsContext _dbContext;

        public PackagesController(TravelExpertsContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: PackagesController
        public ActionResult Index()
        {
            return RedirectToAction("Packages", "Packages");
        }

        public ActionResult Packages()
        {
            var packages = _dbContext.Packages.Select(p => new PackagesViewModel
            {
                PkgName = p.PkgName,
                PkgStartDate = p.PkgStartDate,
                PkgBasePrice = p.PkgBasePrice,
                PkgDesc = p.PkgDesc,
            }).ToList();

            return View(packages);
        }

    }
}
