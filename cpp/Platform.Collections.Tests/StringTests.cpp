namespace Platform::Collections::Tests
{
    TEST_CLASS(StringTests)
    {
        public: TEST_METHOD(CapitalizeFirstLetterTest)
        {
            Assert::AreEqual("Hello", "hello".CapitalizeFirstLetter());
            Assert::AreEqual("Hello", "Hello".CapitalizeFirstLetter());
            Assert::AreEqual("  Hello", "  hello".CapitalizeFirstLetter());
        }

        public: TEST_METHOD(TrimSingleTest)
        {
            Assert::AreEqual("", "'".TrimSingle('\''));
            Assert::AreEqual("", "''".TrimSingle('\''));
            Assert::AreEqual("hello", "'hello'".TrimSingle('\''));
            Assert::AreEqual("hello", "hello'".TrimSingle('\''));
            Assert::AreEqual("hello", "'hello".TrimSingle('\''));
        }
    };
}
