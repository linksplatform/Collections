#pragma once

#include <gsl/gsl>

#include <type_traits>
#include <vector>
#include <string>
#include <array>
#include <bitset>
#include <set>
#include <algorithm>
#include <cmath>
#include <span>
#include <iostream>
#include <numeric>

#include <Platform.Exceptions.h>
#include <Platform.Interfaces.h>
#include <Platform.Equality.h>
#include <Platform.Hashing.h>
#include <Platform.Random.h>

#include "Arrays/GenericArrayExtensions.h"
#include "Arrays/ArrayFiller[TElement].h"
#include "Arrays/ArrayFiller[TElement, TReturnConstant].h"
#include "Arrays/ArrayPool.h"
#include "Arrays/ArrayPool[T].h"
#include "Arrays/ArrayString.h"
#include "Arrays/CharArrayExtensions.h"

#include "Concurrent/ConcurrentQueueExtensions.h"
#include "Concurrent/ConcurrentStackExtensions.h"

#include "Lists/CharIListExtensions.h"
#include "Lists/IListComparer.h"
#include "Lists/IListEqualityComparer.h"
#include "Lists/IListExtensions.h"
#include "Lists/ListFiller.h"

#include "Segments/Walkers/AllSegmentsWalkerBase.h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase.h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase.h"

#include "Segments/Segment.h"
#include "Segments/CharSegment.h"

#include "Sets/ISetExtensions.h"
#include "Sets/SetFiller.h"

#include "Stacks/DefaultStack.h"
#include "Stacks/IStack.h"
#include "Stacks/IStackExtensions.h"
#include "Stacks/IStackFactory.h"
#include "Stacks/StackExtensions.h"

#include "BitString.h"
#include "BitStringExtensions.h"
#include "StringExtensions.h"

#include "EnsureExtensions.h"

#include "ICollectionExtensions.h"
#include "IDictionaryExtensions.h"

#include "Trees/Node.h"
