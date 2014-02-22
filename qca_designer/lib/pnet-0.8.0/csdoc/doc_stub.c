/*
 * doc_texi.c - Convert csdoc into texinfo.
 *
 * Copyright (C) 2001  Southern Storm Software, Pty Ltd.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

#include <stdio.h>

#include <dirent.h>

#include "il_system.h"
#include "il_utils.h"
#include "il_meta.h"
#include "doc_tree.h"
#include "doc_backend.h"
#ifdef HAVE_SYS_TYPES_H
#include <sys/types.h>
#endif
#ifdef HAVE_SYS_STAT_H
#include <sys/stat.h>
#endif
#ifdef HAVE_UNISTD_H
#include <unistd.h>
#endif
#include <errno.h>

#ifdef	__cplusplus
extern	"C" {
#endif

char *license="\
/*\n\
 * %s.cs - Implementation of \"%s\" \n\
 *\n\
 * Copyright (C) 2003  Southern Storm Software, Pty Ltd.\n\
 * \n\
 * Autogenerated by csdoc2stub \n\
 *\n\
 * This program is free software; you can redistribute it and/or modify\n\
 * it under the terms of the GNU General Public License as published by\n\
 * the Free Software Foundation; either version 2 of the License, or\n\
 * (at your option) any later version.\n\
 *\n\
 * This program is distributed in the hope that it will be useful,\n\
 * but WITHOUT ANY WARRANTY; without even the implied warranty of\n\
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\n\
 * GNU General Public License for more details.\n\
 *\n\
 * You should have received a copy of the GNU General Public License\n\
 * along with this program; if not, write to the Free Software\n\
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA\n\
 */\n\n";

/*********END GOPAL's H4cks**************/
/*#define CSDOC_STUB_DEBUG */	
char const ILDocProgramHeader[] =
	"CSDOC2STUB " VERSION " - Convert C# documentation into Stub C#";

char const ILDocProgramName[] = "CSDOC2STUB";

ILCmdLineOption const ILDocProgramOptions[] = {
	{"-fclass",'c',1,
			"-fclass=CLASS",
			"Specify class to stub out as -fclass=CLASS"},
	{"-fnamespace",'n',1,
			"-fnamespace=NS",
			"Specify namespace to stub out as -fnamespace=NS"},
	{0, 0, 0, 0, 0}
};

char *ILDocDefaultOutput(int numInputs, char **inputs, const char *progname)
{
	return ".";
}

int ILDocValidateOutput(char *outputPath, const char *progname)
{
	/* Nothing to do here: any pathname is considered valid */
	return 1;
}
static void PrintThrowNotImplemented(FILE *fp, char * name, char * indent)
{
		fprintf(fp,"%s{\n",indent);
		fprintf(fp,"%s\tthrow new NotImplementedException(\"%s\");\n",
						indent,name);
		fprintf(fp,"%s}\n\n",indent);
}
void ILDocPrintMember(FILE *fp,ILDocType *type,ILDocMember *member)
{
	int noBody= (type->kind == ILDocTypeKind_Interface) | 
				(member->memberAttrs & IL_META_METHODDEF_ABSTRACT) 
					;
	char * sig=strdup(member->csSignature);
	if(member->memberType == ILDocMemberType_Constructor || member->memberType ==
					ILDocMemberType_Method)
	{
		sig[strlen(sig)-1]='\0';
		if(!noBody)
		{
			fprintf(fp,"\t\t[TODO]\n");
		}
		if(!noBody)
		{
			fprintf(fp,"\t\t%s\n",sig);
			PrintThrowNotImplemented(fp, member->name, "\t\t");
		}
		else
		{
			fprintf(fp,"\t\t%s;\n\n",sig);
		}
	}
	else if(member->memberType==ILDocMemberType_Property)
	{
		if(noBody)
		{
			fprintf(fp,"\t\t%s\n",sig);
		}
		else
		{
			fprintf(fp,"\t\t[TODO]\n\t\t");
			while(*sig)
			{
				if(!strncmp(sig,"get;",4))
				{
					fprintf(fp,"\t\t\tget\n");
					PrintThrowNotImplemented(fp, member->name, "\t\t\t");
					sig=sig+4;
				}
				if(!strncmp(sig,"set;",4))
				{
					fprintf(fp,"\t\t\tset\n");
					PrintThrowNotImplemented(fp, member->name, "\t\t\t");
					sig=sig+4;
				}
				else
				{
					if(*sig == '{')
					{
						fputs("\n\t\t{\n",fp);
					}
					else if(*sig == '}')
					{
						fputs("\t\t}\n",fp);
					}
					else
					{
						fputc(*sig,fp);
					}
					sig++;
				}
			}
		}
		fputc('\n',fp);	
	}
	else fprintf(fp,"\t\t%s;\n\n",sig);
}
void ILDocPrintType(ILDocNamespace *ns, ILDocType *type,char *name)
{
	ILDocMember *i;
	FILE *fp;
	char *fname=(char*)calloc(strlen(name)+strlen(type->name)+10,sizeof(char));
	sprintf(fname,"%s/%s.cs",name,type->name);
	fp=fopen(fname,"w");
	if(!fp)
	{
		fprintf(stderr,"Could not open %s\n",fname);
		return;
	}
	printf("\twriting %s\n",fname);
	fprintf(fp,license,type->name,type->fullName);
	fprintf(fp,"namespace %s\n",ns->name);
	fprintf(fp,"{\n");
	fprintf(fp,"\t%s\n",type->csSignature);
	fprintf(fp,"\t{\n");
	
	for(i=type->members;i!=NULL;i=i->next)
		ILDocPrintMember(fp,type,i);
	
	fprintf(fp,"\t}\n");
	fprintf(fp,"}//namespace");
	fclose(fp);
	free(fname);
}

void ILDocPrintNS(ILDocNamespace *ns,char *outputPath)
{
	ILDocType *i;
	int j;
	char *name=(char*)calloc(strlen(outputPath)+strlen(ns->name)+10,sizeof(char));
	strcat(name,outputPath);
	if(name && strlen(name) && name[strlen(name)-1]!='/')
	{
		name[strlen(name)]='/';
	}
	strcat(name,ns->name);
	for(j=strlen(outputPath)-1;j<strlen(name);j++)
	{
			if(name[j]=='.')
			{
				name[j]='\0';
			#ifdef IL_WIN32_NATIVE
				mkdir(name);
			#else
				mkdir(name,0777);
			#endif
				//create a new dir every time we correct a NS
				name[j]='/';
			}
	}
#ifdef CSDOC_STUB_DEBUG
	printf("Processing %s namespace\n",name);
#endif
#ifdef IL_WIN32_NATIVE
	mkdir(name);
#else
	mkdir(name,0777);//make dir if still not made
#endif
	for(i=ns->types;i!=NULL;i=i->nextNamespace)
	{
		ILDocPrintType(ns,i,name);
	}
	free(name);
}

int ILDocPrintClass(ILDocNamespace *ns,char  *klassname,char *outputPath)
{
	char *namespace=strdup(klassname);
	ILDocNamespace *n=ns;
	ILDocType *j;
	int i;
	for(i=strlen(namespace)-1;i>=0;i--)
	{
		if(namespace[i]=='.')
		{
			namespace[i]='\0';//split off last entries
			break;
		}
	}
	while(n!=NULL)
	{
		if(!strcmp(n->name,namespace))
		{
			j=n->types;
			while(j!=NULL)
			{
				if(!strcmp(klassname,j->fullName))
				{
					ILDocPrintType(n,j,outputPath);
					return 1;
				}
				j=j->nextNamespace;
			}
		}
		n=n->next;
	}
	fprintf(stderr,"Class %s not found in the XML file \n",klassname);
	return 0;
}

int ILDocConvert(ILDocTree *tree, int numInputs, char **inputs,
				 char *outputPath, const char *progname)
{
	ILDocNamespace *i=tree->namespaces;
	char *klass=(char*)ILDocFlagValue("class");
	char *ns=(char*)ILDocFlagValue("namespace");
	if(!outputPath || !strcmp(outputPath,"."))
	{
		outputPath=getcwd(NULL,0);
	}
	if(klass!=NULL)
	{
		return	ILDocPrintClass(i,klass,outputPath);
	}
	if(ns!=NULL)
	{
		while(i!=NULL)
		{
			if(!strcmp(i->name,ns))
			{
				ILDocPrintNS(i,outputPath);
				return 1;
			}
			i=i->next;
		}
		fprintf(stderr," Namespace %s not found in XML file\n",ns);
		return 0;
	}
	else 
	{
		while(i!=NULL)
		{
			ILDocPrintNS(i,outputPath);
			i=i->next;
		}
	}
	return 1;
}

#ifdef	__cplusplus
};
#endif