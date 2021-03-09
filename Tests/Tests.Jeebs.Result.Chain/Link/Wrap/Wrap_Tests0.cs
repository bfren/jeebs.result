﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Wrap_Tests : ILink_Wrap
	{
		[Fact]
		public void Value_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var r = Chain.Create();

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			var value = F.Rnd.Int;
			var r = Chain.Create().Error();

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
		}
	}
}
