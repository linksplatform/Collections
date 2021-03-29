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

    //TODO пока используйте 'vector' вместо 'array'
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


int main() {
    //ShiftRight_Benchmark();
}