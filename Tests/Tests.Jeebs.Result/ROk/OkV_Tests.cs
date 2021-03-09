﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ROk_Tests
{
	public class OkV_Tests : IOk_OkV
	{
		[Fact]
		public void Returns_Object_With_Value()
		{
			// Arrange
			var r = Result.Ok();
			var value = F.Rnd.Int;

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, next.Value);
		}

		[Fact]
		public void Keeps_Messages()
		{
			// Arrange
			var value = F.Rnd.Int;
			var m0 = new IntMsg(F.Rnd.Int);
			var m1 = new StringMsg(F.Rnd.Str);
			var r = Result.Ok().AddMsg(m0, m1);

			// Act
			var next = r.OkV(value);

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
