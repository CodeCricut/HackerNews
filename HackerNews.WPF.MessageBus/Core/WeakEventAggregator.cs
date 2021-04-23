using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HackerNews.WPF.MessageBus.Core
{
	public class WeakEventAggregator : IEventAggregator
	{
		class WeakAction
		{
			private WeakReference weakReference;
			public WeakAction(object action)
			{
				weakReference = new WeakReference(action);
			}

			public bool IsAlive
			{
				get { return weakReference.IsAlive; }
			}

			public void Execute<TEvent>(TEvent param)
			{
				var action = (Action<TEvent>)weakReference.Target;
				action.Invoke(param);
			}
		}

		/// <summary>
		/// SynchronizationContext used to transition to the correct thread
		/// </summary>
		private readonly SynchronizationContext mSynchronizationContext;

		private readonly ConcurrentDictionary<Type, List<WeakAction>> subscriptions
			= new ConcurrentDictionary<Type, List<WeakAction>>();

		/// <summary>
		/// Initializes a new instance of the WeakEventAggregator class.
		/// </summary>
		public WeakEventAggregator()
		{
			mSynchronizationContext = SynchronizationContext.Current;
		}

		public void PostMessage<T>(T message)
		{
			if (message == null)
			{
				return;
			}

			if (mSynchronizationContext != null)
			{
				mSynchronizationContext.Post(
					m => Dispatch<T>((T)m),
					message);
			}
			else
			{
				Dispatch(message);
			}
		}

		public Action<T> RegisterHandler<T>(Action<T> eventHandler)
		{
			var subscribers = subscriptions.GetOrAdd(typeof(T), t => new List<WeakAction>());
			subscribers.Add(new WeakAction(eventHandler));

			return eventHandler;
		}

		public void SendMessage<T>(T message)
		{
			if (message == null)
			{
				return;
			}

			if (mSynchronizationContext != null)
			{
				mSynchronizationContext.Send(
					m => Dispatch<T>((T)m),
					message);
			}
			else
			{
				Dispatch(message);
			}
		}

		public void UnregisterHandler<T>(Action<T> eventHandler)
		{
			// TODO: probably completely wrong.
			subscriptions.TryGetValue(typeof(T), out var subscribers);
			
			subscribers.Remove(new WeakAction(eventHandler));
		}

		/// <summary>
		/// Dispatch a message to all appropriate handlers
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="message">Message to dispatch to registered handlers</param>
		private void Dispatch<T>(T message)
		{
			List<WeakAction> subscribers;
			if (subscriptions.TryGetValue(typeof(T), out subscribers))
			{
				subscribers.RemoveAll(x => !x.IsAlive);
				subscribers.ForEach(x => x.Execute<T>(message));
			}
		}
	}
}
