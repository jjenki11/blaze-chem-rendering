# Install script for directory: /home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain

# Set the install prefix
IF(NOT DEFINED CMAKE_INSTALL_PREFIX)
  SET(CMAKE_INSTALL_PREFIX "/usr/local")
ENDIF(NOT DEFINED CMAKE_INSTALL_PREFIX)
STRING(REGEX REPLACE "/$" "" CMAKE_INSTALL_PREFIX "${CMAKE_INSTALL_PREFIX}")

# Set the install configuration name.
IF(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)
  IF(BUILD_TYPE)
    STRING(REGEX REPLACE "^[^A-Za-z0-9_]+" ""
           CMAKE_INSTALL_CONFIG_NAME "${BUILD_TYPE}")
  ELSE(BUILD_TYPE)
    SET(CMAKE_INSTALL_CONFIG_NAME "RelWithDebInfo")
  ENDIF(BUILD_TYPE)
  MESSAGE(STATUS "Install configuration: \"${CMAKE_INSTALL_CONFIG_NAME}\"")
ENDIF(NOT DEFINED CMAKE_INSTALL_CONFIG_NAME)

# Set the component getting installed.
IF(NOT CMAKE_INSTALL_COMPONENT)
  IF(COMPONENT)
    MESSAGE(STATUS "Install component: \"${COMPONENT}\"")
    SET(CMAKE_INSTALL_COMPONENT "${COMPONENT}")
  ELSE(COMPONENT)
    SET(CMAKE_INSTALL_COMPONENT)
  ENDIF(COMPONENT)
ENDIF(NOT CMAKE_INSTALL_COMPONENT)

# Install shared libraries without execute permission?
IF(NOT DEFINED CMAKE_INSTALL_SO_NO_EXE)
  SET(CMAKE_INSTALL_SO_NO_EXE "1")
ENDIF(NOT DEFINED CMAKE_INSTALL_SO_NO_EXE)

IF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  IF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee]|[Nn][Oo][Nn][Ee]|)$")
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_CHECK
             FILE "${file}"
             RPATH "")
      ENDIF()
    ENDFOREACH()
    FILE(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib" TYPE SHARED_LIBRARY FILES
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so.1.8.1"
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so"
      )
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_REMOVE
             FILE "${file}")
        IF(CMAKE_INSTALL_DO_STRIP)
          EXECUTE_PROCESS(COMMAND "/usr/bin/strip" "${file}")
        ENDIF(CMAKE_INSTALL_DO_STRIP)
      ENDIF()
    ENDFOREACH()
  ENDIF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Rr][Ee][Ll][Ee][Aa][Ss][Ee]|[Nn][Oo][Nn][Ee]|)$")
ENDIF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")

IF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  IF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_CHECK
             FILE "${file}"
             RPATH "")
      ENDIF()
    ENDFOREACH()
    FILE(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib" TYPE SHARED_LIBRARY FILES
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so.1.8.1"
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so"
      )
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_REMOVE
             FILE "${file}")
        IF(CMAKE_INSTALL_DO_STRIP)
          EXECUTE_PROCESS(COMMAND "/usr/bin/strip" "${file}")
        ENDIF(CMAKE_INSTALL_DO_STRIP)
      ENDIF()
    ENDFOREACH()
  ENDIF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Rr][Ee][Ll][Ww][Ii][Tt][Hh][Dd][Ee][Bb][Ii][Nn][Ff][Oo])$")
ENDIF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")

IF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  IF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_CHECK
             FILE "${file}"
             RPATH "")
      ENDIF()
    ENDFOREACH()
    FILE(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib" TYPE SHARED_LIBRARY FILES
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so.1.8.1"
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so"
      )
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_REMOVE
             FILE "${file}")
        IF(CMAKE_INSTALL_DO_STRIP)
          EXECUTE_PROCESS(COMMAND "/usr/bin/strip" "${file}")
        ENDIF(CMAKE_INSTALL_DO_STRIP)
      ENDIF()
    ENDFOREACH()
  ENDIF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Mm][Ii][Nn][Ss][Ii][Zz][Ee][Rr][Ee][Ll])$")
ENDIF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")

IF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  IF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_CHECK
             FILE "${file}"
             RPATH "")
      ENDIF()
    ENDFOREACH()
    FILE(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/lib" TYPE SHARED_LIBRARY FILES
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so.1.8.1"
      "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/build/lib/libOgreTerrain.so"
      )
    FOREACH(file
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so.1.8.1"
        "$ENV{DESTDIR}${CMAKE_INSTALL_PREFIX}/lib/libOgreTerrain.so"
        )
      IF(EXISTS "${file}" AND
         NOT IS_SYMLINK "${file}")
        FILE(RPATH_REMOVE
             FILE "${file}")
        IF(CMAKE_INSTALL_DO_STRIP)
          EXECUTE_PROCESS(COMMAND "/usr/bin/strip" "${file}")
        ENDIF(CMAKE_INSTALL_DO_STRIP)
      ENDIF()
    ENDFOREACH()
  ENDIF("${CMAKE_INSTALL_CONFIG_NAME}" MATCHES "^([Dd][Ee][Bb][Uu][Gg])$")
ENDIF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")

IF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
  FILE(INSTALL DESTINATION "${CMAKE_INSTALL_PREFIX}/include/OGRE/Terrain" TYPE FILE FILES
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrain.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainGroup.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainLayerBlendMap.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainMaterialGenerator.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainMaterialGeneratorA.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainPrerequisites.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainQuadTreeNode.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainPaging.h"
    "/home/jeff/dev/dev_apps/sand/ogre/ogre_src_v1-8-1/Components/Terrain/include/OgreTerrainPagedWorldSection.h"
    )
ENDIF(NOT CMAKE_INSTALL_COMPONENT OR "${CMAKE_INSTALL_COMPONENT}" STREQUAL "Unspecified")
