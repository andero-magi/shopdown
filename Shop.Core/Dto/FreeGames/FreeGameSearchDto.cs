namespace Shop.Core.Dto.FreeGames;

using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
public class FreeGameSearchDto
{
    public static readonly IReadOnlyList<string> GameTags =
    [
        "mmorpg", "shooter", "strategy", 
        "moba", "racing", "sports", 
        "social", "sandbox", "open-world", 
        "survival", "pvp", "pve", 
        "pixel", "voxel", "zombie", 
        "turn-based", "first-person", "third-person", 
        "top-down", "tank", "space", 
        "sailing", "side-scroller", "superhero", 
        "permadeath", "card", "battle-royale", 
        "mmo", "mmofps", "mmotps", 
        "3d", "2d", "anime", 
        "fantasy", "sci-fi", "fighting", 
        "action-rpg", "action", "military",
        "martial-arts", "flight", "low-spec", 
        "tower-defense", "horror", "mmorts"
    ];

    public GamePlatform Platform { get; set; } = GamePlatform.All;

    public List<string> Tags { get; set; } = [];

    public GameSort Sort { get; set; } = GameSort.Relevance;
}

public enum GamePlatform
{
    All,
    PC,
    WebBrowser
}

public enum GameSort
{
    Relevance,
    Popularity,
    ReleaseDate,
    Alphabetic
}
