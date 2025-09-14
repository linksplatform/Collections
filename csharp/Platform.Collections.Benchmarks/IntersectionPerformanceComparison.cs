using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Random;

namespace Platform.Collections.Benchmarks
{
    /// <summary>
    /// Performance comparison between HashSet and BitString intersection operations.
    /// Tests various scenarios: sparse vs dense sets, different sizes and intersection rates.
    /// </summary>
    [SimpleJob]
    [MemoryDiagnoser]
    public class IntersectionPerformanceComparison
    {
        [Params(1000, 10000, 100000, 1000000)]
        public int N { get; set; }

        [Params(0.1, 0.5, 0.9)]
        public double FillRate { get; set; }

        [Params(0.1, 0.3, 0.7)]
        public double IntersectionRate { get; set; }

        private BitString _bitStringLeft;
        private BitString _bitStringRight;
        private HashSet<int> _hashSetLeft;
        private HashSet<int> _hashSetRight;

        [GlobalSetup]
        public void Setup()
        {
            var random = RandomHelpers.Default;
            
            // Setup BitString
            _bitStringLeft = new BitString(N);
            _bitStringRight = new BitString(N);
            
            // Setup HashSet
            _hashSetLeft = new HashSet<int>();
            _hashSetRight = new HashSet<int>();
            
            // Fill left collections
            var leftBits = new List<int>();
            for (int i = 0; i < N; i++)
            {
                if (random.NextDouble() < FillRate)
                {
                    _bitStringLeft.Set(i);
                    _hashSetLeft.Add(i);
                    leftBits.Add(i);
                }
            }
            
            // Fill right collections with controlled intersection
            var intersectionSize = (int)(leftBits.Count * IntersectionRate);
            var rightOnlySize = (int)(leftBits.Count * FillRate) - intersectionSize;
            
            // Add intersection elements
            var intersectionIndices = leftBits.Take(intersectionSize).ToList();
            foreach (var index in intersectionIndices)
            {
                _bitStringRight.Set(index);
                _hashSetRight.Add(index);
            }
            
            // Add right-only elements
            int rightOnlyAdded = 0;
            for (int i = 0; i < N && rightOnlyAdded < rightOnlySize; i++)
            {
                if (!leftBits.Contains(i) && random.NextDouble() < FillRate)
                {
                    _bitStringRight.Set(i);
                    _hashSetRight.Add(i);
                    rightOnlyAdded++;
                }
            }
        }

        [Benchmark(Baseline = true)]
        public HashSet<int> HashSetIntersection()
        {
            var result = new HashSet<int>(_hashSetLeft);
            result.IntersectWith(_hashSetRight);
            return result;
        }

        [Benchmark]
        public List<long> BitStringIntersection()
        {
            var leftCopy = new BitString(_bitStringLeft);
            leftCopy.And(_bitStringRight);
            return leftCopy.GetSetIndices();
        }

        [Benchmark]
        public List<long> BitStringVectorIntersection()
        {
            var leftCopy = new BitString(_bitStringLeft);
            leftCopy.VectorAnd(_bitStringRight);
            return leftCopy.GetSetIndices();
        }

        [Benchmark]
        public List<long> BitStringParallelIntersection()
        {
            var leftCopy = new BitString(_bitStringLeft);
            leftCopy.ParallelAnd(_bitStringRight);
            return leftCopy.GetSetIndices();
        }

        [Benchmark]
        public List<long> BitStringParallelVectorIntersection()
        {
            var leftCopy = new BitString(_bitStringLeft);
            leftCopy.ParallelVectorAnd(_bitStringRight);
            return leftCopy.GetSetIndices();
        }

        [Benchmark]
        public List<long> BitStringGetCommonIndices()
        {
            return _bitStringLeft.GetCommonIndices(_bitStringRight);
        }

        [Benchmark]
        public long BitStringCountCommonBits()
        {
            return _bitStringLeft.CountCommonBits(_bitStringRight);
        }

        [Benchmark]
        public bool BitStringHaveCommonBits()
        {
            return _bitStringLeft.HaveCommonBits(_bitStringRight);
        }

        [Benchmark]
        public int HashSetIntersectionCount()
        {
            return _hashSetLeft.Intersect(_hashSetRight).Count();
        }

        [Benchmark]
        public bool HashSetHaveCommon()
        {
            return _hashSetLeft.Overlaps(_hashSetRight);
        }
    }
}