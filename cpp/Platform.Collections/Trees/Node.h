namespace Platform::Collections::Trees
{
    struct NodeTag
    {
    };

    template<typename T>
    struct Repeat : public NodeTag
    {
        using type = T;
    };

    template<typename T>
    concept NotNodeTag = not std::derived_from<T, NodeTag>;

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

            public: auto operator[](TKey key) -> Child&
            {
                if(!ChildNodes().contains(key))
                    return AddChild(key);

                return ChildNodes()[std::move(key)];
            }

            public: auto ContainsChild(std::ranges::range auto&& keys) const -> bool
            {
                auto* node = this;
                for (auto&& key : keys)
                {
                    auto iter = node->ChildNodes().find(key);
                    if (iter != node->ChildNodes().end())
                    {
                        node = &*iter;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }

            public: auto GetChild(std::ranges::range auto&& keys) const -> Child&
            {
                auto* node = this;
                for (auto&& key : keys)
                {
                    auto iter = node->ChildNodes().find(key);
                    if (iter != node->ChildNodes().end())
                    {
                        node = &*iter;
                    }
                    else
                    {
                        throw std::logic_error{"unknown error"};
                    }
                }
                return *node;
            }

            public: auto GetChildValue(std::ranges::range auto&& keys) const -> auto
            {
                return GetChild(std::forward<decltype(keys)>(keys)).Value;
            }

            public: auto AddChild(const TKey& key, const TValue& value) -> Child& { return AddChild(key, Child{value}); }

            public: auto AddChild(const TKey& key) -> Child& { return AddChild(key, Child{}); }

            public: auto AddChild(TKey key, Child child) -> Child&
            {
                Dictionaries::Add(ChildNodes(), key, std::move(child));
                return ChildNodes()[std::move(key)];
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

            public: auto SetChildValue(TValue value, TKey key) -> Child&
            {
                auto& child = (*this)[std::move(key)];
                child.Value = std::move(value);
                return child;
            }
        };
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

        auto&& GetChild(auto... keys) const
        {
            return _GetChild(std::tie(keys...), *this);
        }

        public: auto&& GetChildValue(auto&&... keys) const
        {
            return GetChild(std::forward<decltype(keys)>(keys)...).Value;
        };

        auto& SetChildValue(auto&& value, auto&&... keys)
        {
            auto& child = GetChild(std::forward<decltype(keys)>(keys)...);
            child.Value = value;
            return child;
        }

        auto& SetChildValue(auto&&... keys)
        {
            SetChildValue(TValue{}, std::forward<decltype(keys)>(keys)...);
        }

    private:
        template<std::size_t depth = 0>
        auto&& _GetChild(auto tuple_keys, auto&& root) const
        {
            using tuple_type = decltype(tuple_keys);
            constexpr auto tuple_size = std::tuple_size_v<tuple_type>;
            auto&& child_nodes = root.ChildNodes();
            auto&& child_key = std::get<depth>(tuple_keys);

            auto iter = child_nodes.find(child_key);
            if (iter == child_nodes.end())
            {
                throw std::logic_error{"unknown error"};
            }
            auto&& [key, child] = *iter;
            if constexpr (tuple_size - 1 == depth)
            {
                return child;
            }
            else
            {
                return _GetChild<depth + 1>(tuple_keys, child);
            }
        }
    };
}
