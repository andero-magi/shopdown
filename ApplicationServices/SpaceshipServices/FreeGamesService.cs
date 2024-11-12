namespace Shop.ApplicationServices.SpaceshipServices;

using Nancy.Json;
using Shop.Core.Dto.FreeGames;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class FreeGamesService: IFreeGamesService
{
    public async Task<List<FreeGameDto>> QueryGames(FreeGameSearchDto? search)
    {
        var url = $"https://www.freetogame.com/api/games{CreateSearchParams(search)}";


        using (WebClient c = new())
        {
            var jsonString = c.DownloadString(url);
            return JsonSerializer.Deserialize<List<FreeGameDto>>(jsonString);
        }
    }

    private string CreateSearchParams(FreeGameSearchDto? search)
    {
        if (search == null)
        {
            return string.Empty;
        }

        string result = "?";

        if (search.Tags.Any())
        {
            string tagList = string.Join(".", search.Tags.ToArray());
            result += $"&tag={tagList}";
        }

        if (search.Platform != GamePlatform.All)
        {
            result += $"&platform={GetPlatformName(search.Platform)}";
        }

        result += $"&sort-by={GetSortName(search.Sort)}";

        return result;
    }

    private string GetSortName(GameSort sort)
    {
        return sort switch
        {
            GameSort.Relevance => "relevance",
            GameSort.Alphabetic => "alphabetical",
            GameSort.Popularity => "popularity",
            GameSort.ReleaseDate => "release-date",
            _ => "relevance"
        };
    }

    private string GetPlatformName(GamePlatform platform)
    {
        return platform switch
        {
            GamePlatform.PC => "pc",
            GamePlatform.WebBrowser => "browser",
            _ => "all",
        };
    }
}
