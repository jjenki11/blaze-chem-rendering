thisdir = tools/wsdl
SUBDIRS = 
include ../../build/rules.make

LOCAL_MCS_FLAGS = /r:System.Xml.dll /r:System.Web.Services.dll

ifeq (net_2_0, $(PROFILE))
PROGRAM = wsdl2.exe
else
PROGRAM = wsdl.exe
endif

EXTRA_DISTFILES = wsdl2.exe.sources $(shell cat wsdl2.exe.sources)

include ../../build/executable.make
