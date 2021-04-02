#ifndef COLLECTIONS_PLATFORM_COLLECTIONS_H
#define COLLECTIONS_PLATFORM_COLLECTIONS_H

#include <type_traits>
#include <concepts>
#include <vector>
#include <string>
#include <array>
#include <bitset>
#include <queue>
#include <map>
#include <set>
#include <memory>
#include <ranges>
#include <algorithm>
#include <cmath>
#include <span>
#include <iostream>
#include <functional>
#include <coroutine>
#include <thread>





#include ".Concepts/BaseConcepts.h"
//using namespace Platform::Collections::System;


#include "Arrays/GenericArrayExtensions.h"
#include "Arrays/ArrayFiller[TElement].h"
#include "Arrays/ArrayFiller[TElement, TReturnConstant].h"
#include "Arrays/ArrayPool.h"
#include "Arrays/ArrayPool[T].h"
#include "Arrays/ArrayString.h"
#include "Arrays/CharArrayExtensions.h"



#include "Lists/CharIListExtensions.h"
#include "Lists/IListComparer.h"
#include "Lists/IListEqualityComparer.h"
#include "Lists/IListExtensions.h"
#include "Lists/ListFiller.h"


#include "Segments/Segment.h"
#include "Segments/CharSegment.h"
#include "Segments/Walkers/AllSegmentsWalkerBase.h"
#include "Segments/Walkers/AllSegmentsWalkerBase[T, TSegment].h"
#include "Segments/Walkers/AllSegmentsWalkerBase[T].h"
#include "Segments/Walkers/AllSegmentsWalkerExtensions.h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase[T, TSegment].h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase[T].h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase[T, Segment].h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase[T].h"



#include "Stacks/DefaultStack.h"
#include "Stacks/IStack.h"
#include "Stacks/IStackExtensions.h"
#include "Stacks/IStackFactory.h"
#include "Stacks/StackExtensions.h"



#include "Trees/Node.h"



//#include "StringExtensions.h"




//#include "StringExtensions.h"



#endif //COLLECTIONS_PLATFORM_COLLECTIONS_H
