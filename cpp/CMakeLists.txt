cmake_minimum_required(VERSION 3.17)

project(Platform.Collections)
include(${CMAKE_BINARY_DIR}/conanbuildinfo.cmake)
set(CONAN_DISABLE_CHECK_COMPILER TRUE)
conan_basic_setup(TARGETS)

add_subdirectory(${CMAKE_PROJECT_NAME})
target_link_libraries(${CMAKE_PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.interfaces)

# FUTURE
    # target_link_libraries(${CURRENT_PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.equality) # may be Platform.Equality
    # target_link_libraries(${CURRENT_PROJECT_NAME}.Library INTERFACE CONAN_PKG::platform.hashing) # may be Platform.Hashing
# FUTURE

add_subdirectory(${CMAKE_PROJECT_NAME}.Tests)
target_link_libraries(${CMAKE_PROJECT_NAME}.Tests PUBLIC ${CMAKE_PROJECT_NAME}.Library)