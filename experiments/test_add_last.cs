using System;
using System.Collections.Generic;
using Platform.Collections.Lists;
using Platform.Collections.Arrays;

public class TestAddLast
{
    public static void Main()
    {
        Console.WriteLine("Testing AddLast functionality...");
        
        // Test ListFiller AddLast
        var list = new List<int>();
        var listFiller = new ListFiller<int, bool>(list, true);
        var elements = new List<int> { 1, 2, 3, 4, 5 };
        
        var result1 = listFiller.AddLastAndReturnTrue(elements);
        Console.WriteLine($"ListFiller AddLastAndReturnTrue: {result1}, List now has: [{string.Join(", ", list)}]");
        
        var result2 = listFiller.AddLastAndReturnConstant(elements);
        Console.WriteLine($"ListFiller AddLastAndReturnConstant: {result2}, List now has: [{string.Join(", ", list)}]");
        
        // Test ArrayFiller AddLast
        var array = new int[10];
        var arrayFiller = new ArrayFiller<int, string>(array, "success");
        
        var result3 = arrayFiller.AddLastAndReturnConstant(elements);
        Console.WriteLine($"ArrayFiller AddLastAndReturnConstant: {result3}");
        Console.WriteLine($"Array now has: [{string.Join(", ", array.Take(2))}...]");
        
        // Test extension methods directly
        var testList = new List<string>();
        var words = new List<string> { "first", "middle", "last" };
        
        testList.AddLast(words);
        Console.WriteLine($"Extension AddLast: List now has: [{string.Join(", ", testList)}]");
        
        var result4 = testList.AddLastAndReturnTrue(words);
        Console.WriteLine($"Extension AddLastAndReturnTrue: {result4}, List now has: [{string.Join(", ", testList)}]");
        
        Console.WriteLine("All AddLast tests completed successfully!");
    }
}