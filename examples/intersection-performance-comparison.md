# HashSet vs BitString Intersection Performance Comparison

This document describes the performance comparison benchmarks implemented to address [Issue #52](https://github.com/linksplatform/Collections/issues/52) - "Compare HashSet and BitString intersection performance".

## Implementation Overview

A comprehensive benchmark suite has been added to compare the intersection performance between `HashSet<int>` and `BitString` collections under various scenarios.

## Benchmark Class: `IntersectionPerformanceComparison`

Located in: `csharp/Platform.Collections.Benchmarks/IntersectionPerformanceComparison.cs`

### Test Parameters

The benchmark tests different combinations of:

- **Collection Size (N)**: 1,000, 10,000, 100,000, 1,000,000 elements
- **Fill Rate**: 0.1 (sparse), 0.5 (medium), 0.9 (dense)  
- **Intersection Rate**: 0.1 (low overlap), 0.3 (medium overlap), 0.7 (high overlap)

### Benchmark Methods

#### HashSet Operations
- `HashSetIntersection()` - Standard HashSet.IntersectWith() method (baseline)
- `HashSetIntersectionCount()` - Count intersection elements using LINQ
- `HashSetHaveCommon()` - Check if sets overlap using Overlaps() method

#### BitString Operations  
- `BitStringIntersection()` - BitString.And() with GetSetIndices()
- `BitStringVectorIntersection()` - BitString.VectorAnd() with GetSetIndices()
- `BitStringParallelIntersection()` - BitString.ParallelAnd() with GetSetIndices()
- `BitStringParallelVectorIntersection()` - BitString.ParallelVectorAnd() with GetSetIndices()
- `BitStringGetCommonIndices()` - Direct GetCommonIndices() method
- `BitStringCountCommonBits()` - Count intersection elements
- `BitStringHaveCommonBits()` - Check if bitstrings have common bits

## Running the Benchmarks

### Prerequisites

- .NET 8.0 SDK
- BenchmarkDotNet package (already included)

### Commands

```bash
# Navigate to the benchmark project
cd csharp/Platform.Collections.Benchmarks

# Run intersection performance comparison
dotnet run intersection

# Run original BitString benchmarks  
dotnet run bitstring

# Show help
dotnet run
```

### Sample Benchmark Execution

```bash
dotnet run -- --configuration Release intersection
```

This will run the full benchmark suite with all parameter combinations, testing:
- 4 collection sizes × 3 fill rates × 3 intersection rates = 36 parameter combinations
- 11 different intersection methods per combination
- Total: 396 individual benchmark runs

## Expected Results Analysis

### When BitString Should Perform Better
- **Dense collections** (high fill rate): BitString uses compact bit operations
- **Large collections**: Vectorized and parallel operations provide advantages
- **Simple existence checks**: `HaveCommonBits()` vs `Overlaps()` should be faster
- **Counting operations**: Bit counting can be more efficient than enumeration

### When HashSet Should Perform Better  
- **Sparse collections** (low fill rate): Less memory overhead and faster iteration
- **Small collections**: Lower setup overhead
- **Complex intersection results**: Direct set operations may be more efficient

### Performance Factors Tested

1. **Memory usage**: BitString has fixed memory based on max index, HashSet varies by element count
2. **CPU utilization**: BitString can leverage SIMD and parallelization
3. **Cache efficiency**: Compact bit representation vs pointer-based hash table
4. **Algorithmic complexity**: O(n) bit operations vs O(n+m) set operations

## Usage Examples

### Basic BitString Intersection
```csharp
var left = new BitString(1000);
var right = new BitString(1000);

// Setup bits...
left.Set(5); left.Set(10); left.Set(15);
right.Set(10); right.Set(15); right.Set(20);

// Intersection using And operation
var result = new BitString(left);
result.And(right);
var commonIndices = result.GetSetIndices(); // [10, 15]

// Or using direct method  
var commonIndices2 = left.GetCommonIndices(right); // [10, 15]
```

### HashSet Intersection
```csharp
var left = new HashSet<int> { 5, 10, 15 };
var right = new HashSet<int> { 10, 15, 20 };

// Standard intersection
var result = new HashSet<int>(left);
result.IntersectWith(right); // {10, 15}

// Count intersection
int count = left.Intersect(right).Count(); // 2

// Check overlap
bool hasCommon = left.Overlaps(right); // true
```

## Key Insights

This benchmark suite enables empirical comparison of:
- Raw intersection performance across different data characteristics
- Memory efficiency for different sparsity patterns
- Scalability of parallel vs sequential approaches
- Effectiveness of hardware acceleration (SIMD) for bit operations

The results will help determine optimal collection choice based on specific use case requirements.