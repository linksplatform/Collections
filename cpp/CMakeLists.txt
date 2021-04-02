cmake_minimum_required(VERSION 3.16)
project(Platform.Collections)

set(CMAKE_CXX_STANDARD 20) # use only C++20

set(CMAKE_CXX_FLAGS -O3)

add_executable(Platform.Collections Platform.Collections.Tests/test.cpp)