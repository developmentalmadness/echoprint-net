// echoprint-cli.h

#pragma once

#include "Codegen.h";

using namespace System;

namespace echoprintcli {

	public ref class CodegenCLI
	{
		public:
			String^ getCodeString(const float* pcm, int samples, int start_offset);
	};
}
