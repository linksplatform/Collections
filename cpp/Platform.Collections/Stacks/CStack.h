#pragma once

namespace Platform::Collections::Stacks
{
    template<typename TElement>
    concept CStack = requires(TElement item)
    {
        { self.empty() } -> std::same_as<bool>;

        { self.push(item) };

        { self.pop() };

        { self.top() } -> std::same_as<TElement&>;
    };
}
