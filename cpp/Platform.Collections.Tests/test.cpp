#include <bits/stdc++.h>
#include "../Platform.Collections/Platform.Collections.h"

using namespace std;

struct timer {
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
    using namespace Platform::Collections::Arrays;

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


template <typename TElement>
void BadTemplate_Test_Support(Array<TElement> auto& array) {
    cout << "size: " << array.size() << endl;
    cout << "elements: ";
    for(auto it : array) {
        if constexpr(same_as<TElement, string>) {
            cout << '"';
            cout << it << '"' << " ";
        }
        else {
            cout << it << " ";
        }
    }
    cout << endl;
}

template <typename TArray, typename TElement> requires Array<TArray, TElement>
void VeryBadTemplate_Test_Support(TArray& array) {
    cout << "size: " << array.size() << endl;
    cout << "elements: ";
    for(auto it : array) {
        if constexpr(same_as<TElement, string>) {
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
    //VeryBadTemplate_Test_Support<array<int, 3>, int>(a);
}



int main() {
    //Template_Test();



}


