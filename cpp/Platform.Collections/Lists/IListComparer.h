namespace Platform::Collections::Lists
{
    template <typename ...> class IListComparer;
    template <typename T> class IListComparer<T> : public IComparer<IList<T>>
    {
        public: std::int32_t Compare(IList<T> &left, IList<T> &right) { return left.CompareTo(right); }
    };
}
