using System.Collections.Generic;

namespace ProCards.Web.Logic;

public static class ConcatList
{
    public static string ConcatListOfString(List<string> strings)
    {
        var result = "";
        foreach (var str in strings)
        {
            result += str + "\n";
        }

        return result;
    }
}