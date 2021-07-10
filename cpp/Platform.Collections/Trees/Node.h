namespace Platform::Collections::Trees
{
    struct HelperTypeTag
    {
    };

    template<typename T>
    struct Repeat : public HelperTypeTag
    {
        using type = T;
    };

    template<typename T>
    concept NotHelperType = !std::derived_from<T, HelperTypeTag>;

    template<typename TValue, typename...>
    class Node;

    template<typename TValue>
    class Node<TValue>
    {
        static_assert(std::default_initializable<TValue>);

        public: TValue Value;

        public: Node() = default;

        public: explicit Node(TValue value = {}) : Value(std::move(value)) { }
    };

    template<typename TValue, typename TKey>
    class Node<TValue, Repeat<TKey>>
    {
        static_assert(std::default_initializable<TValue>);

        public: TValue Value;
        public: using Child = Node;

        private: std::unordered_map<TKey, Child*> _childNodes;
        public: auto ChildNodes() -> auto& { return _childNodes; }

        public: explicit Node(TValue value = {}) : Value(std::move(value)) { }

        public: auto operator[](const TKey& key) -> Child&
        {
            if(!_childNodes.contains(key))
                return AddChild(key);

            return *_childNodes[key];
        }

        public: auto ContainsChild(const std::vector<TKey>& keys) -> bool { return GetChild(keys) != nullptr; }

        public: auto GetChild(const std::vector<TKey>& keys) -> Child*
        {
            auto node = this;
            for (auto&& key : keys)
            {
                node = node->ChildNodes()[key];
                if (node == nullptr)
                {
                    return node;
                }
            }
            return node;
        }

        public: auto GetChildValue(const std::vector<TKey>& keys) -> TValue*
        {
            auto child = GetChild(keys);
            return (child == nullptr) ? nullptr : &child->Value;
        }

        public: auto AddChild(const TKey& key, const TValue& value = {}) -> Child& { return AddChild(key, Child(value)); }

        public: auto AddChild(TKey key, const Child& child) -> Child&
        {
            Dictionaries::Add(_childNodes, key, new Child{child});
            return *_childNodes[key];
        }

        public: auto SetChild(const std::vector<TKey>& keys) -> Child& { return SetChildValue(TValue{}, keys); }

        public: auto SetChild(TKey key) -> Child& { SetChildValue(TValue{}, key); }

        public: auto SetChildValue(const TValue& value, const std::vector<TKey>& keys) -> Child&
        {
            auto node = this;
            for (auto&& key : keys)
            {
                node = &SetChildValue(value, key);
            }
            node->Value = value;
            return *node;
        }

        public: auto SetChildValue(const TValue& value, const TKey& key) -> Child&
        {
            auto& child = (*this)[key];
            child.Value = value;
            return child;
        }
    };
}