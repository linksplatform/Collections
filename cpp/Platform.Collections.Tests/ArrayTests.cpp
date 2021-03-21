namespace Platform::Collections::Tests
{
    TEST_CLASS(ArrayTests)
    {
        public: TEST_METHOD(GetElementTest)
        {
            auto nullArray = (std::int32_t[]){};
            Assert::AreEqual(0, nullArray.GetElementOrDefault(1));
            Assert::IsFalse(nullArray.TryGetElement(1, out std::int32_t element));
            Assert::AreEqual(0, element);
            auto array = std::int32_t[] { 1, 2, 3 };
            Assert::AreEqual(3, array.GetElementOrDefault(2));
            Assert::IsTrue(array.TryGetElement(2, out element));
            Assert::AreEqual(3, element);
            Assert::AreEqual(0, array.GetElementOrDefault(10));
            Assert::IsFalse(array.TryGetElement(10, out element));
            Assert::AreEqual(0, element);
        }
    };
}
