# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 2.8

#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:

# Remove some rules from gmake that .SUFFIXES does not remove.
SUFFIXES =

.SUFFIXES: .hpux_make_needs_suffix_list

# Suppress display of executed commands.
$(VERBOSE).SILENT:

# A target that is always out of date.
cmake_force:
.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/local/bin/cmake

# The command to remove a file.
RM = /usr/local/bin/cmake -E remove -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build

# Include any dependencies generated for this target.
include Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/depend.make

# Include the progress variables for this target.
include Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/progress.make

# Include the compile flags for this target's objects.
include Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/flags.make

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/flags.make
Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o: ../Samples/BezierPatch/src/BezierPatch.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch && /usr/bin/c++   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o -c /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BezierPatch/src/BezierPatch.cpp

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.i"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -E /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BezierPatch/src/BezierPatch.cpp > CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.i

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.s"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -S /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BezierPatch/src/BezierPatch.cpp -o CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.s

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.requires:
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.requires

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.provides: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.requires
	$(MAKE) -f Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/build.make Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.provides.build
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.provides

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.provides.build: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o

# Object files for target Sample_BezierPatch
Sample_BezierPatch_OBJECTS = \
"CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o"

# External object files for target Sample_BezierPatch
Sample_BezierPatch_EXTERNAL_OBJECTS =

lib/Sample_BezierPatch.so: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o
lib/Sample_BezierPatch.so: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/build.make
lib/Sample_BezierPatch.so: lib/libOgreMain.so.1.8.1
lib/Sample_BezierPatch.so: lib/libOgreRTShaderSystem.so.1.8.1
lib/Sample_BezierPatch.so: /usr/lib/libOIS.so
lib/Sample_BezierPatch.so: lib/libOgreMain.so.1.8.1
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libfreetype.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libSM.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libICE.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libX11.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libXext.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libXt.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libXaw.so
lib/Sample_BezierPatch.so: /usr/lib/libboost_thread-mt.so
lib/Sample_BezierPatch.so: /usr/lib/libboost_date_time-mt.so
lib/Sample_BezierPatch.so: /usr/lib/libfreeimage.so
lib/Sample_BezierPatch.so: /usr/lib/libzzip.so
lib/Sample_BezierPatch.so: /usr/lib/i386-linux-gnu/libz.so
lib/Sample_BezierPatch.so: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX shared library ../../lib/Sample_BezierPatch.so"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/Sample_BezierPatch.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/build: lib/Sample_BezierPatch.so
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/build

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/requires: Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/src/BezierPatch.cpp.o.requires
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/requires

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/clean:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch && $(CMAKE_COMMAND) -P CMakeFiles/Sample_BezierPatch.dir/cmake_clean.cmake
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/clean

Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/depend:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1 /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BezierPatch /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : Samples/BezierPatch/CMakeFiles/Sample_BezierPatch.dir/depend
