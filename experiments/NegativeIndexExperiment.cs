using System;
using Platform.Collections.Arrays;

// This experiment demonstrates that the fix works correctly
public class Program
{
    public static void Main()
    {
        var array = new int[] { 1, 2, 3 };
        
        Console.WriteLine("Testing GetElementOrDefault with negative index:");
        
        // Before fix: This would throw IndexOutOfRangeException
        // After fix: Returns default value (0)
        var result1 = array.GetElementOrDefault(-1);
        Console.WriteLine($"GetElementOrDefault(-1) = {result1}"); // Should be 0
        
        var result2 = array.GetElementOrDefault(-10);
        Console.WriteLine($"GetElementOrDefault(-10) = {result2}"); // Should be 0
        
        Console.WriteLine("\nTesting TryGetElement with negative index:");
        
        // Before fix: This would throw IndexOutOfRangeException 
        // After fix: Returns false and sets element to default
        bool success1 = array.TryGetElement(-1, out int element1);
        Console.WriteLine($"TryGetElement(-1, out element) = {success1}, element = {element1}"); // Should be false, 0
        
        bool success2 = array.TryGetElement(-10, out int element2);
        Console.WriteLine($"TryGetElement(-10, out element) = {success2}, element = {element2}"); // Should be false, 0
        
        Console.WriteLine("\nTesting with long versions:");
        
        var result3 = array.GetElementOrDefault(-1L);
        Console.WriteLine($"GetElementOrDefault(-1L) = {result3}"); // Should be 0
        
        bool success3 = array.TryGetElement(-1L, out int element3);
        Console.WriteLine($"TryGetElement(-1L, out element) = {success3}, element = {element3}"); // Should be false, 0
        
        Console.WriteLine("\nTesting with valid indices (should still work):");
        
        var result4 = array.GetElementOrDefault(1);
        Console.WriteLine($"GetElementOrDefault(1) = {result4}"); // Should be 2
        
        bool success4 = array.TryGetElement(1, out int element4);
        Console.WriteLine($"TryGetElement(1, out element) = {success4}, element = {element4}"); // Should be true, 2
        
        Console.WriteLine("\nAll tests completed without exceptions!");
    }
}