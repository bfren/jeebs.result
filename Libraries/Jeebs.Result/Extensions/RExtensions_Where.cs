﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: Where
	/// </summary>
	public static class RExtensions_Where
	{
		/// <summary>
		/// Enables LINQ where on Result objects, e.g.
		/// <c>from x in Result</c>
		/// <c>where x == y</c>
		/// <c>select x</c>
		/// <para>NB: only works with <see cref="IOkV{TValue}"/> - otherwise will return <see cref="IError{TValue}"/></para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="predicate">Select where predicate</param>
		public static IR<TValue> Where<TValue>(this IR<TValue> @this, Func<TValue, bool> predicate) =>
			@this switch
			{
				IOkV<TValue> x =>
					predicate(x.Value) switch
					{
						true =>
							x,

						false =>
							@this.Error(),
					},

				_ =>
					@this.Error()
			};

		/// <summary>
		/// Enables LINQ where on Result objects, e.g.
		/// <c>from x in Result</c>
		/// <c>where x == y</c>
		/// <c>select x</c>
		/// <para>NB: only works with <see cref="IOkV{TValue, TState}"/> - otherwise will return <see cref="IError{TValue, TState}"/></para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="predicate">Select where predicate</param>
		public static IR<TValue, TState> Where<TValue, TState>(this IR<TValue, TState> @this, Func<TValue, bool> predicate) =>
			@this switch
			{
				IOkV<TValue, TState> x =>
					predicate(x.Value) switch
					{
						true =>
							x,

						false =>
							@this.Error(),
					},

				_ =>
					@this.Error()
			};
	}
}
