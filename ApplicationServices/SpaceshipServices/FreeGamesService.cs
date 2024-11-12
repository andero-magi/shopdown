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
        var url = $"https://www.freetogame.com/api/{CreateSearchParams(search)}";


        using (WebClient c = new())
        {
            try 
            {
                var jsonString = c.DownloadString(url);
                
                if (jsonString.Contains("status\":0")) {
                    return [];
                }

                return JsonSerializer.Deserialize<List<FreeGameDto>>(jsonString);
            } 
            catch (WebException exc) 
            {
                var httpRes = exc.Response as HttpWebResponse;
                if (httpRes == null)
                {
                    throw;
                }

                if (httpRes.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }

                throw;
            }
        }
    }

    private string CreateSearchParams(FreeGameSearchDto? search)
    {
        if (search == null)
        {
            return string.Empty;
        }

        string result = "";

        if (search.Tags.Any())
        {
            string tagList = string.Join(".", search.Tags.ToArray());
            result += $"filter?tag={tagList}";
        } 
        else 
        {
            result += "games?";
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
