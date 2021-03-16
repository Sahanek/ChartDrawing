//Command to run: dotnet run animal attacks {NumberOfResultToPrint} < data.txt

using System;
using System.Collections.Generic;
using System.Linq;

var line = "abecadlo\r\n";
Console.WriteLine(line[..^2]);

var lines = ReadAllLinesFromConsole()
    .Select(line => line.Split('\t'))
    .ToList();

var groupingColumnName = args[0];
var numericColumnName = args[1];
var numberOfResultToPrint = int.Parse(args[2]);

var groupingColumnIndex = Array.IndexOf(lines.First(), groupingColumnName);
var numericColumnIndex = Array.IndexOf(lines.First(), numericColumnName);

var result = lines.Skip(1)
    .GroupBy(line => line[groupingColumnIndex])
    .Select(group => new
    {
        GroupName = group.Key,
        Sum = group.Sum(item => int.Parse(item[numericColumnIndex]))
    })
    .OrderByDescending(line => line.Sum)
    .Take(numberOfResultToPrint)
    .ToList();

var maxSum = result.Max(x => x.Sum);
var maxNameLength = result.Max(x => x.GroupName.Length);

foreach (var group in result)
{
    var chartSign = (int) ((group.Sum * 100) / maxSum);
    Console.Write($"{group.GroupName} |".PadLeft(maxNameLength + 2, ' '));
    Console.BackgroundColor = ConsoleColor.Red;
    Console.WriteLine(" ".PadLeft(chartSign, ' '));
    Console.BackgroundColor = ConsoleColor.Black;
}




static IEnumerable<string> ReadAllLinesFromConsole()
{
    while (true)
    {
        var line = Console.ReadLine();
        if (string.IsNullOrEmpty(line)) break;
        yield return line.EndsWith("\r\n") ? line[..^2] : line;
    }
}
