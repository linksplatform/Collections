pragma once

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

#include "Lists/IListExtensions.h"
#include "Lists/ListFiller.h"

#include "Segments/Walkers/AllSegmentsWalkerBase.h"
#include "Segments/Walkers/DuplicateSegmentsWalkerBase.h"
#include "Segments/Walkers/DictionaryBasedDuplicateSegmentsWalkerBase.h"

#include "Sets/ISetExtensions.h"
#include "Sets/SetFiller.h"

#include "Stacks/IStack.h"
#include "Stacks/CStack.h"

#include "BitStringExtensions.h"
#include "StringExtensions.h"

#include "EnsureExtensions.h"

#include "IDictionaryExtensions.h"

#include "Trees/Node.h"
