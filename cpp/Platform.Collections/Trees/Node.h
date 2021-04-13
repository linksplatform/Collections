namespace Platform::Collections::Trees
{
    struct Helper{};

    template<typename T>
    struct Repeat : public Helper{
        using type = T;
    };

    template<typename T>
    concept NotHelperType = !std::is_base_of_v<T, Helper>;

    template<typename TValue, typename...>
    class Node;

    template<typename TValue, typename...>
    class Node
    {
        public:
        TValue Value;

        Node(TValue value = TValue{})
        {
            Value = value;
        }
    };


    template<typename TValue, NotHelperType TKey, typename ... Tail>
    class Node<TValue, TKey, Tail...>
    {
        public:

        Node() = default;
        std::unordered_map<TKey, Node<TValue, Tail...>> _childNodes;

        auto& operator[](TKey key)
        {
            if(!_childNodes.contains(key))
                return AddChild(key);

            return _childNodes[key];
        }

        auto& AddChild(TKey key, const Node<TValue, Tail...>& node = Node<TValue, Tail...>())
        {
            IDictionaryExtensions::Add(_childNodes, key, node);
            return _childNodes[key];
        }
    };

    template<typename TValue, typename TKey>
    class Node<TValue, Repeat<TKey>>
    {
        public:
        TValue Value;
        std::unordered_map<TKey, Node<TValue, Repeat<TKey>>*> _childNodes;

        Node(TValue value = TValue{})
        {
            Value = value;
        }

        auto& operator[](TKey key)
        {
            if(!_childNodes.contains(key))
                return AddChild(key);

            return *_childNodes[key];
        }

        auto& AddChild(TKey key, TValue value = TValue{})
        {
            return AddChild(key, Node<TValue, Repeat<TKey>>(value));
        }

        auto& AddChild(TKey key, Node<TValue, Repeat<TKey>> node)
        {
            IDictionaryExtensions::Add(_childNodes, key, new Node<TValue, Repeat<TKey>>(node));
            return *_childNodes[key];
        }
    };


}