cmake_minimum_required(VERSION 3.17)
include_directories(../${CMAKE_PROJECT_NAME})

include(FetchContent)
FetchContent_Declare(
    googletest
    URL https://github.com/google/googletest/archive/609281088cfefc76f9d0ce82e1ff6c30cc3591e5.zip
)

FetchContent_MakeAvailable(googletest)
FetchContent_GetProperties(googletest)
if(NOT googletest_POPULATED)
    FetchContent_Populate(googletest)
    add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})
endif()


add_executable(${CMAKE_PROJECT_NAME}.Tests ${CMAKE_PROJECT_NAME}.Tests.cpp)

target_link_libraries(${CMAKE_PROJECT_NAME}.Tests PUBLIC gtest gtest_main)

set_target_properties(${CMAKE_PROJECT_NAME}.Tests PROPERTIES CXX_STANDARD 20)
