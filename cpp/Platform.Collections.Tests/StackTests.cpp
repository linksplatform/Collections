#include <stack>

namespace Platform::Collections::Tests 
{
struct EmptyStack : public Stacks::IStack<int> 
{
    bool empty() const {}

    void push(int item) {}

    void pop() {}

    int &top() {}

    const int &top() const {}
};

TEST(StackTests, Concept) 
{
    Stacks::CStack<int> auto stack = std::stack<int>{};
    stack.push(1);
    ASSERT_EQ(stack.top(), 1);
    stack.pop();
    ASSERT_TRUE(stack.empty());
}

TEST(StackTests, Interface) {}
TEST(StackTests, ConceptInterface) {}
} // namespace Platform::Collections::Tests
