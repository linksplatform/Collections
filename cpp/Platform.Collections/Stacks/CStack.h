#pragma once

namespace Platform::Collections::Stacks
{
    template<typename TSelf, typename TElement>
    concept CStack = requires(TSelf& self, TElement item)
    {
        { self.push(item) };

        { self.pop() };
    } 
    &&
    requires(const TSelf& self, TElement item)
    {
        { self.empty() } -> std::same_as<bool>;

        { self.top() } -> std::same_as<const TElement&>;
    };
}
