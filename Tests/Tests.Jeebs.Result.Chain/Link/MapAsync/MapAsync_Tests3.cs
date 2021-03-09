﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class MapAsync_Tests
	{
		[Fact]
		public void IOk_Value_Input_When_IOk_Maps_To_Next_Type()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			static async Task<IR<string>> f(IOkV<int> r) => r.Ok<string>();

			// Act
			var next = chain.Link().MapAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IOk<string>>(next);
		}

		[Fact]
		public void IOk_Value_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			var error = F.Rnd.Str;
			async Task<IR<string>> f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().MapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			var error = Chain<int>.Create().Error();
			static async Task<IR<int>> f(IOkV<int> _) => throw new Exception();

			// Act
			var next = error.Link().MapAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
