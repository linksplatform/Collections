git clone https://github.com/linksplatform/conan-center-index
cd conan-center-index && cd recipes
conan create platform.interfaces/all --version 0.3.41
conan create platform.random/all platform.random/all --version 0.2.0
conan create platform.equality/all platform.equality/all --version 0.0.1
conan create platform.exceptions/all platform.exceptions/all --version 0.3.2
conan create platform.hashing/all platform.hashing/all --version 0.5.0
