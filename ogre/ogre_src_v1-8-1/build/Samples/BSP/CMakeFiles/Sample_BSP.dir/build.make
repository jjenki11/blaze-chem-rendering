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
include Samples/BSP/CMakeFiles/Sample_BSP.dir/depend.make

# Include the progress variables for this target.
include Samples/BSP/CMakeFiles/Sample_BSP.dir/progress.make

# Include the compile flags for this target's objects.
include Samples/BSP/CMakeFiles/Sample_BSP.dir/flags.make

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o: Samples/BSP/CMakeFiles/Sample_BSP.dir/flags.make
Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o: ../Samples/BSP/src/BSP.cpp
	$(CMAKE_COMMAND) -E cmake_progress_report /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/CMakeFiles $(CMAKE_PROGRESS_1)
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Building CXX object Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP && /usr/bin/c++   $(CXX_DEFINES) $(CXX_FLAGS) -o CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o -c /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BSP/src/BSP.cpp

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/Sample_BSP.dir/src/BSP.cpp.i"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -E /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BSP/src/BSP.cpp > CMakeFiles/Sample_BSP.dir/src/BSP.cpp.i

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/Sample_BSP.dir/src/BSP.cpp.s"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP && /usr/bin/c++  $(CXX_DEFINES) $(CXX_FLAGS) -S /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BSP/src/BSP.cpp -o CMakeFiles/Sample_BSP.dir/src/BSP.cpp.s

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.requires:
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.requires

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.provides: Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.requires
	$(MAKE) -f Samples/BSP/CMakeFiles/Sample_BSP.dir/build.make Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.provides.build
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.provides

Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.provides.build: Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o

# Object files for target Sample_BSP
Sample_BSP_OBJECTS = \
"CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o"

# External object files for target Sample_BSP
Sample_BSP_EXTERNAL_OBJECTS =

lib/Sample_BSP.so: Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o
lib/Sample_BSP.so: Samples/BSP/CMakeFiles/Sample_BSP.dir/build.make
lib/Sample_BSP.so: lib/libOgreMain.so.1.8.1
lib/Sample_BSP.so: lib/libOgreRTShaderSystem.so.1.8.1
lib/Sample_BSP.so: /usr/lib/libOIS.so
lib/Sample_BSP.so: lib/libOgreMain.so.1.8.1
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libfreetype.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libSM.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libICE.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libX11.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libXext.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libXt.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libXaw.so
lib/Sample_BSP.so: /usr/lib/libboost_thread-mt.so
lib/Sample_BSP.so: /usr/lib/libboost_date_time-mt.so
lib/Sample_BSP.so: /usr/lib/libfreeimage.so
lib/Sample_BSP.so: /usr/lib/libzzip.so
lib/Sample_BSP.so: /usr/lib/i386-linux-gnu/libz.so
lib/Sample_BSP.so: Samples/BSP/CMakeFiles/Sample_BSP.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --red --bold "Linking CXX shared library ../../lib/Sample_BSP.so"
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP && $(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/Sample_BSP.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
Samples/BSP/CMakeFiles/Sample_BSP.dir/build: lib/Sample_BSP.so
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/build

Samples/BSP/CMakeFiles/Sample_BSP.dir/requires: Samples/BSP/CMakeFiles/Sample_BSP.dir/src/BSP.cpp.o.requires
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/requires

Samples/BSP/CMakeFiles/Sample_BSP.dir/clean:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP && $(CMAKE_COMMAND) -P CMakeFiles/Sample_BSP.dir/cmake_clean.cmake
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/clean

Samples/BSP/CMakeFiles/Sample_BSP.dir/depend:
	cd /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1 /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Samples/BSP /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/Samples/BSP/CMakeFiles/Sample_BSP.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : Samples/BSP/CMakeFiles/Sample_BSP.dir/depend

