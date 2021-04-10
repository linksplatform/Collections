namespace Platform::Collections::Trees
{
    template <typename... Types>
    class Node
    {
        #define PARAMS_REQUIRES(TParams) \
            requires(TParams params, int index) \
            { \
                requires Platform::Collections::System::Array<TParams, std::decay_t<decltype(params[index])>>; \
                std::convertible_to<decltype(params[index]), KeyType>; \
            }

        using ValueType = std::any;
        using KeyType = std::variant<Types...>;
        using Dictionary = std::unordered_map<KeyType, Node*>;

        static_assert(!std::same_as<ValueType, Node>);

        public: ValueType Value;
        private: Dictionary _childNodes;

        public: Dictionary& ChildNodes() {return _childNodes;}

        public: Node(ValueType value = ValueType{})
        {
            Value = value;
        }

        public: Node& operator[](KeyType key)
        {
            return _childNodes.contains(key) ? *_childNodes[key] : *AddChild(key);
        }

        public: Node* AddChild(KeyType key, ValueType value = ValueType{})
        {
            return AddChild(key, new Node(value));
        }

        public: Node* AddChild(KeyType key, Node* node)
        {
            IDictionaryExtensions::Add(_childNodes, key, node);
            return node;
        }

        public: template <typename TParams>
        requires
            PARAMS_REQUIRES(TParams)
        bool ContainsChild(const TParams& keys)
        {
            return GetChild(keys) != nullptr;
        }

        public: template <typename TParams>
        requires
            PARAMS_REQUIRES(TParams)
        ValueType GetChildValue(const TParams& keys)
        {
            return GetChild(keys) == nullptr ? ValueType{} : GetChild(keys)->Value;
        }

        // TODO крутой костыль, но зато теперь коллекции реально идеально передаются
        //  в идеале первая строчка requires'a должна быть везде, где передается Array и его тип может быть любым
        //  (не указан через template)
        //  а ещё лучше просто сделать Array без типа. То есть как IList<T> и IList
        public: template <typename TParams>
        requires
            PARAMS_REQUIRES(TParams)
        Node* GetChild(const TParams& keys)
        {
            auto* node = this;
            for (int i = 0; i < keys.size(); i++)
            {
                Dictionary& dictionary = node->ChildNodes();
                if(!dictionary.contains(keys[i]))
                    return nullptr;

                node = dictionary[keys[i]];
            }
            return node;
        }

        public: template <typename TParams>
        requires
            PARAMS_REQUIRES(TParams)
        Node* SetChild(const TParams& keys)
        {
            return SetChildValue(ValueType{}, keys);
        }

        Node* SetChild(ValueType key)
        {
            return SetChildValue(ValueType{}, std::vector{key});
        }

        public: template <typename TParams>
        requires
            PARAMS_REQUIRES(TParams)
        Node* SetChildValue(ValueType value, const TParams& keys)
        {
            auto node = this;
            for (auto i = 0; i < keys.size(); i++)
            {
                node = SetChildValue(value, keys[i]);
            }
            node->Value = value;
            return node;
        }

        Node* SetChildValue(ValueType value, KeyType key)
        {
            Node* child;
            if (!IDictionaryExtensions::TryGetValue(_childNodes, key, child))
            {
                child = AddChild(key, value);
            }
            child->Value = value;
            return child;
        }

        #undef PARAMS_REQUIRES
    };
}