namespace Platform::Collections
{
    class ICollectionExtensions
    {
        public: template <typename T> static bool IsNullOrEmpty(ICollection<T> &collection) { return collection == nullptr || collection.Count() == 0; }

        public: template <typename T> static bool AllEqualToDefault(ICollection<T> &collection)
        {
            auto equalityComparer = EqualityComparer<T>.Default;
            return collection.All(item => equalityComparer.Equals(item, 0));
        }
    };
}
