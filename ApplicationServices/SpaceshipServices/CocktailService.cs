namespace Shop.ApplicationServices.SpaceshipServices;

using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;
using Shop.Core.ServiceInterface;
using Shop.Data;
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
    private readonly ShopContext _context;

    public CocktailService(ShopContext ctx)
    {
        _context = ctx;
    }

    public async Task<List<CocktailDrink>> GetCocktails(string search)
    {
        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={HttpUtility.UrlEncode(search)}";
        var dto = await MakeQuery(url);

        if (dto == null)
        {
            return [];
        }

        return await dto.CreateDomain(TestSaved);
    }

    private async Task<bool> TestSaved(Drink drink)
    {
        return await _context.Drinks.ContainsAsync(drink);
    }

    public async Task<CocktailDto?> GetById(string id)
    {
        string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}";
        return await MakeQuery(url);
    }

    private async Task<CocktailDto?> MakeQuery(string url)
    {
        using (WebClient c = new())
        {
            string json = await c.DownloadStringTaskAsync(url);
            CocktailDto? dto = JsonSerializer.Deserialize<CocktailDto>(json);

            if (dto == null || dto.Drinks == null)
            {
                return null;
            }

            return dto;
        }
    }

    public async Task SaveToDatabase(CocktailDto dto)
    {
        foreach (var item in dto.Drinks)
        {
            if (await _context.Drinks.ContainsAsync(item))
            {
                continue;
            }

            await _context.Drinks.AddAsync(item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CocktailDrink>> GetSaved()
    {
        var list = await _context.Drinks.ToListAsync();
        List<CocktailDrink> result = [];

        foreach (var item in list)
        {
            var d = item.CreateDomain();
            d.IsSaved = true;
            result.Add(d);
        }

        return result;
    }
}
