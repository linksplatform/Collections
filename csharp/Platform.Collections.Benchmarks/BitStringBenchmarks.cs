using BenchmarkDotNet.Attributes;

namespace Platform.Collections.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class BitStringBenchmarks
    {
        [Params(1000, 10000, 100000, 1000000, 10000000, 100000000)]
        public int N { get; set; }
        private BitString _left;
        private BitString _right;

        [GlobalSetup]
        public void Setup()
        {
            _left = new BitString(N);
            _right = new BitString(N);
            while (_left.Equals(_right))
            {
                _left.SetRandomBits();
                _right.SetRandomBits();
            }
        }

        [Benchmark]
        public BitString Not() => new BitString(_left).Not();

        [Benchmark]
        public BitString VectorNot() => new BitString(_left).VectorNot();

        [Benchmark]
        public BitString ParallelNot() => new BitString(_left).ParallelNot();

        [Benchmark]
        public BitString ParallelVectorNot() => new BitString(_left).ParallelVectorNot();

        [Benchmark]
        public BitString And() => new BitString(_left).And(_right);

        [Benchmark]
        public BitString VectorAnd() => new BitString(_left).VectorAnd(_right);

        [Benchmark]
        public BitString ParallelAnd() => new BitString(_left).ParallelAnd(_right);

        [Benchmark]
        public BitString ParallelVectorAnd() => new BitString(_left).ParallelVectorAnd(_right);

        [Benchmark]
        public BitString Or() => new BitString(_left).Or(_right);

        [Benchmark]
        public BitString VectorOr() => new BitString(_left).VectorOr(_right);

        [Benchmark]
        public BitString ParallelOr() => new BitString(_left).ParallelOr(_right);

        [Benchmark]
        public BitString ParallelVectorOr() => new BitString(_left).ParallelVectorOr(_right);

        [Benchmark]
        public BitString Xor() => new BitString(_left).Xor(_right);

        [Benchmark]
        public BitString VectorXor() => new BitString(_left).VectorXor(_right);

        [Benchmark]
        public BitString ParallelXor() => new BitString(_left).ParallelXor(_right);

        [Benchmark]
        public BitString ParallelVectorXor() => new BitString(_left).ParallelVectorXor(_right);

        [Benchmark]
        public BitString IntrinsicsNot() => new BitString(_left).IntrinsicsNot();

        [Benchmark]
        public BitString ParallelIntrinsicsNot() => new BitString(_left).ParallelIntrinsicsNot();

        [Benchmark]
        public BitString IntrinsicsAnd() => new BitString(_left).IntrinsicsAnd(_right);

        [Benchmark]
        public BitString ParallelIntrinsicsAnd() => new BitString(_left).ParallelIntrinsicsAnd(_right);

        [Benchmark]
        public BitString IntrinsicsOr() => new BitString(_left).IntrinsicsOr(_right);

        [Benchmark]
        public BitString ParallelIntrinsicsOr() => new BitString(_left).ParallelIntrinsicsOr(_right);

        [Benchmark]
        public BitString IntrinsicsXor() => new BitString(_left).IntrinsicsXor(_right);

        [Benchmark]
        public BitString ParallelIntrinsicsXor() => new BitString(_left).ParallelIntrinsicsXor(_right);
    }
}
