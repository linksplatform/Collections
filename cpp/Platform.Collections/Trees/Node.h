
// FIXME конечно же я просто закинул всё сюда
template <typename T>
concept std_hashable = requires(const T& object) {std::hash<T>{}(object);};

struct IHashCalculator {
    virtual std::size_t Hash(void* data) const = 0;
    ~IHashCalculator() = default;
};

template<std_hashable T>
struct HashCalculator : public IHashCalculator {
    std::size_t Hash(void* data) const final override {
        return std::hash<T>{}(*(T*)data);
    }
};

struct DataWrapper {
    void* data;
    std::shared_ptr<IHashCalculator> hash_calculator;

    template<typename T>
    DataWrapper(const T data) {
        this->data = static_cast<void*>((T*)&data);
        hash_calculator = std::make_shared<HashCalculator<T>>();
    }

    template<typename T>
    void operator=(const T data) {
        this->data = static_cast<void*>((T*)&data);
        hash_calculator = std::make_shared<HashCalculator<T>>();
    }

    bool operator==(const DataWrapper other) const {
        return Hash() == other.Hash();
    }

    void* Data() const {
        return data;
    }
    std::size_t Hash() const {
        return hash_calculator->Hash(data);
    }
};

namespace std {

    template<>
    struct hash<DataWrapper> {
        std::size_t operator()(const DataWrapper& wrapper) const {
            return wrapper.Hash();
        }
    };
}

namespace Platform::Collections::Trees
{
    class Node
    {

        public: std::any Value;

        std::unordered_map<DataWrapper, Node*> _childNodes;
        public: auto& ChildNodes() {return _childNodes;}

        public: Node(std::any value = std::any{}) : Value(value) {}



        public: Node* AddChild(DataWrapper key)
        {
            return AddChild(key, new Node());
        }

        public: Node* AddChild(DataWrapper key, std::any value)
        {
            return AddChild(key, new Node(value));
        }

        public: Node* AddChild(const DataWrapper key, Node* child)
        {
            // TODO шарповский 'Add' кидает исключение, если ключ уже находился в словаре. Заинлайним :)
            if(_childNodes.contains(key)) {throw std::logic_error("lol i trolled csharp razrabs"/*temp message*/);}
            _childNodes[key] = child;
            return child;
        }

        public: Node& operator[](DataWrapper key)
        {
            bool contains = _childNodes.contains(key);
            if(!contains)
                return *AddChild(key);

            return *_childNodes[key];
        }

/*
        public: bool ContainsChild(Platform::Collections::System::Array<DataWrapper> auto& keys)
        {
            return GetChild(keys) != nullptr;
        }

        public: template <Platform::Collections::System::Array<DataWrapper> TArray>
        Node& GetChild(const TArray& keys)
        {
            Node& node = *this;
            for (auto i = 0; i < keys.size(); i++)
            {
                node = operator[](keys[i]);
            }
            return node;
        }

        public: Node* RawGetChild(const auto key)
        {
            bool contains = _childNodes.contains(DataWrapper(key));
            if(!contains) throw std::logic_error(std::to_string(DataWrapper(key).Hash()));

            auto node = _childNodes[key];
            if (node == nullptr)
            {
                return nullptr;
            }
            return node;
        }

        public: auto GetChildValue(const Platform::Collections::System::Array<DataWrapper> auto& keys)
        {
            //return GetChild(keys) == nullptr ? nullptr : GetChild(keys)->Value;
            return GetChild(keys)->Value;
        }



        public: Node* SetChild(Platform::Collections::System::Array<void*> auto& keys) { return SetChildValue({}, keys); }

        public: Node* SetChild(auto key) { return SetChildValue({}, key); }

        public: Node* SetChildValue(auto value, Platform::Collections::System::Array<DataWrapper> auto& keys)
        {
            auto node = this;
            for (auto i = 0; i < keys.Length; i++)
            {
                node = SetChildValue(value, keys[i]);
            }
            //node->Value = value;
            return node;
        }

        public: Node* SetChildValue(auto value, auto key)
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
        */
    };
}