namespace Core.GitHubIntegration;

public class PatchParser
{
    public static string ExtractAddedLines(string patch)
    {
        var addedLines = new List<string>();
        var lines = patch.Split('\n');

        foreach (var line in lines)
        {
            if (line.StartsWith("+") && !line.StartsWith("+++"))
            {
                // Strip the leading '+' to get the raw code
                addedLines.Add(line.Substring(1));
            }
        }

        return string.Join("", addedLines).Trim();
    }
}