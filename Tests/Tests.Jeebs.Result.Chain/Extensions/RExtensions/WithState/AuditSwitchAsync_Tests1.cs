﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests.WithState
{
	public partial class AuditSwitchAsync_Tests
	{
		[Fact]
		public void IOk_Input_When_IOk_Runs_Func()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			int sideEffect = 1;
			async Task a(IOk<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IOkV_Does_Nothing()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(value, state);
			int sideEffect = 1;
			async Task a(IOk<int, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_When_IError_Does_Nothing()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state).Error();
			int sideEffect = 1;
			async Task a(IOk<bool, int> _) => sideEffect++;

			// Act
			chain.AuditSwitchAsync(isOk: a).Await();

			// Assert
			Assert.Equal(1, sideEffect);
		}

		[Fact]
		public void IOk_Input_Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static async Task a<TValue, TState>(IOk<TValue, TState> _) => throw new Exception();

			// Act
			var next = chain.AuditSwitchAsync(a).Await();

			// Assert
			Assert.Equal(1, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.AuditAsync.AuditSwitchAsyncExceptionMsg>());
			Assert.Equal(state, next.State);
		}
	}
}
