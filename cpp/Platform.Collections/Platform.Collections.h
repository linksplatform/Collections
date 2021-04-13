#ifndef PLATFORM_COLLECTIONS_H
#define PLATFORM_COLLECTIONS_H

#include <type_traits>
#include <concepts>
#include <vector>
#include <variant>
#include <string>
#include <array>
#include <bitset>
#include <queue>
#include <map>
#include <map>
#include <set>
#include <memory>
#include <ranges>
#include <algorithm>
#include <cmath>
#include <span>
#include <iostream>
#include <functional>
#include <thread>
#include <any>

//#include <Platform.Exceptions.h>

#include <Platform.Hashing.h>
#include <Platform.Equality.h>

#include ".Concepts/BaseConcepts.h"

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
#include "Segments/Walkers/AllSegmentsWalkerBase[T, TSegment].h"
#include "Segments/Walkers/AllSegmentsWalkerBase[T].h"
#include "Segments/Walkers/AllSegmentsWalkerExtensions.h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase[T, TSegment].h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase[T].h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase[T, Segment].h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase[T].h"
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

//#include "EnsureExtensions.h"

#include "ICollectionExtensions.h"
#include "IDictionaryExtensions.h"

#include "Trees/Node.h"





#endif //PLATFORM_COLLECTIONS_H
