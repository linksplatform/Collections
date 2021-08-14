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
    concept NotHelperType = not std::derived_from<T, HelperTypeTag>;

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
        static_assert(std::default_initializable<TValue>,
            "todo!");

        public: using Child = Node;
        public: using RefChild = std::reference_wrapper<Child>;
        public: using ConstRefChild = std::reference_wrapper<const Child>;
        public: using RefValue = std::reference_wrapper<TValue>;

        public: TValue Value;

        using dict_type = std::unordered_map<TKey, Child>;
        private: std::unique_ptr<dict_type> _childNodes = std::make_unique<dict_type>();
        public: auto ChildNodes() const -> auto&& { return *_childNodes; }

        public: explicit Node(TValue value = {}) : Value(std::move(value)) { }

        public: Node(const Node& other)
            : Value(other.Value), _childNodes(std::make_unique<dict_type>(other.ChildNodes())) { }

        public: auto operator[](const TKey& key) -> Child&
        {
            if(!ChildNodes().contains(key))
                return AddChild(key);

            return ChildNodes()[key];
        }

        public: auto ContainsChild(const std::vector<TKey>& keys) const -> bool { return GetChild(keys).has_value(); }

        public: auto GetChild(const std::vector<TKey>& keys) const -> auto
        {
            auto* node = this;
            for (auto&& key : keys)
            {
                if (node->ChildNodes().contains(key))
                {
                    node = &node->ChildNodes().at(key);
                }
                else
                {
                    return decltype(std::optional{std::ref(*node)})(std::nullopt);
                }
            }
            return std::optional{std::ref(*node)};
        }

        public: auto GetChildValue(const std::vector<TKey>& keys) const -> std::optional<const RefValue>
        {
            auto child = (std::optional<RefChild>)GetChild(keys);
            return (child.has_value()) ? child.value().Value : std::nullopt;
        }

        public: auto AddChild(const TKey& key, const TValue& value = {}) -> Child& { return AddChild(key, Child(value)); }

        public: auto AddChild(TKey key, Child child) -> Child&
        {
            Dictionaries::Add(ChildNodes(), key, std::move(child));
            return ChildNodes()[key];
        }

        public: auto SetChild(const std::vector<TKey>& keys) -> Child& { return SetChildValue({}, keys); }

        public: auto SetChild(TKey key) -> Child& { SetChildValue({}, key); }

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