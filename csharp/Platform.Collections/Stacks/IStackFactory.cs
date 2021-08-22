using Platform.Interfaces;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Stacks
{
    /// <summary>
    /// <para>
    /// Defines the stack factory.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="IFactory{IStack{TElement}}"/>
    public interface IStackFactory<TElement> : IFactory<IStack<TElement>>
    {
    }
}
