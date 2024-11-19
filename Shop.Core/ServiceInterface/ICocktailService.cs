namespace Shop.Core.ServiceInterface;

using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICocktailService
{
    public Task<List<CocktailDrink>> GetCocktails(string search);

    public Task<CocktailDto> GetById(string id);

    public Task SaveToDatabase(CocktailDto dto);

    public Task<List<CocktailDrink>> GetSaved();
}
