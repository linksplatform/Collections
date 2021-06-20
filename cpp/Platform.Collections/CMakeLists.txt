cmake_minimum_required(VERSION 3.8)

set(CMAKE_PROJECT_NAME Platform.Collections)
project(${CMAKE_PROJECT_NAME})

# DEPRECATED SYNTAX (Oh my God)
    set(LINKS_PLATFORM_DIR ../../../)
    set(LINKS_PLATFORM_DEPENDENCIES_DIR ${CMAKE_CURRENT_BINARY_DIR}/_deps/LinksPlatform)

    function(check_dependence DEPENDENCE_NAME)
        if (NOT EXISTS ${LINKS_PLATFORM_DIR}/${DEPENDENCE_NAME})
            execute_process(COMMAND git clone https://github.com/uselessgoddess/${DEPENDENCE_NAME}.git ${LINKS_PLATFORM_DIR}/)
        endif ()
    endfunction()

    function(add_dependence EXECUTABLE_NAME DEPENDENCE_NAME)
        set(DEPENDENCE_DIR ${LINKS_PLATFORM_DIR}/${DEPENDENCE_NAME}/cpp)
        add_subdirectory(${DEPENDENCE_DIR} ${LINKS_PLATFORM_DEPENDENCIES_DIR}/${DEPENDENCE_NAME}-build)
        target_link_libraries(${EXECUTABLE_NAME} INTERFACE Platform.${DEPENDENCE_NAME}.Library)
    endfunction()

    function(register_dependence EXECUTABLE_NAME DEPENDENCE_NAME)
        check_dependence(${DEPENDENCE_NAME})
        add_dependence(${EXECUTABLE_NAME} ${DEPENDENCE_NAME})
    endfunction()
# DEPRECATED

add_library(${CMAKE_PROJECT_NAME}.Library INTERFACE)

# DEPRECATED
    register_dependence(${CMAKE_PROJECT_NAME}.Library Equality)
    register_dependence(${CMAKE_PROJECT_NAME}.Library Hashing)
# DEPRECATED

set_target_properties(${CMAKE_PROJECT_NAME}.Library PROPERTIES CXX_STANDARD 20)
