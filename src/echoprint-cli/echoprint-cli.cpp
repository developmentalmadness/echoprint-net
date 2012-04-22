	#include "stdafx.h"
	#include <msclr\marshal_cppstd.h>
	#include "echoprint-cli.h"

	using namespace System;
	using namespace System::Runtime::InteropServices;
	using namespace msclr::interop;

	namespace echoprintcli {

		String^ CodegenCLI::getCodeString(array<float>^ buffer, unsigned int samples, int start_offset){
			String^ result = String::Empty;

			pin_ptr<float> p = &buffer[0];
			pin_ptr<Codegen> codegen = new Codegen(p, samples, start_offset);

			try{
				std::string code = codegen->getCodeString();
				result = marshal_as<String^>(code);
			}finally{
				delete codegen;
				delete p;
			}

			return result;
		}
	}