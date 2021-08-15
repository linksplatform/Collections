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

    template<typename Self>
    concept ValueNode = requires(Self self)
    {
        self.Value;
    };

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
        template<typename TValue, typename TKey, typename Child>
        class NodeBase
        {
            using dict_type = std::map<TKey, Child>;
            private: std::unique_ptr<dict_type> _childNodes = std::make_unique<dict_type>();
            public: auto ChildNodes() const -> auto&& { return *_childNodes; }

            public: NodeBase() = default;

            public: NodeBase(const NodeBase& other)
                : _childNodes(std::make_unique<dict_type>(other.ChildNodes())) { }

            public: auto operator[](const TKey& key) -> Child&
            {
                if(!ChildNodes().contains(key))
                    return AddChild(key);

                return ChildNodes()[key];
            }

            public: auto ContainsChild(std::ranges::range auto&& keys) const -> bool { return GetChild(std::forward<decltype(keys)>(keys)).has_value(); }

            public: auto GetChild(std::ranges::range auto&& keys) const -> auto
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

            public: auto GetChildValue(std::ranges::range auto&& keys) const -> auto
            {
                auto child = GetChild(std::forward<decltype(keys)>(keys));
                using optional_type = decltype(std::optional{std::ref(child.value().get().Value)});
                return (child.has_value()) ? std::optional{std::ref(child.value().get().Value)} : optional_type{std::nullopt};
            }

            public: auto AddChild(const TKey& key, const TValue& value) -> Child& { return AddChild(key, Child{value}); }

            public: auto AddChild(const TKey& key) -> Child& { return AddChild(key, Child{}); }

            public: auto AddChild(TKey key, Child child) -> Child&
            {
                Dictionaries::Add(ChildNodes(), key, std::move(child));
                return ChildNodes()[key];
            }

            public: auto SetChild(auto&&... keys) -> Child& { return SetChildValue({}, std::forward<decltype(keys)>(keys)...); }

            public: auto SetChildValue(const TValue& value, std::ranges::range auto&& keys) -> Child&
            {
                auto node = this;
                for (auto&& key : keys)
                {
                    node = &SetChildValue(value, key);
                }
                node->Value = value;
                return *node;
            }
        };

        template<typename TNode, std::size_t depth>
        consteval auto ChildHasValueHelper()
        {
            if constexpr (depth == 0) {
                return ValueNode<TNode>;
            } else {
                return requires(TNode node) {
                    requires ChildHasValueHelper<decltype(node.ChildNodes().at(0)), depth - 1>();
                };
            }
        }

        template<typename TNode, std::size_t depth>
        consteval auto ChildHasValue()
        {
            return ChildHasValueHelper<TNode, depth>();
        }
    }

    template<typename TValue, typename TKey>
    struct Node<TValue, Repeat<TKey>>
        : public Internal::NodeBase<TValue, TKey, Node<TValue, Repeat<TKey>>>,
          public Node<TValue>
    {
        using base = Internal::NodeBase<TValue, TKey, Node<TValue, Repeat<TKey>>>;
        using base::base;

        using leaf_base = Node<TValue>;
        using leaf_base::leaf_base;

        Node() : base(), leaf_base() {}

        Node(const Node& other) : base(other), leaf_base(other) {}
    };

    template<typename TValue, NotHelperType TKey, typename... Tail>
    class Node<TValue, TKey, Tail...> : public Internal::NodeBase<TValue, TKey, Node<TValue, Tail...>>
    {
        using Child = Node<TValue, Tail...>;
        using base = Internal::NodeBase<TValue, TKey, Child>;
        using base::base;

        auto& AddChild(const TKey& key, const TValue& value) requires ValueNode<Child> { return base::AddChild(key, value); };

        auto& GetChild(auto&&... keys) const
        {
            std::tuple tuple_keys = { std::forward<decltype(keys)>(keys)... };
            return _GetChild(std::move(tuple_keys), *this);
        }

        public: auto GetChildValue(auto&&... keys) requires (Internal::ChildHasValue<Node, sizeof...(keys)>())
        {
            return GetChild(std::forward<decltype(keys)>(keys)..., *this).Value;
        };

        auto& SetChildValue(auto&& value, auto&&... keys) requires (Internal::ChildHasValue<Node, sizeof...(keys)>())
        {
            auto& child = GetChild(std::forward<decltype(keys)>(keys)..., *this);
            child.Value = value;
            return child;
        }

        auto& SetChildValue(auto&&... keys) requires (Internal::ChildHasValue<Node, sizeof...(keys)>())
        {
            SetChildValue(TValue{}, std::forward<decltype(keys)>(keys)...);
        }

    private:
        template<std::size_t depth = 0>
        auto& _GetChild(auto tuple_keys, auto& root) const
        {
            if constexpr (std::tuple_size_v<decltype(tuple_keys)> - 2 == depth)
            {
                return root.ChildNodes().at(std::get<depth>(tuple_keys));
            }
            else
            {
                return _GetChild<depth + 1>(std::move(tuple_keys), root.ChildNodes().at(std::get<depth>(tuple_keys)));
            }
        }
    };
}
