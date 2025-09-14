using BenchmarkDotNet.Attributes;
using Platform.Collections.Segments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Collections.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class SegmentBenchmarks
    {
        [Params(10, 100, 1000, 10000)]
        public int Length { get; set; }

        private char[] _data = null!;
        private CharSegment _charSegment = null!;
        private Segment<char> _segment = null!;
        private string _testString = null!;

        [GlobalSetup]
        public void Setup()
        {
            var random = new System.Random(42); // Fixed seed for reproducible results
            _data = new char[Length * 2]; // Make it larger to test different offsets
            
            // Fill with random characters
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = (char)('a' + random.Next(26));
            }

            int offset = Length / 4; // Use some offset to test realistic scenarios
            
            _charSegment = new CharSegment(_data, offset, Length);
            _segment = new Segment<char>(_data, offset, Length);
            _testString = new string(_data, offset, Length);
        }

        [Benchmark]
        public int CharSegmentGetHashCode() => _charSegment.GetHashCode();

        [Benchmark]
        public int SegmentGetHashCode() => _segment.GetHashCode();

        [Benchmark]
        public bool CharSegmentEqualsItself() => _charSegment.Equals(_charSegment);

        [Benchmark]
        public bool SegmentEqualsItself() => _segment.Equals(_segment);

        [Benchmark]
        public bool CharSegmentEqualsOther()
        {
            var other = new CharSegment(_data, _charSegment.Offset, _charSegment.Length);
            return _charSegment.Equals(other);
        }

        [Benchmark]
        public bool SegmentEqualsOther()
        {
            var other = new Segment<char>(_data, _segment.Offset, _segment.Length);
            return _segment.Equals(other);
        }

        [Benchmark]
        public char CharSegmentIndexAccess()
        {
            char result = '\0';
            for (int i = 0; i < _charSegment.Count; i++)
            {
                result ^= _charSegment[i];
            }
            return result;
        }

        [Benchmark]
        public char SegmentIndexAccess()
        {
            char result = '\0';
            for (int i = 0; i < _segment.Count; i++)
            {
                result ^= _segment[i];
            }
            return result;
        }

        [Benchmark]
        public string CharSegmentToString() => _charSegment.ToString();

        [Benchmark]
        public string SegmentToString() => _segment.ToString() ?? string.Empty;

        [Benchmark]
        public string CharSegmentImplicitConversion() => _charSegment;

        [Benchmark]
        public bool CharSegmentContains()
        {
            char searchChar = _data[_charSegment.Offset + _charSegment.Length / 2];
            return _charSegment.Contains(searchChar);
        }

        [Benchmark]
        public bool SegmentContains()
        {
            char searchChar = _data[_segment.Offset + _segment.Length / 2];
            return _segment.Contains(searchChar);
        }

        [Benchmark]
        public int CharSegmentIndexOf()
        {
            char searchChar = _data[_charSegment.Offset + _charSegment.Length / 2];
            return _charSegment.IndexOf(searchChar);
        }

        [Benchmark]
        public int SegmentIndexOf()
        {
            char searchChar = _data[_segment.Offset + _segment.Length / 2];
            return _segment.IndexOf(searchChar);
        }

        [Benchmark]
        public char[] CharSegmentToArray() => _charSegment.ToArray();

        [Benchmark]
        public char[] SegmentToArray() => _segment.ToArray();

        [Benchmark]
        public List<char> CharSegmentToList() => _charSegment.ToList();

        [Benchmark]
        public List<char> SegmentToList() => _segment.ToList();

        [Benchmark]
        public void CharSegmentForeachIteration()
        {
            foreach (var c in _charSegment)
            {
                // Empty loop to measure iteration overhead
            }
        }

        [Benchmark]
        public void SegmentForeachIteration()
        {
            foreach (var c in _segment)
            {
                // Empty loop to measure iteration overhead
            }
        }
    }
}