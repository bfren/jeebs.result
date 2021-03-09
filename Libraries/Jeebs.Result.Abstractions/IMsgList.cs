﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs
{
	/// <summary>
	/// Persists a list of messages (of type <see cref="IMsg"/>) in the result chain
	/// </summary>
	public interface IMsgList
	{
		/// <summary>
		/// The number of messages
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Clear all messages
		/// </summary>
		void Clear();

		/// <summary>
		/// Add a single message of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		void Add<TMsg>() where TMsg : IMsg, new();

		/// <summary>
		/// Add a single message
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		/// <param name="message">The message to add</param>
		void Add<TMsg>(TMsg message) where TMsg : IMsg;

		/// <summary>
		/// Add a range of messages
		/// </summary>
		/// <param name="add">Array of messages</param>
		void AddRange(params IMsg[] add);

		/// <summary>
		/// Returns whether or not the message list contains at least one message of type <typeparamref name="TMsg"/>
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		bool Contains<TMsg>() where TMsg : IMsg;

		/// <summary>
		/// Get matching messages
		/// </summary>
		/// <typeparam name="TMsg">IMsg type</typeparam>
		List<TMsg> Get<TMsg>() where TMsg : IMsg;

		/// <summary>
		/// Get all message values
		/// </summary>
		/// <param name="withType">[Optional] If true, will include the message type as well</param>
		List<string> GetAll(bool withType = false);

		/// <summary>
		/// Returns the messages list as an Enumerable
		/// </summary>
		IEnumerable<IMsg> GetEnumerable();

		/// <summary>
		/// Return all message values on new lines - or default <see cref="ToString(bool)"/> if there are no messages
		/// </summary>
		/// <param name="withType">If true, will include the message type as well</param>
		string ToString(bool withType);
	}
}
