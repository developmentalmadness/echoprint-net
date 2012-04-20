#include "stdafx.h"
#include <msclr\marshal_cppstd.h>
#include "echoprint-cli.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace msclr::interop;

namespace echoprintcli {
	String^ CodegenCLI::getCodeString(array<float>^ buffer, unsigned int samples, int start_offset){
		String^ result = String::Empty;

		if(buffer->Length > 0){
			GCHandle h = GCHandle::Alloc(buffer, System::Runtime::InteropServices::GCHandleType::Pinned);
			Codegen* codegen = nullptr;

			try{
				float* pcm = (float*)(void*)h.AddrOfPinnedObject();
				codegen = new Codegen(pcm, samples, start_offset);
				std::string code = codegen->getCodeString();
				
				result = marshal_as<String^>(code);
			}
			finally{
				delete codegen;
				h.Free();
			}
		}
		return result;
	}
}