﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Wrap_Tests
	{
		[Fact]
		public void Func_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			int f() => value;
			var r = Chain.Create();

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Func_Input_When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			const string error = "Ooops!";
			static int f() => throw new Exception(error);
			var r = Chain.Create();

			// Act
			var next = r.Link().Wrap(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Func_Input_When_IError_Returns_IError()
		{
			// Arrange
			static int f() => throw new Exception();
			var r = Chain.Create().Error();

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
		}
	}
}
