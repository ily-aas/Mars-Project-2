using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Project_2_Web_App.Models;

namespace Project_2_Web_App.Controllers
{
    public class webComplaintsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(webComplaintsModel model)
        {
            string apiUrl = "https://localhost:44398/Complaints"; // Adjust the port based on your Project 2 configuration

            string fname = model.ComplaintFname;
            string lname = model.ComplaintLname;
            string email = model.ComplaintEmail;
            string mnumber = model.ComplaintMnumber;
            string details = model.ComplaintDetails;
            string ip = model.ComplaintIP;
            DateTime dateCreated = model.ComplaintDateCreated;

            // Create a complaint object with the desired data
            Mars_Project_1.Models.Complaint complaint = new Mars_Project_1.Models.Complaint
            {
                complaintFname = fname,
                complaintLname = lname,
                complaintEmail = email,
                complaintMnumber = mnumber,
                complaintDetails = details,
                complaintIP = ip,
                complaintDateCreated = dateCreated
            };

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Convert the complaint object to JSON
                    string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(complaint);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send a POST request to the API
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response if needed
                        // Example: Complaint createdComplaint = await response.Content.ReadAsAsync<Complaint>();
                        Console.WriteLine("Complaint posted successfully!");
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

            return View("ThankYou");
        }
    }
}
