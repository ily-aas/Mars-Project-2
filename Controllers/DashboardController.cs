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

        public IActionResult Dashboard()
        {
            var dashObj = new DashboardModel();

            // Call the asynchronous method
            CallApiEndpointAsync(dashObj);

            // Pass complaints list to the view as the model
            return View(dashObj);
        }

        static async Task CallApiEndpointAsync(DashboardModel dashObj)
        {
            string apiUrl = "https://localhost:44398/Complaints";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        List<Mars_Project_1.Models.Complaint> complaints = await response.Content.ReadAsAsync<List<Mars_Project_1.Models.Complaint>>();

                        foreach (var complaint in complaints)
                        {

                            dashObj.ComplaintList.Add(new webComplaintsModel
                            { 
                                
                                webComplaintFname = complaint.complaintFname,
                                webComplaintLname = complaint.complaintLname,
                                webComplaintEmail = complaint.complaintEmail,
                                webComplaintMnumber = complaint.complaintMnumber,
                                webComplaintDetails = complaint.complaintDetails,
                                webComplaintIP = complaint.complaintIP,
                                webComplaintDateCreated = complaint.complaintDateCreated,
                                

                                });
                            
                        }
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

           
        }


    }
}

