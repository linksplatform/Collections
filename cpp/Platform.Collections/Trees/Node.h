

// ReSharper disable ForCanBeConvertedToForeach

namespace Platform::Collections::Trees
{
    class Node
    {
        private: Dictionary<void*, Node> _childNodes;

        public: void *Value
        {
            get;
            set;
        }

        public: Dictionary<void*, Node> ChildNodes
        {
            get => _childNodes ?? (_childNodes = Dictionary<void*, Node>());
        }

        public: Node this[void *key]
        {
            get => GetChild(key) ?? AddChild(key);
            set => SetChildValue(value, key);
        }

        public: Node(void *value) { Value = value; }

        public: Node() : this({}) { }

        public: bool ContainsChild(params void *keys[]) { return this->GetChild(keys) != {}; }

        public: Node GetChild(params void *keys[])
        {
            auto node = this;
            for (auto i = 0; i < keys.Length; i++)
            {
                node.ChildNodes.TryGetValue(keys[i], out node);
                if (node == nullptr)
                {
                    return {};
                }
            }
            return node;
        }

        public: void *GetChildValue(params void *keys[]) { return this->GetChild(keys)?.Value; }

        public: Node AddChild(void *key) { return this->AddChild(key, this->Node({})); }

        public: Node AddChild(void *key, void *value) { return this->AddChild(key, this->Node(value)); }

        public: Node AddChild(void *key, Node child)
        {
            ChildNodes.Add(key, child);
            return child;
        }

        public: Node SetChild(params void *keys[]) { return this->SetChildValue({}, keys); }

        public: Node SetChild(void *key) { return this->SetChildValue({}, key); }

        public: Node SetChildValue(void *value, params void *keys[])
        {
            auto node = this;
            for (auto i = 0; i < keys.Length; i++)
            {
                node = this->SetChildValue(value, keys[i]);
            }
            node.Value = value;
            return node;
        }

        public: Node SetChildValue(void *value, void *key)
        {
            if (!ChildNodes.TryGetValue(key, out Node child))
            {
                child = this->AddChild(key, value);
            }
            child.Value = value;
            return child;
        }
    };
}