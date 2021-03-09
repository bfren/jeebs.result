﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ROk_Tests
{
	public class Ok_Tests : IOk_Ok
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			var r0 = Result.Ok();
			var r1 = Result.Ok<int>();

			// Act
			var n0 = r0.Ok();
			var n1 = r1.Ok();

			// Assert
			Assert.StrictEqual(r0, n0);
			Assert.StrictEqual(r1, n1);
		}

		[Fact]
		public void Same_Type_Returns_Original_Object()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var next = r.Ok<bool>();

			// Assert
			Assert.StrictEqual(r, next);
		}

		[Fact]
		public void Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new IntMsg(F.Rnd.Int);
			var m1 = new StringMsg(F.Rnd.Str);
			var r = Result.Ok().AddMsg(m0, m1);

			// Act
			var next = r.Ok<int>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<IntMsg>());
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		public class IntMsg : Jm.WithValueMsg<int>
		{
			public IntMsg(int value) : base(value) { }
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
