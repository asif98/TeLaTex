using System.Web;

namespace TeLaTeX;

public static class UrlFactory
{
    public static string Create(string code)
    {
        var encoded = HttpUtility.UrlEncode($"{code}").Replace("+", "%20");
        var url = $"https://latex.codecogs.com/png.image?%5Chuge%20%5Cdpi%7B110%7D{encoded}";
        return url;
    }
}