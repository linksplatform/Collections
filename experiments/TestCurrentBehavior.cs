using System;
using Platform.Collections.Trees;

namespace Experiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing current SetChildValue behavior...");
            
            var root = new Node();
            
            // Test setting value with multiple keys
            root.SetChildValue("FinalValue", "key1", "key2", "key3");
            
            // Check values at each level
            Console.WriteLine($"Root value: {root.Value}");
            Console.WriteLine($"Level 1 (key1) value: {root.GetChildValue("key1")}");
            Console.WriteLine($"Level 2 (key1->key2) value: {root.GetChildValue("key1", "key2")}");
            Console.WriteLine($"Level 3 (key1->key2->key3) value: {root.GetChildValue("key1", "key2", "key3")}");
            
            Console.WriteLine();
            Console.WriteLine("What we EXPECT:");
            Console.WriteLine("Root value: null");
            Console.WriteLine("Level 1 (key1) value: null");
            Console.WriteLine("Level 2 (key1->key2) value: null");
            Console.WriteLine("Level 3 (key1->key2->key3) value: FinalValue");
            
            Console.WriteLine();
            Console.WriteLine("What we ACTUALLY GET:");
            Console.WriteLine("Root value: " + (root.Value ?? "null"));
            Console.WriteLine("Level 1 (key1) value: " + (root.GetChildValue("key1") ?? "null"));
            Console.WriteLine("Level 2 (key1->key2) value: " + (root.GetChildValue("key1", "key2") ?? "null"));
            Console.WriteLine("Level 3 (key1->key2->key3) value: " + (root.GetChildValue("key1", "key2", "key3") ?? "null"));
        }
    }
}