add_executable(${CMAKE_PROJECT_NAME}.Tests ${CMAKE_PROJECT_NAME}.Tests.cpp)

target_link_libraries(${CMAKE_PROJECT_NAME}.Tests PRIVATE CONAN_PKG::gtest)

set_target_properties(${CMAKE_PROJECT_NAME}.Tests PROPERTIES CXX_STANDARD 20)
