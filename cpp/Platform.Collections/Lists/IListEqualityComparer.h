namespace Platform::Collections::Lists
{
    template <typename ...> class IListEqualityComparer;
    template <typename T> class IListEqualityComparer<T> : public IEqualityComparer<IList<T>>
    {
        public: bool operator ==(const IList<T> &left, IList<T> &right) const { return left.EqualTo(right); }

        public: std::int32_t GetHashCode(IList<T> &list) { return list.GenerateHashCode(); }
    };
}
