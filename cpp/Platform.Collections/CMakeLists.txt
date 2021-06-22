add_library(${CMAKE_PROJECT_NAME}.Library INTERFACE)

target_include_directories(${CMAKE_PROJECT_NAME}.Library INTERFACE ${CMAKE_CURRENT_SOURCE_DIR})

set_target_properties(${CMAKE_PROJECT_NAME}.Library PROPERTIES CXX_STANDARD 20)
