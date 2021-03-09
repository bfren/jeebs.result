﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ROk_Tests
{
	public class WithState_Tests : IOk_WithState
	{
		[Fact]
		public void Returns_Ok_With_State()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r0 = Result.Ok();
			var r1 = Result.Ok<string>();

			// Act
			var n0 = r0.WithState(state);
			var n1 = r1.WithState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(n0);
			Assert.Equal(state, n0.State);
			Assert.IsAssignableFrom<IOk<string, int>>(n1);
			Assert.Equal(state, n1.State);
		}

		[Fact]
		public void Returns_Ok_With_State_And_Keeps_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var result = Result.OkV(value);

			// Act
			var next = result.WithState(state);

			// Assert
			Assert.Equal(value, next.Value);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Returns_Ok_With_State_And_Keeps_Messages()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok();
			r.AddMsg(new StringMsg(F.Rnd.Str));

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
