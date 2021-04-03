//#include <bits/stdc++.h> // sorry i'm gcc-fan
#include "../Platform.Collections/Platform.Collections.h"

using namespace Platform::Collections;
using namespace System;
using namespace Arrays;
using namespace Lists;
using namespace Segments;
using namespace Segments::Walkers;
using namespace Stacks;
using namespace Trees;



#include <chrono>

using namespace std;

struct timer{
    chrono::system_clock::time_point time{};
    string message;

    explicit timer(string&& message = "") : message(message), time(chrono::system_clock::now()) {}

    ~timer() {
        auto curTime = std::chrono::system_clock::now();
        auto duration = std::chrono::duration<double>(curTime - time);

        cout << "timeline '" + message + "' :  " << duration.count() << "ms" << endl;
    }
};


void ShiftRight_Benchmark() {
    timer global_t("ShiftRight");
    using namespace Platform::Collections::Arrays;

    // TODO пока используйте 'vector' вместо 'array'
    vector<int> array{1, 7, 7, 0, 3, 3};

    {
        timer t("new_array_1");
        auto new_array = GenericArrayExtensions::ShiftRight<int>(array, 100000000);
        new_array = new_array;
    }

    {
        timer t("new_array_2");
        vector<int>& new_array = array;
        for(int i = 0; i < 10000; i++) {
            new_array = GenericArrayExtensions::ShiftRight<int>(new_array);
        }
        new_array = new_array;
    }
}

void GenericArrayExtensions_Test() {
    using namespace Platform::Collections::Arrays;
    vector<int> a{2, 2, 8, 0, 3, 3};
    vector<int> b{1, 7, 7};
    int64_t position = 0;

    GenericArrayExtensions::AddAll<int>(a, position, b);

    for(auto it : a) {
        cout << it << " ";
    }
}

void Node_Test() {

}

void ArrayFiller_Test() {
    //using namespace Platform::Collections::Arrays;

    vector<int> a{1, 7, 7, 0, 1, 3, -100};

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
        filler.AddSkipFirstAndReturnTrue(vector<int>{7, 3, 1});
        filler.AddAllAndReturnTrue(array<int, 5>{0, 7, 7, 1, -177013});
    }


    for(auto it : a) {
        cout << it << " ";
    }

};


template<typename TElement>
void BadTemplate_Test_Support(Array <TElement> auto& array) {
    cout << "size: " << array.size() << endl;
    cout << "elements: ";
    for(auto it : array) {
        if constexpr(same_as < TElement, string >) {
            cout << '"';
            cout << it << '"' << " ";
        }
        else {
            cout << it << " ";
        }
    }
    cout << endl;
}

template<typename TArray, typename TElement>
requires Array<TArray, TElement>
void VeryBadTemplate_Test_Support(TArray& array) {
    cout << "size: " << array.size() << endl;
    cout << "elements: ";
    for(auto it : array) {
        if constexpr(same_as < TElement, string >) {
            cout << '"';
            cout << it << '"' << " ";
        }
        else {
            cout << it << " ";
        }

    }
    cout << endl;
}


void Template_Test() {
    using namespace Platform::Collections::Arrays;
    vector<int> a{1, 2, 3};
    vector<string> b{"1", "2", "3"};

    a = GenericArrayExtensions::ShiftRight<int>(a);
    b = GenericArrayExtensions::ShiftRight<string>(b);

    BadTemplate_Test_Support<int>(a);
    BadTemplate_Test_Support<string>(b);
    //VeryBadTemplate_Test_Support<vector<int>, int>(a);
}


void ListCompare_Benchmark() {
    timer t("ListCompare_Benchmark");

    srand(time(nullptr));

    int size = 10000;
    int count = 10000;

    vector<int> a(size);
    vector<int> b(size);

    for(int i = 0; i < size; i++) {
        a[i] = rand();
        b[i] = a[i];
    }

    for(int i = 0; i < count; i++) {
        //auto order1 = IListExtensions::CompareTo<int>(a, b);
        auto order1 = a == b;
        order1 = order1;
    }
}

void ListSort_Test() {
    vector<vector<int>> a{{1, 3, 3, 7, 2, 2, 8, 6, 9},
                          {2, 2, 2, 2, 2, 2, 2, 2, 2},
                          {1, 2, 3, 4, 5, 6, 7, 8, 9},
                          {9, 0, 0, 0, 0, 0, 0, 0, 9},
                          {4, 8, 3, 2, 2, 2, 2, 4, 9},
                          {6, 2, 3, 4, 4, 6, 7, 8, 9},
                          {4, 2, 3, 3, 5, 6, 1, 3, 9}};

    std::ranges::sort(a);

    for(const auto& i : a) {
        for(auto j : i) {
            cout << j << " ";
        }
        cout << endl;
    }
}

void Span_Test() {
    cout << "Is enumerable: " << IEnumerable<std::span<int>> << endl;
    cout << "Is array: " << Array<std::span<int>, int> << endl;
    cout << "Is list: " << IList<std::span<int>, int> << endl;
}


void ListFiller_Test() {
    vector<int> a;
    auto filler = ListFiller<int, string, vector<int>>(a, "socks");

    filler.Add(1);
    filler.Add(3);
    filler.Add(3);
    filler.Add(7);

    cout << filler.AddAndReturnConstant(2) << endl;
    cout << filler.AddAllAndReturnConstant(vector{2, 8}) << endl;

    for(auto it : a) {
        cout << it << ' ';
    }
}


void StringExtensions_Test() {
    string s = "five cocks and one hen";

    s = StringExtensions::CapitalizeFirstLetter(s);
    s = StringExtensions::Truncate(s, 12);
    s = StringExtensions::TrimSingle(s, 'a');
    s = StringExtensions::TrimSingle(s, ' ');

    cout << s;
}


int main()
{
    StringExtensions_Test();
}
