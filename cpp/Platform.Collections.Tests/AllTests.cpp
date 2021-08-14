#include <gtest/gtest.h>
#include <Platform.Collections.h>

//#include "ArrayTests.cpp"
//#include "BitStringTests.cpp"
//#include "CharsSegmentTests.cpp"
//#include "ListTests.cpp"
//#include "FillersTests.cpp"
//#include "StringTests.cpp"
//#include "WalkersTests.cpp"


int main()
{

    using namespace Platform::Collections::Trees;

    using value_t = int;
    Node<value_t, Repeat<int>> node{};
    // node["123"]; // compile error
    node[12][123][124].Value = 123;
    std::cout << node[12][123][124].Value;
//
    const auto copy = node[12][123];
    std::cout << copy.GetChild(std::vector{124}).value().get().Value;
}