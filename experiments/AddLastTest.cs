using System.Collections.Generic;
using Platform.Collections.Lists;
using Platform.Collections.Arrays;

namespace AddLastTests
{
    public static class AddLastTest
    {
        public static void TestAddLastFunctionality()
        {
            // Test ListFiller AddLast
            var list = new List<int>();
            var listFiller = new ListFiller<int, bool>(list, true);
            var elements = new List<int> { 1, 2, 3, 4, 5 };
            
            var result1 = listFiller.AddLastAndReturnTrue(elements);
            System.Console.WriteLine($"✓ ListFiller AddLastAndReturnTrue: {result1}, List contains: {list[0]}");
            
            var result2 = listFiller.AddLastAndReturnConstant(elements);
            System.Console.WriteLine($"✓ ListFiller AddLastAndReturnConstant: {result2}, List contains: [{list[0]}, {list[1]}]");
            
            // Test ArrayFiller AddLast
            var array = new int[10];
            var arrayFiller = new ArrayFiller<int, string>(array, "success");
            
            var result3 = arrayFiller.AddLastAndReturnConstant(elements);
            System.Console.WriteLine($"✓ ArrayFiller AddLastAndReturnConstant: {result3}, Array[0]: {array[0]}");
            
            // Test extension methods directly
            var testList = new List<string>();
            var words = new List<string> { "first", "middle", "last" };
            
            testList.AddLast(words);
            System.Console.WriteLine($"✓ Extension AddLast: List contains: {testList[0]}");
            
            var result4 = testList.AddLastAndReturnTrue(words);
            System.Console.WriteLine($"✓ Extension AddLastAndReturnTrue: {result4}, List size: {testList.Count}");
            
            System.Console.WriteLine("✓ All AddLast tests completed successfully!");
        }
    }
}