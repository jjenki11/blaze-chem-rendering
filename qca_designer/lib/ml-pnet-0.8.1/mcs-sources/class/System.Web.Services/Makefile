thisdir = class/System.Web.Services
SUBDIRS = 
include ../../build/rules.make

LIBRARY = System.Web.Services.dll
LIB_MCS_FLAGS = \
	/nowarn:649 /nowarn:169			\
	/r:$(corlib)				\
	/r:System.dll				\
	/r:System.EnterpriseServices.dll	\
	/r:System.Xml.dll			\
	/r:System.Web.dll
TEST_MCS_FLAGS = $(LIB_MCS_FLAGS) -nowarn:618

include ../../build/library.make
