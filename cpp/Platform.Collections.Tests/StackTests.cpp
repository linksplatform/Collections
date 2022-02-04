namespace Platform::Collections::Tests {
TEST(StackTests, Concept) {
  Stacks::CStack<int> auto stack = std::stack<int>{};
  stack.push(1);
  ASSERT_EQ(stack.top(), 1);
  stack.pop();
  ASSERT_TRUE(stack.empty());
}
TEST(StackTests, Interface) {
  struct EmptyStack : public Stacks::IStack<int> {
    bool empty() const {}

    void push(int item) {}

    void pop() {}

    int &top() {}

    const int &top() const {}
  };
}
} // namespace Platform::Collections::Tests