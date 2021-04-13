//#include <bits/stdc++.h> // sorry i'm gcc-fan
#include "../Platform.Collections/Platform.Collections.h"

using namespace Platform::Collections;
using namespace Platform::Collections::System;
using namespace Platform::Collections::Arrays;
using namespace Platform::Collections::Lists;
using namespace Platform::Collections::Segments;
using namespace Platform::Collections::Segments::Walkers;
using namespace Platform::Collections::Stacks;
//using namespace Platform::Collections::Trees;

#include <chrono>
#include <codecvt>
#include <locale>


struct timer
{
    std::chrono::system_clock::time_point time{};
    std::string message;

    explicit timer(std::string&& message = "") : message(message), time(std::chrono::system_clock::now())
    {
    }

    ~timer()
    {
        auto curTime = std::chrono::system_clock::now();
        auto duration = std::chrono::duration<double>(curTime - time);

        std::cout << "timeline '" + message + "' :  " << duration.count() << "ms" << std::endl;
    }
};


void ShiftRight_Benchmark()
{
    timer global_t("ShiftRight");
    using namespace Platform::Collections::Arrays;

    // TODO пока используйте 'vector' вместо 'array'
    std::vector<int> array{1, 7, 7, 0, 3, 3};

    {
        timer t("new_array_1");
        auto new_array = GenericArrayExtensions::ShiftRight<int>(array, 100000000);
        new_array = new_array;
    }

    {
        timer t("new_array_2");
        std::vector<int>& new_array = array;
        for(int i = 0; i < 10000; i++)
        {
            new_array = GenericArrayExtensions::ShiftRight<int>(new_array);
        }
        new_array = new_array;
    }
}

void GenericArrayExtensions_Test()
{
    using namespace Platform::Collections::Arrays;
    std::vector<int> a{2, 2, 8, 0, 3, 3};
    std::vector<int> b{1, 7, 7};
    int64_t position = 0;

    GenericArrayExtensions::AddAll<int>(a, position, b);

    for(auto it : a)
    {
        std::cout << it << " ";
    }
}

void Node_Test()
{

}

void ArrayFiller_Test()
{
    std::vector<int> a{1, 7, 7, 0, 1, 3, -100};

    {
        ArrayFiller<int> filler(a);
        filler.Add(2);
        filler.Add(2);
        filler.Add(8);
        filler.Add(1);
        filler.Add(3);
        filler.Add(3);
        filler.Add(7);
    }

    {
        ArrayFiller<int> filler(a);
        filler.AddSkipFirstAndReturnTrue(std::vector<int>{7, 3, 1});
        filler.AddAllAndReturnTrue(std::array<int, 5>{0, 7, 7, 1, -177013});
    }


    for(auto it : a)
    {
        std::cout << it << " ";
    }

};


template<typename TElement>
void BadTemplate_Test_Support(Array <TElement> auto& array)
{
    std::cout << "size: " << array.size() << std::endl;
    std::cout << "elements: ";
    for(auto it : array)
    {
        if constexpr(std::same_as<TElement, std::string>)
        {
            std::cout << '"';
            std::cout << it << '"' << " ";
        }
        else
        {
            std::cout << it << " ";
        }
    }
    std::cout << std::endl;
}

template<typename TArray, typename TElement>
requires Array<TArray, TElement>
void VeryBadTemplate_Test_Support(TArray& array)
{
    std::cout << "size: " << array.size() << std::endl;
    std::cout << "elements: ";
    for(auto it : array)
    {
        if constexpr(std::same_as<TElement, std::string>)
        {
            std::cout << '"';
            std::cout << it << '"' << " ";
        }
        else
        {
            std::cout << it << " ";
        }

    }
    std::cout << std::endl;
}


void Template_Test()
{
    using namespace Platform::Collections::Arrays;
    std::vector<int> a{1, 2, 3};
    std::vector <std::string> b{"1", "2", "3"};

    a = GenericArrayExtensions::ShiftRight<int>(a);
    b = GenericArrayExtensions::ShiftRight<std::string>(b);

    BadTemplate_Test_Support<int>(a);
    BadTemplate_Test_Support<std::string>(b);
    //VeryBadTemplate_Test_Support<vector<int>, int>(a);
}


void ListCompare_Benchmark()
{
    timer t("ListCompare_Benchmark");

    srand(time(nullptr));

    int size = 10000;
    int count = 10000;

    std::vector<int> a(size);
    std::vector<int> b(size);

    for(int i = 0; i < size; i++)
    {
        a[i] = rand();
        b[i] = a[i];
    }

    for(int i = 0; i < count; i++)
    {
        //auto order1 = IListExtensions::CompareTo<int>(a, b);
        auto order1 = a == b;
        order1 = order1;
    }
}

void ListSort_Test()
{
    std::vector<std::vector<int>> a{{1, 3, 3, 7, 2, 2, 8, 6, 9},
                                    {2, 2, 2, 2, 2, 2, 2, 2, 2},
                                    {1, 2, 3, 4, 5, 6, 7, 8, 9},
                                    {9, 0, 0, 0, 0, 0, 0, 0, 9},
                                    {4, 8, 3, 2, 2, 2, 2, 4, 9},
                                    {6, 2, 3, 4, 4, 6, 7, 8, 9},
                                    {4, 2, 3, 3, 5, 6, 1, 3, 9}};

    std::ranges::sort(a);

    for(const auto& i : a)
    {
        for(auto j : i)
        {
            std::cout << j << " ";
        }
        std::cout << std::endl;
    }
}

void Span_Test()
{
    std::cout << "Is enumerable: " << IEnumerable < std::span<int>> << std::endl;
    std::cout << "Is array: " << Array < std::span<int>, int > << std::endl;
    std::cout << "Is list: " << IList < std::span<int>, int > << std::endl;
}


void ListFiller_Test()
{
    std::vector<int> a;
    auto filler = ListFiller<int, std::string, std::vector<int>>
    (a, "socks");

    filler.Add(1);
    filler.Add(3);
    filler.Add(3);
    filler.Add(7);

    std::cout << filler.AddAndReturnConstant(2) << std::endl;
    std::cout << filler.AddAllAndReturnConstant(std::vector{2, 8}) << std::endl;

    for(auto it : a)
    {
        std::cout << it << ' ';
    }
}


void StringExtensions_Test()
{
    std::u16string s = u"five cocks and one hen";

    s = StringExtensions::CapitalizeFirstLetter(s);
    s = StringExtensions::Truncate(s, 12);
    s = StringExtensions::TrimSingle(s, 'a');
    s = StringExtensions::TrimSingle(s, ' ');

    std::wstring_convert<std::codecvt_utf8_utf16<char16_t>, char16_t> decoder;

    std::cout << "result: " << decoder.to_bytes(s);
}

void unordered_map_Any_Test()
{
    std::unordered_map<std::any, std::string> dictionary;
    dictionary[0] = "i";
    dictionary[1] = "l";
    dictionary[2] = "o";
    dictionary[3] = "v";
    dictionary[4] = "e";
    dictionary["5"] = "c";
    dictionary["6"] = "o";
    dictionary["7"] = "c";
    dictionary["8"] = "k";

    for(const auto& it : dictionary)
    {
        std::cout << it.first.type().name() << " " << it.second << std::endl;
    }
}

int main()
{
    Platform::Collections::Trees::Node<int, Platform::Collections::Trees::Repeat<std::any>> root;
    root["1"]["2"]["3"]["4"]["5"]["6"].Value = 1337;
    std::cout << root["1"]["2"]["3"]["4"]["5"]["6"].Value << std::endl;
}



