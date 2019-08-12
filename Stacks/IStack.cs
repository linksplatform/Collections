namespace Platform.Collections.Stacks
{
    public interface IStack<TElement>
    {
        bool IsEmpty { get; }
        void Push(TElement element);
        TElement Pop();
        TElement Peek();
    }
}
