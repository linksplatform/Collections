from conans import ConanFile, CMake, tools


class PlatformCollectionsConan(ConanFile):
    settings = "os", "compiler", "build_type", "arch"
    generators = "cmake", "cmake_find_package"

    def requirements(self):
        self.requires("platform.interfaces/0.1.2")

    def build(self):
        cmake = CMake(self)
        cmake.configure()
        cmake.build()
