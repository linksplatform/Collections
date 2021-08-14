namespace Platform::Collections::Tests
{
    TEST(NodeTests, Basic)
    {
        using namespace Collections::Trees;

        using value_t = int;
        Node<value_t, int, int, Repeat<std::string>> node;
        // node["123"]; // compile error
        node[123][123]["123"]["sdf"]["gfd"]["444"];

        Node<value_t, int, int, std::string> node2;
        // node2[123][321]["fsdf"]["fdw"] // compile error
        node2[123][321]["fsdf"];
    }

    template<typename TValue, typename... Args>
    void DFS_print(const Trees::Node<TValue, Args...>& root, std::string modifier = "")
    {
        for (auto&& [key, child] : root.ChildNodes()) {
            std::cout << modifier << key << ": " << child.Value << std::endl;
            DFS_print(child, modifier + "  ");
        }
    }

    TEST(NodeTests, SmallExample)
    {
        using namespace Collections::Trees;

        using value_t = std::size_t;
        Node<value_t, Repeat<std::string>> population;

        auto& earth = population["Earth"];
        earth.Value = 7'886'226'000;

        earth["Eurasia"].Value = 5'348'554'000;
        earth["Africa"].Value = 1'275'920'000;
        earth["Australia"].Value = 25'726'000;
        earth["America"].Value = 25'726'000;
        earth["Antarctica"].Value = 4000;

        earth["America"]["USA"].Value = 332'278'200;
        earth["America"]["USA"]["New York metropolitan area"].Value = 332'278'200;
        earth["America"]["USA"]["New York metropolitan area"]["New York"].Value = 8'405'837;

        earth["Eurasia"]["Russia"].Value = 145'975'300;
        earth["Eurasia"]["Russia"]["Moscow"].Value = 12'655'050;

        DFS_print(population);
    }
}