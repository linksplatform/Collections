namespace Platform::Collections::Trees
{
    template <typename... Types>
    class Node
    {
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

        public: bool ContainsChild(const Platform::Collections::System::Array<KeyType> auto& keys)
        {
            return GetChild(keys) != nullptr;
        }

        ValueType GetChildValue(const Platform::Collections::System::Array<KeyType> auto& keys)
        {
            return GetChild(keys) == nullptr ? ValueType{} : GetChild(keys)->Value;
        }

        // TODO крутой костыль, но зато теперь коллекции реально идеально передаются
        //  в идеале первые две строчки requires'a должны быть везде, где передается Array и его тип может быть любым
        //  (не указан через template)
        //  а ещё лучше просто сделать Array без типа. То есть как IList<T> и IList
        public: template <typename TParams>
        requires
            requires(TParams params, int index)
            {
                {params[index]}; // Have operator[]

                requires Platform::Collections::System::Array<TParams, std::remove_reference_t<decltype(params[index])>>; // Is Array<type operator[]>

                std::convertible_to<decltype(params[index]), KeyType>; // по сути можно и через конструктор проверить
            }
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

        Node* SetChild(const Platform::Collections::System::Array<KeyType> auto& keys)
        {
            return SetChildValue(ValueType{}, keys);
        }

        Node* SetChild(ValueType key)
        {
            return SetChildValue(ValueType{}, std::vector{key});
        }

        Node* SetChildValue(ValueType value, const Platform::Collections::System::Array<KeyType> auto& keys)
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
    };
}