cmake_minimum_required(VERSION 3.8)
project(Platform.Collections)

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


add_library(Platform.Collections.Library INTERFACE)

register_dependence(Platform.Collections.Library Equality)
register_dependence(Platform.Collections.Library Hashing)

target_compile_features(Platform.Collections.Library INTERFACE cxx_std_20)
