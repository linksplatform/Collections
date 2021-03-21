namespace Platform::Collections::Tests
{
    TEST_CLASS(CharsSegmentTests)
    {
        public: TEST_METHOD(GetHashCodeEqualsTest)
        {
            inline static std::string testString = "test test";
            auto testArray = testString.ToCharArray();
            auto firstHashCode = CharSegment(testArray, 0, 4).GetHashCode();
            auto secondHashCode = CharSegment(testArray, 5, 4).GetHashCode();
            Assert::AreEqual(firstHashCode, secondHashCode);
        }

        public: TEST_METHOD(EqualsTest)
        {
            inline static std::string testString = "test test";
            auto testArray = testString.ToCharArray();
            auto first = CharSegment(testArray, 0, 4);
            auto second = CharSegment(testArray, 5, 4);
            Assert::IsTrue(first.Equals(second));
        }
    };
}
