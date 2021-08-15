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

        public: TValue Value{};

        public: Node() = default;

        public: explicit Node(TValue value) : Value(std::move(value)) { }
    };

    namespace Internal
    {
        template<typename Self>
        concept ValueNode = requires(Self self)
        {
            self.Value;
        };

        template<typename TValue, typename TKey, typename Child>
        struct NodeBase : public Node<TValue>
        {
            using base = Node<TValue>;
            using base::base;

            using dict_type = std::unordered_map<TKey, Child>;
            private: std::unique_ptr<dict_type> _childNodes = std::make_unique<dict_type>();
            public: auto ChildNodes() const -> auto&& { return *_childNodes; }

            public: NodeBase(const NodeBase& other)
                : base(other.Value), _childNodes(std::make_unique<dict_type>(other.ChildNodes())) { }

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
                using optional_type = decltype(std::optional{std::ref(*node)});
                for (auto&& key : keys)
                {
                    if (node->ChildNodes().contains(key))
                    {
                        node = &node->ChildNodes().at(key);
                    }
                    else
                    {
                        return optional_type{std::nullopt};
                    }
                }
                return std::optional{std::ref(*node)};
            }

            public: auto GetChildValue(const std::vector<TKey>& keys) const -> auto
            {
                auto child = GetChild(keys);
                using optional_type = decltype(std::optional{std::ref(child.value().get().Value)});
                return (child.has_value()) ? std::optional{std::ref(child.value().get().Value)} : optional_type{std::nullopt};
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

    template<typename TValue, typename TKey>
    class Node<TValue, Repeat<TKey>> : public Internal::NodeBase<TValue, TKey, Node<TValue, Repeat<TKey>>>
    {
        using base = Internal::NodeBase<TValue, TKey, Node<TValue, Repeat<TKey>>>;
        using base::base;
    };

    template<typename TValue, NotHelperType TKey, typename... Tail>
    class Node<TValue, TKey, Tail...> : public Internal::NodeBase<TValue, TKey, Node<TValue, Tail...>>
    {
        using Child = Node<TValue, Tail...>;
        using base = Internal::NodeBase<TValue, TKey, Child>;
        using base::base;

        auto& AddChild(const TKey& key, const TValue& value = {}) requires Internal::ValueNode<Child> { return base::AddChild(key, Child{value}); };
        auto GetChildValue(auto&&... args) requires Internal::ValueNode<Child> { return base::SetChildValue(std::forward<decltype(args)>(args)...); };
        auto& SetChildValue(auto&&... args) requires Internal::ValueNode<Child> { return base::SetChildValue(std::forward<decltype(args)>(args)...); };
    };
}