

// ReSharper disable ForCanBeConvertedToForeach

namespace Platform::Collections::Trees
{
    class Node
    {
        private: std::map<void*, Node*> _childNodes;

        public: void* Value;

        public: auto ChildNodes()
        {
            return _childNodes;
        }

        //public: Node this[void* key]
        //{
        //    get => GetChild(key) ?? AddChild(key);
        //    set => SetChildValue(value, key);
        //}

        public: Node(void* value) { Value = value; }

        //public: bool ContainsChild(Array<void*> auto& keys) { return this->GetChild(keys) != {}; }

        public: Node* GetChild(Array<void*> auto& keys)
        {
            auto node = this;
            for (auto i = 0; i < keys.size(); i++)
            {
                // TODO просто заинлайнил ручками 'TryGetValue'
                bool contains = node->ChildNodes().contains(keys[i]);
                node = node->ChildNodes()[keys[i]];
                if (node == nullptr)
                {
                    return nullptr;
                }
            }
            return node;
        }

        public: void* GetChildValue(Array<void*> auto& keys) { GetChild(keys) == nullptr ? nullptr : GetChild(keys)->Value; }

        public: Node* AddChild(void* key) { return this->AddChild(key, new Node({})); }

        public: Node* AddChild(void* key, void* value) { return this->AddChild(key, new Node(value)); }

        public: Node* AddChild(void* key, Node* child)
        {
            // TODO шарповский 'Add' кидает исключение, если ключ уже находился в словаре. Заинлайним :)
            if(ChildNodes().contains(child)) {throw std::logic_error("lol i trolled csharp razrabs"/*temp message*/);}
            ChildNodes().emplace(key, child);
            return child;
        }

        public: Node SetChild(Array<void*> auto& keys) { return this->SetChildValue({}, keys); }

        public: Node SetChild(void* key) { return this->SetChildValue({}, key); }

        public: Node SetChildValue(void* value, Array<void*> auto& keys)
        {
            auto node = this;
            for (auto i = 0; i < keys.Length; i++)
            {
                node = SetChildValue(value, keys[i]);
            }
            node->Value = value;
            return node;
        }

        public: Node SetChildValue(void* value, void* key)
        {
            bool contains = ChildNodes().contains(key);
            auto child = ChildNodes()[key];
            if (!contains)
            {
                child = AddChild(key, value);
            }
            child->Value = value;
            return child;
        }
    };
}