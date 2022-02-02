#pragma once

namespace Platform::Collections::Stacks
{
    template <typename ...>
    struct IStack;

    template<typename TElement>
    struct IStack<TElement>
    {
        virtual bool empty() = 0;

        virtual void push(TElement item) = 0;

        virtual void pop() = 0;

        virtual TElement& top() = 0;

        virtual ~IStack() = default;
    };
}
