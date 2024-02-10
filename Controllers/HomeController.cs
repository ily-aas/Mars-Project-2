using Microsoft.AspNetCore.Mvc;
using Project_2_Web_App.Models;
using System.Diagnostics;

namespace Project_2_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult webComplaints()
        {
            return View();
        }

        public async Task<IActionResult> LoginTagAsync()
        {

            // Call the asynchronous method to get complaints data
            List<webComplaintsModel> complaints = await CallApiEndpointAsync();

            // Pass the complaints list to the view
            return View("../Dashboard", complaints);

        

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        static async Task<List<webComplaintsModel>> CallApiEndpointAsync()
        {
            string apiUrl = "https://localhost:44398/Complaints";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize response to a list of webComplaintsModel objects
                        List<Mars_Project_1.Models.Complaint> apiComplaints = await response.Content.ReadAsAsync<List<Mars_Project_1.Models.Complaint>>();

                        List<webComplaintsModel> complaints = new List<webComplaintsModel>();

                        foreach (var complaint in apiComplaints)
                        {
                            // Convert each API complaint to webComplaintsModel
                            complaints.Add(new webComplaintsModel
                            {
                                Id = complaint.ID,
                                webComplaintFname = complaint.complaintFname,
                                webComplaintLname = complaint.complaintLname,
                                webComplaintEmail = complaint.complaintEmail,
                                webComplaintMnumber = complaint.complaintMnumber,
                                webComplaintDetails = complaint.complaintDetails,
                                webComplaintIP = complaint.complaintIP,
                                webComplaintDateCreated = complaint.complaintDateCreated
                            });
                        }

                        return complaints;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            return null;
        }

    }
}
