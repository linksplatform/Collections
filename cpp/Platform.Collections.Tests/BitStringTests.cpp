namespace Platform::Collections::Tests
{
    TEST_CLASS(BitStringTests)
    {
        public: TEST_METHOD(BitGetSetTest)
        {
            inline static const std::int32_t n = 250;
            auto bitArray = BitArray(n);
            auto bitString = BitString(n);
            for (auto i = 0; i < n; i++)
            {
                auto value = RandomHelpers.Default.NextBoolean();
                bitArray.Set(i, value);
                bitString.Set(i, value);
                Assert::AreEqual(value, bitArray.Get(i));
                Assert::AreEqual(value, bitString.Get(i));
            }
        }

        public: TEST_METHOD(BitVectorNotTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorNot();
                w.Not();
            });
        }

        public: TEST_METHOD(BitParallelNotTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelNot();
                w.Not();
            });
        }

        public: TEST_METHOD(BitParallelVectorNotTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorNot();
                w.Not();
            });
        }

        public: TEST_METHOD(BitVectorAndTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorAnd(y);
                w.And(v);
            });
        }

        public: TEST_METHOD(BitParallelAndTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelAnd(y);
                w.And(v);
            });
        }

        public: TEST_METHOD(BitParallelVectorAndTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorAnd(y);
                w.And(v);
            });
        }

        public: TEST_METHOD(BitVectorOrTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorOr(y);
                w.Or(v);
            });
        }

        public: TEST_METHOD(BitParallelOrTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelOr(y);
                w.Or(v);
            });
        }

        public: TEST_METHOD(BitParallelVectorOrTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorOr(y);
                w.Or(v);
            });
        }

        public: TEST_METHOD(BitVectorXorTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorXor(y);
                w.Xor(v);
            });
        }

        public: TEST_METHOD(BitParallelXorTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelXor(y);
                w.Xor(v);
            });
        }

        public: TEST_METHOD(BitParallelVectorXorTest)
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorXor(y);
                w.Xor(v);
            });
        }

        private: static void TestToOperationsWithSameMeaning(std::function<void(BitString, BitString, BitString, BitString)> test)
        {
            inline static const std::int32_t n = 5654;
            auto x = BitString(n);
            auto y = BitString(n);
            while (x.Equals(y))
            {
                x.SetRandomBits();
                y.SetRandomBits();
            }
            auto w = BitString(x);
            auto v = BitString(y);
            Assert::IsFalse(x.Equals(y));
            Assert::IsFalse(w.Equals(v));
            Assert::IsTrue(x.Equals(w));
            Assert::IsTrue(y.Equals(v));
            test(x, y, w, v);
            Assert::IsTrue(x.Equals(w));
        }
    };
}
