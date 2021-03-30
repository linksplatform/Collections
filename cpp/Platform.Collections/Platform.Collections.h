#ifndef COLLECTIONS_PLATFORM_COLLECTIONS_H
#define COLLECTIONS_PLATFORM_COLLECTIONS_H

#include ".Concepts/BaseConcepts.h"




#include "Arrays/GenericArrayExtensions.h"
#include "Arrays/ArrayFiller[TElement].h"
//#include "Arrays/ArrayFiller[TElement, TReturnConstant].h"
using namespace Platform::Collections::Arrays;

#include "Lists/CharIListExtensions.h"
#include "Lists/IListComparer.h"
#include "Lists/IListExtensions.h"
using namespace Platform::Collections::Lists;

#include "Segments/Segment.h"
#include "Segments/CharSegment.h"
#include "Segments/Walkers/AllSegmentsWalkerBase.h"
//#include "Segments/Walkers/AllSegmentsWalkerBase[T, TSegment].h"
using namespace Platform::Collections::Segments;

#include "Stacks/DefaultStack.h"
#include "Stacks/IStack.h"
#include "Stacks/IStackExtensions.h"
#include "Stacks/IStackFactory.h"
#include "Stacks/StackExtensions.h"
using namespace Platform::Collections::Stacks;



#include "Trees/Node.h"
using namespace Platform::Collections::Trees;


#endif //COLLECTIONS_PLATFORM_COLLECTIONS_H
