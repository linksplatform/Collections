#!/bin/bash

pip install conan

conan profile new linksplatform --detect
conan profile update settings.compiler=clang linksplatform
conan profile update settings.compiler.version=11 linksplatform
conan profile update settings.compiler.libcxx=libstdc++11 linksplatform
conan profile update env.CXX=clang++ linksplatform
conan profile show linksplatform

git clone https://github.com/linksplatform/conan-center-index
cd conan-center-index && cd recipes
git checkout update-interfaces
conan create platform.random/all platform.interfaces/0.2.0@ -pr=linksplatform
git checkout only-development
conan create platform.converters/all platform.converters/0.1.0@ -pr=linksplatform
conan create platform.ranges/all platform.ranges/0.1.3@ -pr=linksplatform
conan create platform.random/all platform.random/0.1.0@ -pr=linksplatform

cmake_flags="-DCMAKE_BUILD_TYPE=Release -DCMAKE_CXX_COMPILER=clang++ -DLINKS_PLATFORM_TESTS=ON"
cmake_build_dir="build"
cd cpp && mkdir $cmake_build_dir && cd $cmake_build_dir
conan install .. -if=. -pr=linksplatform --build=missing
cmake .. $cmake_flags
cmake --build .
scan-build cmake --build .
binaries=bin/*
for binary in $binaries
do
   ./$binary
done