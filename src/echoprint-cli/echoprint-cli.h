// echoprint-cli.h

#pragma once

#include "Codegen.h";

using namespace System;

namespace echoprintcli {

	public ref class CodegenCLI
	{
		public:
			String^ getCodeString(array<float>^ buffer, unsigned int samples, int start_offset);
	};
}
