using System;
using Platform.Collections;

class Program 
{
    static void Main()
    {
        Console.WriteLine("Testing BitString Length property behavior");
        
        try
        {
            Console.WriteLine("\n1. Testing Length increase from 32 to 64...");
            var bitString = new BitString(32);
            bitString[5] = true;
            bitString[15] = true;
            Console.WriteLine($"Initial length: {bitString.Length}");
            Console.WriteLine($"Bits set: [5]={bitString[5]}, [15]={bitString[15]}");
            
            bitString.Length = 64; // This should work
            Console.WriteLine($"After increasing to 64: {bitString.Length}");
            Console.WriteLine($"Bits preserved: [5]={bitString[5]}, [15]={bitString[15]}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during length increase: {ex.Message}");
        }
        
        try
        {
            Console.WriteLine("\n2. Testing Length decrease from 64 to 32...");
            var bitString = new BitString(64);
            bitString[5] = true;
            bitString[45] = true;
            Console.WriteLine($"Initial length: {bitString.Length}");
            Console.WriteLine($"Bits set: [5]={bitString[5]}, [45]={bitString[45]}");
            
            bitString.Length = 32; // This currently throws NotImplementedException
            Console.WriteLine($"After decreasing to 32: {bitString.Length}");
            Console.WriteLine($"Bit [5] preserved: {bitString[5]}");
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Length decrease throws NotImplementedException (expected issue)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
        
        try
        {
            Console.WriteLine("\n3. Testing Length set to 0...");
            var bitString = new BitString(10);
            Console.WriteLine($"Initial length: {bitString.Length}");
            
            bitString.Length = 0;
            Console.WriteLine($"After setting to 0: {bitString.Length}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting length to 0: {ex.Message}");
        }
        
        try
        {
            Console.WriteLine("\n4. Testing cross-word boundary (63 to 65)...");
            var bitString = new BitString(63);
            bitString[62] = true;
            Console.WriteLine($"Initial length: {bitString.Length}");
            Console.WriteLine($"Bit [62] set: {bitString[62]}");
            
            bitString.Length = 65;
            Console.WriteLine($"After increasing to 65: {bitString.Length}");
            Console.WriteLine($"Bit [62] preserved: {bitString[62]}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error with cross-word boundary: {ex.Message}");
        }
    }
}
