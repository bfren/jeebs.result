﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class AddRange_Tests
	{
		[Fact]
		public void Does_Nothing_When_No_Messages_Given()
		{
			// Arrange
			var l = new MsgList();

			// Act
			var exception = Record.Exception(() => l.AddRange());

			// Assert
			Assert.Null(exception);
			Assert.Equal(0, l.Count);
		}

		[Fact]
		public void Adds_Messages_To_List()
		{
			// Arrange
			var l = new MsgList();

			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var expected = new List<string>(new[]
			{
				string.Format(Jm.WithValueMsg.Format, nameof(StringMsg), m0.Value),
				string.Format(Jm.WithValueMsg.Format, nameof(StringMsg), m1.Value)
			});

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(2, l.Count);
			Assert.Equal(expected, l.GetAll());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
