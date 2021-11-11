using BenchmarkDotNet.Attributes;

namespace Platform.Collections.Benchmarks
{
    /// <summary>
    /// <para>
    /// Represents the bit string benchmarks.
    /// </para>
    /// <para></para>
    /// </summary>
    [SimpleJob]
    [MemoryDiagnoser]
    public class BitStringBenchmarks
    {
        /// <summary>
        /// <para>
        /// Gets or sets the n value.
        /// </para>
        /// <para></para>
        /// </summary>
        [Params(1000, 10000, 100000, 1000000, 10000000, 100000000)]
        public int N { get; set; }
        private BitString _left;
        private BitString _right;

        /// <summary>
        /// <para>
        /// Setup this instance.
        /// </para>
        /// <para></para>
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Nots this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString Not() => new BitString(_left).Not();

        /// <summary>
        /// <para>
        /// Vectors the not.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString VectorNot() => new BitString(_left).VectorNot();

        /// <summary>
        /// <para>
        /// Parallels the not.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelNot() => new BitString(_left).ParallelNot();

        /// <summary>
        /// <para>
        /// Parallels the vector not.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelVectorNot() => new BitString(_left).ParallelVectorNot();

        /// <summary>
        /// <para>
        /// Ands this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString And() => new BitString(_left).And(_right);

        /// <summary>
        /// <para>
        /// Vectors the and.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString VectorAnd() => new BitString(_left).VectorAnd(_right);

        /// <summary>
        /// <para>
        /// Parallels the and.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelAnd() => new BitString(_left).ParallelAnd(_right);

        /// <summary>
        /// <para>
        /// Parallels the vector and.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelVectorAnd() => new BitString(_left).ParallelVectorAnd(_right);

        /// <summary>
        /// <para>
        /// Ors this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString Or() => new BitString(_left).Or(_right);

        /// <summary>
        /// <para>
        /// Vectors the or.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString VectorOr() => new BitString(_left).VectorOr(_right);

        /// <summary>
        /// <para>
        /// Parallels the or.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelOr() => new BitString(_left).ParallelOr(_right);

        /// <summary>
        /// <para>
        /// Parallels the vector or.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelVectorOr() => new BitString(_left).ParallelVectorOr(_right);

        /// <summary>
        /// <para>
        /// Xors this instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString Xor() => new BitString(_left).Xor(_right);

        /// <summary>
        /// <para>
        /// Vectors the xor.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString VectorXor() => new BitString(_left).VectorXor(_right);

        /// <summary>
        /// <para>
        /// Parallels the xor.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelXor() => new BitString(_left).ParallelXor(_right);

        /// <summary>
        /// <para>
        /// Parallels the vector xor.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The bit string</para>
        /// <para></para>
        /// </returns>
        [Benchmark]
        public BitString ParallelVectorXor() => new BitString(_left).ParallelVectorXor(_right);
    }
}
