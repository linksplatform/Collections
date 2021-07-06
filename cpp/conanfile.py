from conans import ConanFile, CMake


class PlatformCollectionsConan(ConanFile):
    settings = "os", "compiler", "build_type", "arch"
    generators = "cmake"

    def requirements(self):
        self.requires("gtest/cci.20210126")
        self.requires("platform.interfaces/0.1.3")
        self.requires("platform.exceptions/0.1.0")

    def build(self):
        cmake = CMake(self)
        cmake.configure()
        cmake.build()
