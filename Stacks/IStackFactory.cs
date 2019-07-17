using Platform.Interfaces;
using Platform.Collections.Stacks;

namespace Platform.Helpers.Collections.Stacks
{
    public interface IStackFactory<TElement> : IFactory<IStack<TElement>>
    {
    }
}
