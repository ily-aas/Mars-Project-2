using Azure;
using Mars_Project_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Text;
using Project_2_Web_App.Models;

namespace Project_2_Web_App.Controllers
{
    public class DashboardController : Controller
    {

        private readonly List<webComplaintsModel> _populatedList;

        // Constructor injection to receive the populated list
        public DashboardController(List<webComplaintsModel> populatedList)
        {
            _populatedList = populatedList;
        }

        public IActionResult Dashboard()
        {
            return View("Dashboard", _populatedList);
        }

    }
}

