git clone https://github.com/linksplatform/conan-center-index
cd conan-center-index && cd recipes
conan create platform.interfaces/all --version 0.3.41
conan create platform.delegates/all --version 0.3.7
conan create platform.exceptions/all --version 0.3.2
conan create platform.converters/all --version 0.2.0
conan create platform.hashing/all --version 0.5.4
conan create platform.ranges/all --version 0.2.0
conan create platform.random/all --version 0.2.0
conan create platform.equality/all --version 0.0.1
