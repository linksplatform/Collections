namespace Platform::Collections::Tests
{
    TEST_CLASS(ListTests)
    {
        public: TEST_METHOD(GetElementTest)
        {
            auto nullList = (IList<std::int32_t>){};
            Assert::AreEqual(0, nullList.GetElementOrDefault(1));
            Assert::IsFalse(nullList.TryGetElement(1, out std::int32_t element));
            Assert::AreEqual(0, element);
            auto list = List<std::int32_t>() { 1, 2, 3 };
            Assert::AreEqual(3, list.GetElementOrDefault(2));
            Assert::IsTrue(list.TryGetElement(2, out element));
            Assert::AreEqual(3, element);
            Assert::AreEqual(0, list.GetElementOrDefault(10));
            Assert::IsFalse(list.TryGetElement(10, out element));
            Assert::AreEqual(0, element);
        }
    };
}
