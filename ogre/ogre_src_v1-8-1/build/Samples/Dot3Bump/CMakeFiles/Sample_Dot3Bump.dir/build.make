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
include Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/depend.make

# Include the progress variables for this target.
include Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/progress.make

# Include the compile flags for this target's objects.
include Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/flags.make

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/flags.make
Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o: ../Samples/Dot3Bump/src/Dot3Bump.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump && /usr/bin/c++   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o -c /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/Dot3Bump/src/Dot3Bump.cpp

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.i"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -E /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/Dot3Bump/src/Dot3Bump.cpp > CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.i

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.s"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -S /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/Dot3Bump/src/Dot3Bump.cpp -o CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.s

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.requires:
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.requires

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.provides: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.requires
	$(MAKE) -f Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/build.make Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.provides.build
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.provides

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.provides.build: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o

# Object files for target Sample_Dot3Bump
Sample_Dot3Bump_OBJECTS = \
"CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o"

# External object files for target Sample_Dot3Bump
Sample_Dot3Bump_EXTERNAL_OBJECTS =

lib/Sample_Dot3Bump.so: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o
lib/Sample_Dot3Bump.so: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/build.make
lib/Sample_Dot3Bump.so: lib/libOgreMain.so.1.8.1
lib/Sample_Dot3Bump.so: lib/libOgreRTShaderSystem.so.1.8.1
lib/Sample_Dot3Bump.so: /usr/lib/libOIS.so
lib/Sample_Dot3Bump.so: lib/libOgreMain.so.1.8.1
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libfreetype.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libSM.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libICE.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libX11.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libXext.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libXt.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libXaw.so
lib/Sample_Dot3Bump.so: /usr/lib/libboost_thread-mt.so
lib/Sample_Dot3Bump.so: /usr/lib/libboost_date_time-mt.so
lib/Sample_Dot3Bump.so: /usr/lib/libfreeimage.so
lib/Sample_Dot3Bump.so: /usr/lib/libzzip.so
lib/Sample_Dot3Bump.so: /usr/lib/i386-linux-gnu/libz.so
lib/Sample_Dot3Bump.so: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX shared library ../../lib/Sample_Dot3Bump.so"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/Sample_Dot3Bump.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/build: lib/Sample_Dot3Bump.so
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/build

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/requires: Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/src/Dot3Bump.cpp.o.requires
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/requires

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/clean:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump && $(CMAKE_COMMAND) -P CMakeFiles/Sample_Dot3Bump.dir/cmake_clean.cmake
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/clean

Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/depend:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1 /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/Dot3Bump /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : Samples/Dot3Bump/CMakeFiles/Sample_Dot3Bump.dir/depend

