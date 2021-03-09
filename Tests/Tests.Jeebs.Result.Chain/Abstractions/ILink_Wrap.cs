﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Link_Tests
{
	public interface ILink_Wrap
	{
		void Func_Input_When_IError_Returns_IError();
		void Func_Input_When_IOk_Catches_Exception_Returns_IError();
		void Func_Input_When_IOk_Wraps_Value();
		void Value_Input_When_IError_Returns_IError();
		void Value_Input_When_IOk_Wraps_Value();
	}
}