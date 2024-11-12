namespace Shop.Core.ServiceInterface;

using Shop.Core.Dto.FreeGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFreeGamesService
{
    public Task<List<FreeGameDto>> QueryGames(FreeGameSearchDto? search);
}
