from conans import ConanFile, CMake, tools


class PlatformCollectionsConan(ConanFile):
    settings = "os", "compiler", "build_type", "arch"
    generators = "cmake"

    def requirements(self):
        self.requires("gtest/cci.20210126")
        self.requires("platform.interfaces/0.1.2")

    def build(self):
        cmake = CMake(self)
        cmake.configure()
        cmake.build()
