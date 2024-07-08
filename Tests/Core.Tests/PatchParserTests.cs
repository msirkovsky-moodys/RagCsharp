using Core.GitHubIntegration;
using FluentAssertions;

namespace Core.Tests
{
    public class PatchParserTests
    {
        [Fact]
        public void Test_HappyPath()
        {
            const string json = """
                @@ -1,2 +1,7 @@
                 ﻿var hello = "Hello, World!";
                -Console.WriteLine(hello);
                \ No newline at end of file
                +Console.WriteLine(hello);
                +
                +
                +
                +var hello2 = "Hello, World!";
                +Console.WriteLine(hello2);
                \ No newline at end of file
                """;

            var result = PatchParser.ExtractAddedLines(json);
            result.Should().Be("""
               Console.WriteLine(hello);                                                            var hello2 = "Hello, World!";               Console.WriteLine(hello2);
               """);
        }
    }
}