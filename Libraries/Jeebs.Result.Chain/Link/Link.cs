﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue}"/>
	public partial class Link<TValue> : ILink<TValue>, IDisposable
	{
		private readonly IR<TValue> result;

		private readonly LinkExceptionHandlers<IR<TValue>> handlers = new();

		internal Link(IR result, Func<Exception, IMsg>? exceptionMsg = null) : this(result.ChangeType().To<TValue>(), exceptionMsg) { }

		internal Link(IR<TValue> result, Func<Exception, IMsg>? exceptionMsg = null)
		{
			this.result = result;
			handlers = new LinkExceptionHandlers<IR<TValue>>(exceptionMsg);
		}

		/// <inheritdoc/>
		public void AddExceptionHandler<TException>(Action<IR<TValue>, TException> handler)
			where TException : Exception =>
			handlers.Add(handler);

		private IR<TNext> Catch<TNext>(Func<Task<IR<TNext>>> f)
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				//result.Log.Error(ex, "Link Error - check Exception for details");
				handlers.Handle(result, ex);
				return result.Error<TNext>();
			}
		}

		private IR<TNext> Catch<TNext>(Func<IR<TNext>> f) =>
			Catch(() => Task.FromResult(f()));

		/// <summary>
		/// Dispose of this Link - including result and handlers objects
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			handlers.Dispose();
		}
	}
}
