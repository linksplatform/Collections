git clone https://github.com/linksplatform/conan-center-index
cd conan-center-index && git checkout only-development && cd recipes
conan create platform.interfaces/0.2.0+ platform.interfaces/0.2.5@ -pr=linksplatform
conan create platform.random/all platform.random/0.1.0@ -pr=linksplatform
conan create platform.equality/all platform.equality/0.0.1@ -pr=linksplatform
conan create platform.exceptions/all platform.exceptions/0.2.0@ -pr=linksplatform
conan create platform.hashing/all platform.hashing/0.2.0@ -pr=linksplatform
