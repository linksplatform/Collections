#include <chrono>
#include <iostream>
#include <vector>

namespace Platform::Collections::Tests 
{
    class PerformanceTimer 
    {
    private:
        std::chrono::high_resolution_clock::time_point start_time;
        
    public:
        PerformanceTimer() : start_time(std::chrono::high_resolution_clock::now()) {}
        
        double elapsed_ms() const 
        {
            auto end_time = std::chrono::high_resolution_clock::now();
            auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end_time - start_time);
            return duration.count() / 1000.0;
        }
    };

    TEST(StackVectorBenchmark, PushBackPerformance) 
    {
        const int iterations = 10000;
        const int elements_per_iteration = 64; // Use stack capacity
        
        // Test StackVector performance
        PerformanceTimer stack_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            StackVector<int, 64> sv;
            for (int j = 0; j < elements_per_iteration; ++j) 
            {
                sv.push_back(j);
            }
        }
        double stack_time = stack_timer.elapsed_ms();
        
        // Test std::vector performance
        PerformanceTimer vector_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            std::vector<int> v;
            v.reserve(elements_per_iteration);
            for (int j = 0; j < elements_per_iteration; ++j) 
            {
                v.push_back(j);
            }
        }
        double vector_time = vector_timer.elapsed_ms();
        
        std::cout << "StackVector time: " << stack_time << " ms" << std::endl;
        std::cout << "std::vector time: " << vector_time << " ms" << std::endl;
        std::cout << "Performance ratio (lower is better for StackVector): " << stack_time / vector_time << std::endl;
        
        // StackVector should be at least competitive with std::vector for small sizes
        ASSERT_LT(stack_time / vector_time, 2.0) << "StackVector should not be more than 2x slower than std::vector";
    }

    TEST(StackVectorBenchmark, RandomAccessPerformance) 
    {
        const int iterations = 100000;
        const int vector_size = 32;
        
        // Setup data
        StackVector<int, 64> sv;
        std::vector<int> v;
        
        for (int i = 0; i < vector_size; ++i) 
        {
            sv.push_back(i);
            v.push_back(i);
        }
        
        // Test StackVector random access
        PerformanceTimer stack_timer;
        volatile int sum = 0; // Prevent optimization
        for (int i = 0; i < iterations; ++i) 
        {
            for (int j = 0; j < vector_size; ++j) 
            {
                sum += sv[j];
            }
        }
        double stack_time = stack_timer.elapsed_ms();
        
        // Test std::vector random access
        PerformanceTimer vector_timer;
        sum = 0;
        for (int i = 0; i < iterations; ++i) 
        {
            for (int j = 0; j < vector_size; ++j) 
            {
                sum += v[j];
            }
        }
        double vector_time = vector_timer.elapsed_ms();
        
        std::cout << "StackVector random access time: " << stack_time << " ms" << std::endl;
        std::cout << "std::vector random access time: " << vector_time << " ms" << std::endl;
        std::cout << "Performance ratio (lower is better for StackVector): " << stack_time / vector_time << std::endl;
        
        // Random access should be very similar between both containers
        ASSERT_LT(stack_time / vector_time, 1.5) << "StackVector random access should be competitive with std::vector";
    }

    TEST(StackVectorBenchmark, IterationPerformance) 
    {
        const int iterations = 100000;
        const int vector_size = 32;
        
        // Setup data
        StackVector<int, 64> sv;
        std::vector<int> v;
        
        for (int i = 0; i < vector_size; ++i) 
        {
            sv.push_back(i);
            v.push_back(i);
        }
        
        // Test StackVector iteration
        PerformanceTimer stack_timer;
        volatile int sum = 0; // Prevent optimization
        for (int i = 0; i < iterations; ++i) 
        {
            for (const auto& element : sv) 
            {
                sum += element;
            }
        }
        double stack_time = stack_timer.elapsed_ms();
        
        // Test std::vector iteration
        PerformanceTimer vector_timer;
        sum = 0;
        for (int i = 0; i < iterations; ++i) 
        {
            for (const auto& element : v) 
            {
                sum += element;
            }
        }
        double vector_time = vector_timer.elapsed_ms();
        
        std::cout << "StackVector iteration time: " << stack_time << " ms" << std::endl;
        std::cout << "std::vector iteration time: " << vector_time << " ms" << std::endl;
        std::cout << "Performance ratio (lower is better for StackVector): " << stack_time / vector_time << std::endl;
        
        // Iteration should be very similar between both containers
        ASSERT_LT(stack_time / vector_time, 1.5) << "StackVector iteration should be competitive with std::vector";
    }

    TEST(StackVectorBenchmark, CopyPerformance) 
    {
        const int iterations = 10000;
        const int vector_size = 32;
        
        // Setup source data
        StackVector<int, 64> source_sv;
        std::vector<int> source_v;
        
        for (int i = 0; i < vector_size; ++i) 
        {
            source_sv.push_back(i);
            source_v.push_back(i);
        }
        
        // Test StackVector copy performance
        PerformanceTimer stack_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            StackVector<int, 64> copy = source_sv;
            (void)copy; // Suppress unused variable warning
        }
        double stack_time = stack_timer.elapsed_ms();
        
        // Test std::vector copy performance
        PerformanceTimer vector_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            std::vector<int> copy = source_v;
            (void)copy; // Suppress unused variable warning
        }
        double vector_time = vector_timer.elapsed_ms();
        
        std::cout << "StackVector copy time: " << stack_time << " ms" << std::endl;
        std::cout << "std::vector copy time: " << vector_time << " ms" << std::endl;
        std::cout << "Performance ratio (lower is better for StackVector): " << stack_time / vector_time << std::endl;
        
        // Stack-allocated copy should be faster than heap-allocated copy
        ASSERT_LT(stack_time, vector_time) << "StackVector copy should be faster than std::vector copy for small sizes";
    }

    TEST(StackVectorBenchmark, AllocationPerformance) 
    {
        const int iterations = 50000;
        const int vector_size = 16;
        
        // Test StackVector allocation performance
        PerformanceTimer stack_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            StackVector<int, 64> sv;
            for (int j = 0; j < vector_size; ++j) 
            {
                sv.push_back(j);
            }
            // Destructor called here
        }
        double stack_time = stack_timer.elapsed_ms();
        
        // Test std::vector allocation performance
        PerformanceTimer vector_timer;
        for (int i = 0; i < iterations; ++i) 
        {
            std::vector<int> v;
            for (int j = 0; j < vector_size; ++j) 
            {
                v.push_back(j);
            }
            // Destructor called here
        }
        double vector_time = vector_timer.elapsed_ms();
        
        std::cout << "StackVector allocation time: " << stack_time << " ms" << std::endl;
        std::cout << "std::vector allocation time: " << vector_time << " ms" << std::endl;
        std::cout << "Performance ratio (lower is better for StackVector): " << stack_time / vector_time << std::endl;
        
        // StackVector should be significantly faster for allocation/deallocation
        ASSERT_LT(stack_time, vector_time * 0.8) << "StackVector should be faster than std::vector for allocation/deallocation";
    }
}