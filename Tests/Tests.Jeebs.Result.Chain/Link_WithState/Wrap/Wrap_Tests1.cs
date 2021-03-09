﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class Wrap_Tests
	{
		[Fact]
		public void Func_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			int f() => value;
			var r = Chain.Create(state);

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void Func_Input_When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Chain.Create(state);
			const string error = "Ooops!";
			static int f() => throw new Exception(error);

			// Act
			var next = r.Link().Wrap(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Func_Input_When_IError_Returns_IError()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Chain.Create(state).Error();
			static int f() => throw new Exception();

			// Act
			var next = r.Link().Wrap(f);

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
		}
	}
}
