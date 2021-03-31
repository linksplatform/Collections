#ifndef COLLECTIONS_PLATFORM_COLLECTIONS_H
#define COLLECTIONS_PLATFORM_COLLECTIONS_H

#include <type_traits>
#include <concepts>
#include <vector>
#include <string>
#include <array>
#include <map>
#include <set>
#include <memory>
#include <ranges>
#include <algorithm>
#include <cmath>
#include <span>
#include <iostream>
#include <functional>





#include ".Concepts/BaseConcepts.h"



#include "Arrays/ArrayFiller[TElement].h"
#include "Arrays/ArrayFiller[TElement, TReturnConstant].h"
#include "Arrays/ArrayPool.h"
#include "Arrays/ArrayPool[T].h"
#include "Arrays/ArrayString.h"
#include "Arrays/CharArrayExtensions.h"
#include "Arrays/GenericArrayExtensions.h"
using namespace Platform::Collections::Arrays;

#include "Lists/CharIListExtensions.h"
#include "Lists/IListComparer.h"
#include "Lists/IListEqualityComparer.h"
#include "Lists/IListExtensions.h"
#include "Lists/ListFiller.h"
using namespace Platform::Collections::Lists;

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
using namespace Platform::Collections::Segments;
using namespace Platform::Collections::Segments::Walkers;

#include "Stacks/DefaultStack.h"
#include "Stacks/IStack.h"
#include "Stacks/IStackExtensions.h"
#include "Stacks/IStackFactory.h"
#include "Stacks/StackExtensions.h"
using namespace Platform::Collections::Stacks;



#include "Trees/Node.h"
using namespace Platform::Collections::Trees;


#endif //COLLECTIONS_PLATFORM_COLLECTIONS_H
