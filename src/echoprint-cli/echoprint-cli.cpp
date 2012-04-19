#include "stdafx.h"

#include "echoprint-cli.h"

namespace echoprintcli {
	String^ CodegenCLI::getCodeString(const float* pcm, int samples, int start_offset){
		Codegen* codegen = new Codegen(pcm, samples, start_offset);
		std::string code = codegen->getCodeString();
		delete codegen;


		return String::Empty;
	}
}