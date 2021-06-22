cmake_minimum_required(VERSION 3.17)

project(Platform.Collections)
include(${CMAKE_BINARY_DIR}/conanbuildinfo.cmake)
set(CONAN_DISABLE_CHECK_COMPILER TRUE)
conan_basic_setup(TARGETS)

add_library(${PROJECT_NAME}.Library INTERFACE)
target_include_directories(${PROJECT_NAME}.Library INTERFACE ${PROJECT_NAME})
target_link_libraries(${PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.interfaces)

# FUTURE
    # target_link_libraries(${CURRENT_PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.equality) # may be Platform.Equality
    # target_link_libraries(${CURRENT_PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.hashing) # may be Platform.Hashing
# FUTURE

add_executable(${PROJECT_NAME}.Tests ${PROJECT_NAME}.Tests/${PROJECT_NAME}.Tests.cpp)
set_target_properties(${PROJECT_NAME}.Tests PROPERTIES CXX_STANDARD 20)

target_link_libraries(${PROJECT_NAME}.Tests PUBLIC CONAN_PKG::gtest)
target_link_libraries(${PROJECT_NAME}.Tests PUBLIC ${PROJECT_NAME}.Library)


