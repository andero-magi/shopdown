namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using Shop.Models.Norris;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

public class NorrisController : Controller
{
    private const string UNSPECIFIED = "Unspecified";

    private List<SelectListItem> GetListItems()
    {
        var url = "https://api.chucknorris.io/jokes/categories";
        using (WebClient client = new())
        {
            var json = client.DownloadString(url);
            var stringList = new JavaScriptSerializer().Deserialize<List<string>>(json);

            List<SelectListItem> result = new();

            result.Add(new SelectListItem("Unspecified Category", UNSPECIFIED, true));

            foreach (var item in stringList)
            {
                result.Add(new SelectListItem()
                {
                    Text = string.Concat(item.Substring(0, 1).ToUpper(), item.AsSpan(1)),
                    Value = item
                });
            }

            return result;
        }
    }

    public IActionResult Index(NorrisViewModel vm)
    {
        vm.JokePresent = false;
        vm.Categories = GetListItems();

        string? cat = vm.Category;

        if (cat == null)
        {
            return View(vm);
        }

        using (WebClient client = new())
        {
            string url = "https://api.chucknorris.io/jokes/random";

            if (!string.IsNullOrEmpty(vm.Category) && !vm.Category.Equals(UNSPECIFIED))
            {
                url += $"?category={vm.Category}";
            }

            var json = client.DownloadString(url);
            var response = new JavaScriptSerializer().Deserialize<ChuckNorrisResponse>(json);

            vm.Joke = response.Value;
            vm.Url = response.Url;
            vm.JokePresent = true;
        }

        return View(vm);
    }
}

internal class ChuckNorrisResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}