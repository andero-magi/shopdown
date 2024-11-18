namespace Shop.ApplicationServices.SpaceshipServices;

using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

public class CocktailService : ICocktailService
{
    public async Task<List<CocktailDrink>> GetCocktails(string search)
    {
        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s=${HttpUtility.UrlEncode(search)}";

        using (WebClient c = new())
        {
            string json = await c.DownloadStringTaskAsync(url);
            CocktailDto dto = JsonSerializer.Deserialize<CocktailDto>(json);

            if (dto == null)
            {
                return [];
            }

            return dto.CreateDomain();
        }
    }
}
