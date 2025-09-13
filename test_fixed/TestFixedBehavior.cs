using System;
using Platform.Collections.Trees;

namespace Experiments
{
    class FixedBehaviorTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing FIXED SetChildValue behavior...");
            
            var root = new Node();
            
            // Test setting value with multiple keys
            root.SetChildValue("FinalValue", "key1", "key2", "key3");
            
            // Check values at each level
            Console.WriteLine($"Root value: {root.Value ?? "null"}");
            Console.WriteLine($"Level 1 (key1) value: {root.GetChildValue("key1") ?? "null"}");
            Console.WriteLine($"Level 2 (key1->key2) value: {root.GetChildValue("key1", "key2") ?? "null"}");
            Console.WriteLine($"Level 3 (key1->key2->key3) value: {root.GetChildValue("key1", "key2", "key3") ?? "null"}");
            
            Console.WriteLine();
            Console.WriteLine("EXPECTED vs ACTUAL:");
            Console.WriteLine($"Root value: null -> {root.Value ?? "null"} {(root.Value == null ? "✓" : "✗")}");
            Console.WriteLine($"Level 1 (key1) value: null -> {root.GetChildValue("key1") ?? "null"} {(root.GetChildValue("key1") == null ? "✓" : "✗")}");
            Console.WriteLine($"Level 2 (key1->key2) value: null -> {root.GetChildValue("key1", "key2") ?? "null"} {(root.GetChildValue("key1", "key2") == null ? "✓" : "✗")}");
            Console.WriteLine($"Level 3 (key1->key2->key3) value: FinalValue -> {root.GetChildValue("key1", "key2", "key3") ?? "null"} {(root.GetChildValue("key1", "key2", "key3")?.ToString() == "FinalValue" ? "✓" : "✗")}");
            
            Console.WriteLine();
            Console.WriteLine("Additional test - setting different values at different paths:");
            root.SetChildValue("Value1", "a");
            root.SetChildValue("Value2", "a", "b");
            root.SetChildValue("Value3", "x", "y", "z");
            
            Console.WriteLine($"Path 'a' value: {root.GetChildValue("a") ?? "null"} (expected: Value1)");
            Console.WriteLine($"Path 'a' -> 'b' value: {root.GetChildValue("a", "b") ?? "null"} (expected: Value2)");
            Console.WriteLine($"Path 'x' -> 'y' -> 'z' value: {root.GetChildValue("x", "y", "z") ?? "null"} (expected: Value3)");
            Console.WriteLine($"Path 'x' value: {root.GetChildValue("x") ?? "null"} (expected: null)");
            Console.WriteLine($"Path 'x' -> 'y' value: {root.GetChildValue("x", "y") ?? "null"} (expected: null)");
        }
    }
}