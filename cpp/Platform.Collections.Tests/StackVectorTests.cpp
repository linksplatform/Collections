#include <vector>
#include <algorithm>

namespace Platform::Collections::Tests 
{
    TEST(StackVectorTests, DefaultConstruction) 
    {
        StackVector<int> sv;
        ASSERT_TRUE(sv.empty());
        ASSERT_EQ(sv.size(), 0);
        ASSERT_TRUE(sv.is_using_stack());
        ASSERT_EQ(sv.capacity(), sv.stack_capacity());
    }

    TEST(StackVectorTests, SizeConstruction) 
    {
        StackVector<int, 10> sv(5);
        ASSERT_FALSE(sv.empty());
        ASSERT_EQ(sv.size(), 5);
        ASSERT_TRUE(sv.is_using_stack());
        
        for (size_t i = 0; i < sv.size(); ++i) 
        {
            ASSERT_EQ(sv[i], int{});
        }
    }

    TEST(StackVectorTests, ValueConstruction) 
    {
        StackVector<int, 10> sv(3, 42);
        ASSERT_EQ(sv.size(), 3);
        ASSERT_TRUE(sv.is_using_stack());
        
        for (size_t i = 0; i < sv.size(); ++i) 
        {
            ASSERT_EQ(sv[i], 42);
        }
    }

    TEST(StackVectorTests, InitializerListConstruction) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        ASSERT_EQ(sv.size(), 5);
        ASSERT_TRUE(sv.is_using_stack());
        
        for (size_t i = 0; i < sv.size(); ++i) 
        {
            ASSERT_EQ(sv[i], static_cast<int>(i + 1));
        }
    }

    TEST(StackVectorTests, IteratorConstruction) 
    {
        std::vector<int> source{10, 20, 30};
        StackVector<int, 10> sv(source.begin(), source.end());
        ASSERT_EQ(sv.size(), 3);
        ASSERT_TRUE(sv.is_using_stack());
        
        ASSERT_EQ(sv[0], 10);
        ASSERT_EQ(sv[1], 20);
        ASSERT_EQ(sv[2], 30);
    }

    TEST(StackVectorTests, CopyConstruction) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        StackVector<int, 10> sv2(sv1);
        
        ASSERT_EQ(sv1.size(), sv2.size());
        ASSERT_TRUE(sv2.is_using_stack());
        
        for (size_t i = 0; i < sv1.size(); ++i) 
        {
            ASSERT_EQ(sv1[i], sv2[i]);
        }
    }

    TEST(StackVectorTests, MoveConstruction) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        size_t original_size = sv1.size();
        
        StackVector<int, 10> sv2(std::move(sv1));
        
        ASSERT_EQ(sv2.size(), original_size);
        ASSERT_EQ(sv1.size(), 0);
        ASSERT_TRUE(sv2.is_using_stack());
        
        ASSERT_EQ(sv2[0], 1);
        ASSERT_EQ(sv2[1], 2);
        ASSERT_EQ(sv2[2], 3);
    }

    TEST(StackVectorTests, Assignment) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        StackVector<int, 10> sv2;
        
        sv2 = sv1;
        ASSERT_EQ(sv1.size(), sv2.size());
        
        for (size_t i = 0; i < sv1.size(); ++i) 
        {
            ASSERT_EQ(sv1[i], sv2[i]);
        }
    }

    TEST(StackVectorTests, MoveAssignment) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        StackVector<int, 10> sv2;
        size_t original_size = sv1.size();
        
        sv2 = std::move(sv1);
        ASSERT_EQ(sv2.size(), original_size);
        ASSERT_EQ(sv1.size(), 0);
        
        ASSERT_EQ(sv2[0], 1);
        ASSERT_EQ(sv2[1], 2);
        ASSERT_EQ(sv2[2], 3);
    }

    TEST(StackVectorTests, ElementAccess) 
    {
        StackVector<int, 10> sv{10, 20, 30};
        
        ASSERT_EQ(sv[0], 10);
        ASSERT_EQ(sv[1], 20);
        ASSERT_EQ(sv[2], 30);
        
        ASSERT_EQ(sv.at(0), 10);
        ASSERT_EQ(sv.at(1), 20);
        ASSERT_EQ(sv.at(2), 30);
        
        ASSERT_EQ(sv.front(), 10);
        ASSERT_EQ(sv.back(), 30);
        
        ASSERT_EQ(*sv.data(), 10);
    }

    TEST(StackVectorTests, ElementAccessOutOfRange) 
    {
        StackVector<int, 10> sv{10, 20, 30};
        
        ASSERT_THROW(sv.at(3), std::out_of_range);
        ASSERT_THROW(sv.at(100), std::out_of_range);
    }

    TEST(StackVectorTests, Iterators) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        
        int expected = 1;
        for (auto it = sv.begin(); it != sv.end(); ++it) 
        {
            ASSERT_EQ(*it, expected++);
        }
        
        expected = 1;
        for (auto it = sv.cbegin(); it != sv.cend(); ++it) 
        {
            ASSERT_EQ(*it, expected++);
        }
        
        expected = 5;
        for (auto it = sv.rbegin(); it != sv.rend(); ++it) 
        {
            ASSERT_EQ(*it, expected--);
        }
    }

    TEST(StackVectorTests, Capacity) 
    {
        StackVector<int, 5> sv;
        
        ASSERT_EQ(sv.capacity(), 5);
        ASSERT_TRUE(sv.empty());
        ASSERT_EQ(sv.size(), 0);
        ASSERT_LT(sv.max_size(), std::numeric_limits<size_t>::max());
    }

    TEST(StackVectorTests, PushBack) 
    {
        StackVector<int, 10> sv;
        
        sv.push_back(1);
        ASSERT_EQ(sv.size(), 1);
        ASSERT_EQ(sv[0], 1);
        ASSERT_TRUE(sv.is_using_stack());
        
        sv.push_back(2);
        ASSERT_EQ(sv.size(), 2);
        ASSERT_EQ(sv[1], 2);
        
        int value = 3;
        sv.push_back(std::move(value));
        ASSERT_EQ(sv.size(), 3);
        ASSERT_EQ(sv[2], 3);
    }

    TEST(StackVectorTests, EmplaceBack) 
    {
        StackVector<std::string, 10> sv;
        
        auto& ref = sv.emplace_back("hello");
        ASSERT_EQ(sv.size(), 1);
        ASSERT_EQ(sv[0], "hello");
        ASSERT_EQ(&ref, &sv[0]);
        
        sv.emplace_back(5, 'a');
        ASSERT_EQ(sv.size(), 2);
        ASSERT_EQ(sv[1], "aaaaa");
    }

    TEST(StackVectorTests, PopBack) 
    {
        StackVector<int, 10> sv{1, 2, 3};
        
        sv.pop_back();
        ASSERT_EQ(sv.size(), 2);
        ASSERT_EQ(sv[1], 2);
        
        sv.pop_back();
        ASSERT_EQ(sv.size(), 1);
        ASSERT_EQ(sv[0], 1);
        
        sv.pop_back();
        ASSERT_EQ(sv.size(), 0);
        ASSERT_TRUE(sv.empty());
        
        // Pop on empty should be safe (no-op)
        sv.pop_back();
        ASSERT_EQ(sv.size(), 0);
    }

    TEST(StackVectorTests, Insert) 
    {
        StackVector<int, 10> sv{1, 3, 5};
        
        auto it = sv.insert(sv.begin() + 1, 2);
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(*it, 2);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
        ASSERT_EQ(sv[3], 5);
        
        sv.insert(sv.end(), 6);
        ASSERT_EQ(sv.size(), 5);
        ASSERT_EQ(sv[4], 6);
    }

    TEST(StackVectorTests, InsertMultiple) 
    {
        StackVector<int, 10> sv{1, 5};
        
        auto it = sv.insert(sv.begin() + 1, 3, 2);
        ASSERT_EQ(sv.size(), 5);
        ASSERT_EQ(*it, 2);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 2);
        ASSERT_EQ(sv[3], 2);
        ASSERT_EQ(sv[4], 5);
    }

    TEST(StackVectorTests, InsertRange) 
    {
        StackVector<int, 10> sv{1, 4};
        std::vector<int> range{2, 3};
        
        auto it = sv.insert(sv.begin() + 1, range.begin(), range.end());
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(*it, 2);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
        ASSERT_EQ(sv[3], 4);
    }

    TEST(StackVectorTests, InsertInitializerList) 
    {
        StackVector<int, 10> sv{1, 4};
        
        auto it = sv.insert(sv.begin() + 1, {2, 3});
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(*it, 2);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
        ASSERT_EQ(sv[3], 4);
    }

    TEST(StackVectorTests, Emplace) 
    {
        StackVector<std::string, 10> sv{"hello", "world"};
        
        auto it = sv.emplace(sv.begin() + 1, 3, 'x');
        ASSERT_EQ(sv.size(), 3);
        ASSERT_EQ(*it, "xxx");
        ASSERT_EQ(sv[0], "hello");
        ASSERT_EQ(sv[1], "xxx");
        ASSERT_EQ(sv[2], "world");
    }

    TEST(StackVectorTests, Erase) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        
        auto it = sv.erase(sv.begin() + 2);
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(*it, 4);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 4);
        ASSERT_EQ(sv[3], 5);
    }

    TEST(StackVectorTests, EraseRange) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        
        auto it = sv.erase(sv.begin() + 1, sv.begin() + 4);
        ASSERT_EQ(sv.size(), 2);
        ASSERT_EQ(*it, 5);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 5);
    }

    TEST(StackVectorTests, Clear) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        
        sv.clear();
        ASSERT_EQ(sv.size(), 0);
        ASSERT_TRUE(sv.empty());
        ASSERT_TRUE(sv.is_using_stack());
    }

    TEST(StackVectorTests, Resize) 
    {
        StackVector<int, 10> sv{1, 2, 3};
        
        sv.resize(5);
        ASSERT_EQ(sv.size(), 5);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
        ASSERT_EQ(sv[3], int{});
        ASSERT_EQ(sv[4], int{});
        
        sv.resize(2);
        ASSERT_EQ(sv.size(), 2);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        
        sv.resize(4, 42);
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 42);
        ASSERT_EQ(sv[3], 42);
    }

    TEST(StackVectorTests, Reserve) 
    {
        StackVector<int, 5> sv{1, 2, 3};
        
        sv.reserve(10);
        ASSERT_EQ(sv.size(), 3);
        ASSERT_GE(sv.capacity(), 10);
        ASSERT_FALSE(sv.is_using_stack()); // Should have moved to heap
        
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
    }

    TEST(StackVectorTests, ShrinkToFit) 
    {
        StackVector<int, 5> sv{1, 2, 3};
        
        sv.reserve(10);
        ASSERT_FALSE(sv.is_using_stack());
        
        sv.shrink_to_fit();
        ASSERT_TRUE(sv.is_using_stack()); // Should have moved back to stack
        ASSERT_EQ(sv.size(), 3);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
    }

    TEST(StackVectorTests, StackToHeapTransition) 
    {
        StackVector<int, 3> sv;
        
        ASSERT_TRUE(sv.is_using_stack());
        ASSERT_EQ(sv.capacity(), 3);
        
        sv.push_back(1);
        sv.push_back(2);
        sv.push_back(3);
        ASSERT_TRUE(sv.is_using_stack());
        
        sv.push_back(4); // This should trigger heap allocation
        ASSERT_FALSE(sv.is_using_stack());
        ASSERT_GE(sv.capacity(), 4);
        
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
        ASSERT_EQ(sv[3], 4);
    }

    TEST(StackVectorTests, HeapGrowth) 
    {
        StackVector<int, 2> sv;
        
        for (int i = 0; i < 10; ++i) 
        {
            sv.push_back(i);
        }
        
        ASSERT_FALSE(sv.is_using_stack());
        ASSERT_EQ(sv.size(), 10);
        ASSERT_GE(sv.capacity(), 10);
        
        for (int i = 0; i < 10; ++i) 
        {
            ASSERT_EQ(sv[i], i);
        }
    }

    TEST(StackVectorTests, Swap) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        StackVector<int, 10> sv2{4, 5};
        
        sv1.swap(sv2);
        
        ASSERT_EQ(sv1.size(), 2);
        ASSERT_EQ(sv1[0], 4);
        ASSERT_EQ(sv1[1], 5);
        
        ASSERT_EQ(sv2.size(), 3);
        ASSERT_EQ(sv2[0], 1);
        ASSERT_EQ(sv2[1], 2);
        ASSERT_EQ(sv2[2], 3);
    }

    TEST(StackVectorTests, ComparisonOperators) 
    {
        StackVector<int, 10> sv1{1, 2, 3};
        StackVector<int, 10> sv2{1, 2, 3};
        StackVector<int, 10> sv3{1, 2, 4};
        StackVector<int, 10> sv4{1, 2};
        
        ASSERT_TRUE(sv1 == sv2);
        ASSERT_FALSE(sv1 != sv2);
        
        ASSERT_FALSE(sv1 == sv3);
        ASSERT_TRUE(sv1 != sv3);
        
        ASSERT_TRUE(sv1 < sv3);
        ASSERT_FALSE(sv3 < sv1);
        
        ASSERT_TRUE(sv4 < sv1);
        ASSERT_FALSE(sv1 < sv4);
        
        ASSERT_TRUE(sv1 <= sv2);
        ASSERT_TRUE(sv1 <= sv3);
        ASSERT_FALSE(sv3 <= sv1);
        
        ASSERT_TRUE(sv3 > sv1);
        ASSERT_FALSE(sv1 > sv3);
        
        ASSERT_TRUE(sv1 >= sv2);
        ASSERT_TRUE(sv3 >= sv1);
        ASSERT_FALSE(sv1 >= sv3);
    }

    TEST(StackVectorTests, StdAlgorithms) 
    {
        StackVector<int, 10> sv{5, 2, 8, 1, 9};
        
        std::sort(sv.begin(), sv.end());
        
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 5);
        ASSERT_EQ(sv[3], 8);
        ASSERT_EQ(sv[4], 9);
        
        auto it = std::find(sv.begin(), sv.end(), 5);
        ASSERT_NE(it, sv.end());
        ASSERT_EQ(*it, 5);
    }

    TEST(StackVectorTests, RangeBasedFor) 
    {
        StackVector<int, 10> sv{1, 2, 3, 4, 5};
        
        int expected = 1;
        for (const auto& value : sv) 
        {
            ASSERT_EQ(value, expected++);
        }
        
        for (auto& value : sv) 
        {
            value *= 2;
        }
        
        expected = 2;
        for (const auto& value : sv) 
        {
            ASSERT_EQ(value, expected);
            expected += 2;
        }
    }

    TEST(StackVectorTests, AssignMethods) 
    {
        StackVector<int, 10> sv;
        
        sv.assign(3, 42);
        ASSERT_EQ(sv.size(), 3);
        ASSERT_EQ(sv[0], 42);
        ASSERT_EQ(sv[1], 42);
        ASSERT_EQ(sv[2], 42);
        
        std::vector<int> source{10, 20, 30, 40};
        sv.assign(source.begin(), source.end());
        ASSERT_EQ(sv.size(), 4);
        ASSERT_EQ(sv[0], 10);
        ASSERT_EQ(sv[1], 20);
        ASSERT_EQ(sv[2], 30);
        ASSERT_EQ(sv[3], 40);
        
        sv.assign({1, 2, 3});
        ASSERT_EQ(sv.size(), 3);
        ASSERT_EQ(sv[0], 1);
        ASSERT_EQ(sv[1], 2);
        ASSERT_EQ(sv[2], 3);
    }
}