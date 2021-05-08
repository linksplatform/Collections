cmake_minimum_required(VERSION 3.17)
project(Platform.Collections)

include_directories(../Platform.Collections)

include(FetchContent)
FetchContent_Declare(
        googletest
        URL https://github.com/google/googletest/archive/609281088cfefc76f9d0ce82e1ff6c30cc3591e5.zip
)
#set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)
FetchContent_MakeAvailable(googletest)

FetchContent_GetProperties(googletest)
if(NOT googletest_POPULATED)
    FetchContent_Populate(googletest)
    add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})
endif()


add_executable(Platform.Collections.Tests Platform.Collections.Tests.cpp)

target_compile_features(Platform.Collections.Tests PUBLIC cxx_std_20)

target_link_libraries(Platform.Collections.Tests PUBLIC gtest gtest_main)
