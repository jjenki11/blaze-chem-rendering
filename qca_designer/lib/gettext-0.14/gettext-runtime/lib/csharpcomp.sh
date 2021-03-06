#!/bin/sh
# Compile a C# program.

# Copyright (C) 2003-2004 Free Software Foundation, Inc.
# Written by Bruno Haible <bruno@clisp.org>, 2003.
#
# This program is free software; you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation; either version 2, or (at your option)
# any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software Foundation,
# Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.  */

# This uses the same choices as csharpcomp.c, but instead of relying on the
# environment settings at run time, it uses the environment variables
# present at configuration time.
#
# This is a separate shell script, because the various C# compilers have
# different command line options.
#
# Usage: /bin/sh csharpcomp.sh [OPTION] SOURCE.cs ... RES.resource ...
# Options:
#   -o PROGRAM.exe  or  -o LIBRARY.dll
#                     set the output assembly name
#   -L DIRECTORY      search for C# libraries also in DIRECTORY
#   -l LIBRARY        reference the C# library LIBRARY.dll
#   -O                optimize
#   -g                generate debugging information

sed_quote_subst='s/\([|&;<>()$`"'"'"'*?[#~=% 	\\]\)/\\\1/g'
options_cscc=
options_mcs=
options_csc="-nologo"
sources=
while test $# != 0; do
  case "$1" in
    -o)
      case "$2" in
        *.dll)
          options_cscc="$options_cscc -shared"
          options_mcs="$options_mcs -target:library"
          options_csc="$options_csc -target:library"
          ;;
        *.exe)
          options_csc="$options_csc -target:exe"
          ;;
      esac
      options_cscc="$options_cscc -o "`echo "$2" | sed -e "$sed_quote_subst"`
      options_mcs="$options_mcs -o "`echo "$2" | sed -e "$sed_quote_subst"`
      options_csc="$options_csc -out:"`echo "$2" | sed -e "$sed_quote_subst"`
      shift
      ;;
    -L)
      options_cscc="$options_cscc -L "`echo "$2" | sed -e "$sed_quote_subst"`
      options_mcs="$options_mcs -L "`echo "$2" | sed -e "$sed_quote_subst"`
      options_csc="$options_csc -lib:"`echo "$2" | sed -e "$sed_quote_subst"`
      shift
      ;;
    -l)
      options_cscc="$options_cscc -l "`echo "$2" | sed -e "$sed_quote_subst"`
      options_mcs="$options_mcs -r "`echo "$2" | sed -e "$sed_quote_subst"`
      options_csc="$options_csc -reference:"`echo "$2" | sed -e "$sed_quote_subst"`
      shift
      ;;
    -O)
      options_cscc="$options_cscc -O"
      options_csc="$options_csc -optimize+"
      ;;
    -g)
      options_cscc="$options_cscc -g"
      options_mcs="$options_mcs -g"
      options_csc="$options_csc -debug+"
      ;;
    -*)
      echo "csharpcomp: unknown option '$1'" 1>&2
      exit 1
      ;;
    *.resource)
      options_cscc="$options_cscc -fresources="`echo "$1" | sed -e "$sed_quote_subst"`
      options_mcs="$options_mcs -resource:"`echo "$1" | sed -e "$sed_quote_subst"`
      options_csc="$options_csc -resource:"`echo "$1" | sed -e "$sed_quote_subst"`
      ;;
    *.cs)
      sources="$sources "`echo "$1" | sed -e "$sed_quote_subst"`
      ;;
    *)
      echo "csharpcomp: unknown type of argument '$1'" 1>&2
      exit 1
      ;;
  esac
  shift
done

if test -n "1"; then
  test -z "$CSHARP_VERBOSE" || echo cscc $options_cscc $sources
  exec cscc $options_cscc $sources
else
  if test -n ""; then
    # mcs prints it errors and warnings to stdout, not stderr. Furthermore it
    # adds a useless line "Compilation succeeded..." at the end. Correct both.
    sed_drop_success_line='${
/^Compilation succeeded/d
}'
    tmpfile=`(mktemp "${TMPDIR-/tmp}/mcserrXXXXXXXX") 2>/dev/null || echo ${TMPDIR-/tmp}/mcserr$$`
    trap 'rm -f "$tmpfile"' 1 2 3 15
    test -z "$CSHARP_VERBOSE" || echo mcs $options_mcs $sources
    mcs $options_mcs $sources > "$tmpfile"
    result=$?
    sed -e "$sed_drop_success_line" < "$tmpfile" >&2
    rm -f "$tmpfile"
    exit $?
  else
    if test -n ""; then
      test -z "$CSHARP_VERBOSE" || echo csc $options_csc $sources
      exec csc $options_csc $sources
    else
      echo 'C# compiler not found, try installing pnet, then reconfigure' 1>&2
      exit 1
    fi
  fi
fi
