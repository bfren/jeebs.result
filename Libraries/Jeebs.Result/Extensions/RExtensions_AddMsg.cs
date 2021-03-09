﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Diagnostics.CodeAnalysis;
using Jeebs.Fluent;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/> interface: AddMsg
	/// </summary>
	public static class RExtensions_AddMsg
	{
		/// <summary>
		/// Begins a fluent With operation, used to add message(s) to the result chain
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		public static AddMsg<TResult> AddMsg<TResult>(this TResult @this)
			where TResult : IR =>
			new(@this);

		/// <summary>
		/// Adds a message of type <typeparamref name="TMsg"/> to the result and returns original <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TMsg">Message type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="message">Message</param>
		public static TResult AddMsg<TResult, TMsg>(this TResult @this, TMsg message)
			where TResult : IR
			where TMsg : IMsg
		{
			@this.Logger.Message(message);
			@this.Messages.Add(message);
			return @this;
		}

		/// <summary>
		/// Adds messages to the result and returns original <typeparamref name="TResult"/> object
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="messages">Messages to add</param>
		public static TResult AddMsg<TResult>(this TResult @this, [NotNull] params IMsg[] messages)
			where TResult : IR
		{
			foreach (var message in messages)
			{
				@this.Logger.Message(message);
			}

			@this.Messages.AddRange(messages);
			return @this;
		}
	}
}
