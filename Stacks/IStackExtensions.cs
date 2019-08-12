namespace Platform.Collections.Stacks
{
    public static class IStackExtensions
    {
        public static void Clear<T>(this IStack<T> stack)
        {
            while (!stack.IsEmpty)
            {
                _ = stack.Pop();
            }
        }
    }
}
