#include <stack>

namespace Platform::Collections::Tests 
{
    TEST(StackTests, Concept) 
    {
        Stacks::CStack<int> auto stack = std::stack<int>{};
        stack.push(1);
        ASSERT_EQ(stack.top(), 1);
        stack.pop();
        ASSERT_TRUE(stack.empty());
    }

    struct EmptyStack : public Stacks::IStack<int>
    {
        bool empty() const {}

        void push(int item) {}

        void pop() {}

        const int &top() const {}
    };

    TEST(StackTests, Interface) 
    {
        std::unique_ptr<Stacks::IStack<int>> stack = std::make_unique<EmptyStack>();
    }
    
    TEST(StackTests, ConceptInterface) 
    {
        Stacks::CStack<int> auto stack = EmptyStack{};
    }
}
