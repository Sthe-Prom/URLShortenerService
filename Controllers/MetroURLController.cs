using Microsoft.AspNetCore.Mvc;
using URLShortenerService.Models;
using URLShortenerService.Models.Repos;
using URLShortenerService.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
// using System.Text.Json;
using System.Threading.Tasks;

namespace URLShortenerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetroURLController: ControllerBase
    {
        private readonly IMetroURL context;

        public MetroURLController(IMetroURL _context)
        {
            context = _context;
        }

        //GET: metroURLs
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMetroURL()
        {
            var metroURLs = await context.GetAllMetroURL();

            if (metroURLs == null)
            {
                return NotFound();
            }

            return Ok(metroURLs);
        }

        //GET: metroURL/s
        [HttpGet("id")]
        public async Task<IActionResult> GetMetroURLById(int Id)
        {
            var metroURL = await context.GetMetroURLById(Id);

            if (metroURL == null)
            {
                return null;
            }

            return Ok(metroURL);

        }

        //POST
        [HttpPost("add")]
        public async Task<IActionResult> AddMetroURL(string LongUrl)
        {
            var metroURL = new MetroURL();

            string shortUrl = await ShortenUrl(LongUrl);

            metroURL.LongUrl = LongUrl;            
            metroURL.ShortUrl = shortUrl;

            var addedMetroURL = await context.AddMetroURL(metroURL);

            //return CreatedAtAction(nameof(GetMetroURLById), new { id = addedMetroURL.ShortUrl }, addedMetroURL);
            return Ok(metroURL);
        }

        //HTTPUT: metroURL/s
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMetroURL([FromBody] MetroURL metroURL)
        {
            var updatedMetroURL = context.UpdateMetroURL(metroURL);

            if (updatedMetroURL == null)
            {
                return NotFound();
            }
            return Ok(updatedMetroURL);
        }


        //Other methods

        [HttpPost]
        public async Task<string> ShortenUrl(string longUrl)
        {
            string apiKey = "35cb00286c4d7d22be87eb1ffd983557894c8f88"; // Replace with your actual API key
            string baseUrl = "https://api-ssl.bitly.com/v4/shorten";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var data = new { long_url = longUrl };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(baseUrl, content);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);

                    string shortenedUrl = responseObject["link"].ToString();

                    // Store shortenedUrl in your database if needed

                    return shortenedUrl;
                }
                catch (Exception ex)
                {
                    // Handle errors (e.g., log the error, return a default value)
                    Console.WriteLine($"Error shortening URL: {ex.Message}");
                    return longUrl; // Or return a default value
                }
            }
        }

    }
}