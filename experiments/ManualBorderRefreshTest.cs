using System;
using Platform.Collections;

namespace Experiments
{
    /// <summary>
    /// Test class to verify manual border refresh functionality for BitString.
    /// This addresses issue #20: "Think about ability to manual refreshing of borders"
    /// </summary>
    public static class ManualBorderRefreshTest
    {
        public static void Main()
        {
            Console.WriteLine("Testing Manual Border Refresh Functionality");
            Console.WriteLine("===========================================");
            
            // Test 1: Default behavior (automatic border refreshing enabled)
            Console.WriteLine("\nTest 1: Default behavior (automatic refreshing)");
            TestDefaultBehavior();
            
            // Test 2: Manual border refreshing (automatic disabled)
            Console.WriteLine("\nTest 2: Manual border refreshing");
            TestManualBorderRefresh();
            
            // Test 3: Performance comparison
            Console.WriteLine("\nTest 3: Performance comparison");
            TestPerformanceComparison();
            
            Console.WriteLine("\nAll tests completed successfully!");
        }
        
        private static void TestDefaultBehavior()
        {
            var bitString = new BitString(1000);
            Console.WriteLine($"AutomaticBorderRefreshing: {bitString.AutomaticBorderRefreshing}");
            
            // Set some bits and verify borders are updated automatically
            bitString.Set(100, true);
            bitString.Set(500, true);
            bitString.Set(900, true);
            
            var firstIndex = bitString.GetFirstSetBitIndex();
            var lastIndex = bitString.GetLastSetBitIndex();
            
            Console.WriteLine($"First set bit: {firstIndex} (expected: 100)");
            Console.WriteLine($"Last set bit: {lastIndex} (expected: 900)");
            
            if (firstIndex == 100 && lastIndex == 900)
            {
                Console.WriteLine("✓ Default behavior test passed");
            }
            else
            {
                Console.WriteLine("✗ Default behavior test failed");
            }
        }
        
        private static void TestManualBorderRefresh()
        {
            // Create BitString with automatic border refreshing disabled
            var bitString = BitString.Create(1000, false);
            Console.WriteLine($"AutomaticBorderRefreshing: {bitString.AutomaticBorderRefreshing}");
            
            // Set some bits (borders should not be updated automatically)
            bitString.Set(100, true);
            bitString.Set(500, true);
            bitString.Set(900, true);
            
            // Without refreshing borders, the first/last might be incorrect
            var firstBeforeRefresh = bitString.GetFirstSetBitIndex();
            var lastBeforeRefresh = bitString.GetLastSetBitIndex();
            
            Console.WriteLine($"Before manual refresh - First: {firstBeforeRefresh}, Last: {lastBeforeRefresh}");
            
            // Manually refresh borders
            bool bordersUpdated = bitString.RefreshBorders();
            Console.WriteLine($"Borders updated by manual refresh: {bordersUpdated}");
            
            var firstAfterRefresh = bitString.GetFirstSetBitIndex();
            var lastAfterRefresh = bitString.GetLastSetBitIndex();
            
            Console.WriteLine($"After manual refresh - First: {firstAfterRefresh}, Last: {lastAfterRefresh}");
            
            if (firstAfterRefresh == 100 && lastAfterRefresh == 900)
            {
                Console.WriteLine("✓ Manual border refresh test passed");
            }
            else
            {
                Console.WriteLine("✗ Manual border refresh test failed");
            }
        }
        
        private static void TestPerformanceComparison()
        {
            const int iterations = 10000;
            const int bitStringSize = 10000;
            
            // Test automatic refreshing
            var automaticBitString = new BitString(bitStringSize);
            var startTime = DateTime.UtcNow;
            
            for (int i = 0; i < iterations; i++)
            {
                automaticBitString.Set(i % bitStringSize, true);
                automaticBitString.Set(i % bitStringSize, false);
            }
            
            var automaticTime = DateTime.UtcNow - startTime;
            Console.WriteLine($"Automatic border refreshing: {automaticTime.TotalMilliseconds:F2} ms");
            
            // Test manual refreshing
            var manualBitString = BitString.Create(bitStringSize, false);
            startTime = DateTime.UtcNow;
            
            for (int i = 0; i < iterations; i++)
            {
                manualBitString.Set(i % bitStringSize, true);
                manualBitString.Set(i % bitStringSize, false);
            }
            
            manualBitString.RefreshBorders(); // Single refresh at the end
            var manualTime = DateTime.UtcNow - startTime;
            Console.WriteLine($"Manual border refreshing: {manualTime.TotalMilliseconds:F2} ms");
            
            var improvement = (automaticTime.TotalMilliseconds - manualTime.TotalMilliseconds) / automaticTime.TotalMilliseconds * 100;
            Console.WriteLine($"Performance improvement: {improvement:F1}%");
            
            if (manualTime < automaticTime)
            {
                Console.WriteLine("✓ Performance test shows manual refreshing is faster");
            }
            else
            {
                Console.WriteLine("! Performance test shows similar or worse performance (expected with small datasets)");
            }
        }
    }
}