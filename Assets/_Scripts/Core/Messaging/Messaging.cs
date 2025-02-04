using System;
using System.Collections.Generic;

namespace EnvizTest.Core.Messaging
{
    public delegate void SendMessage<T>(T data);

    public delegate void SendMessage();

    public static class Messaging
    {
        static object _dataLock = new() { };

        static Dictionary<Enum, List<object>> _dataStore = new() { };

        /// <summary>
        ///     Adds a listener to the end of the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event to add the listener to.</param>
        /// <param name="listener">The listener to add.</param>
        public static void AddListener<T>(Enum name, SendMessage<T> listener)
        {
            lock (_dataLock)
            {
                if (!_dataStore.ContainsKey(name))
                {
                    _dataStore[name] = new List<object> { };
                }

                _dataStore[name].Add(listener);
            }
        }

        /// <summary>
        ///     Adds a listener to the end of the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event to add the listener to.</param>
        /// <param name="listener">The listener to add.</param>
        public static void AddListener(Enum name, SendMessage listener)
        {
            lock (_dataLock)
            {
                if (!_dataStore.ContainsKey(name))
                {
                    _dataStore[name] = new List<object> { };
                }

                _dataStore[name].Add(listener);
            }
        }

        /// <summary>
        ///     Remove the first occurrence of a specific listener from the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event to remove the listener from.</param>
        /// <returns>
        ///     true if item is successfully removed; otherwise, false. This method also returns false if item was not found
        ///     in the <paramref name="name" /> event.
        /// </returns>
        public static bool RemoveListener<T>(Enum name, SendMessage<T> listener)
        {
            lock (_dataLock)
            {
                if (!_dataStore.ContainsKey(name))
                {
                    _dataStore[name] = new List<object> { };
                }

                return _dataStore[name].Remove(listener);
            }
        }

        /// <summary>
        ///     Remove the first occurrence of a specific listener from the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event to remove the listener from.</param>
        /// <returns>
        ///     true if item is successfully removed; otherwise, false. This method also returns false if item was not found
        ///     in the <paramref name="name" /> event.
        /// </returns>
        public static bool RemoveListener(Enum name, SendMessage listener)
        {
            lock (_dataLock)
            {
                if (!_dataStore.ContainsKey(name))
                {
                    _dataStore[name] = new List<object> { };
                }

                return _dataStore[name].Remove(listener);
            }
        }

        /// <summary>
        ///     Remove all listeners for the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event to remove the listeners from.</param>
        public static void RemoveListeners(Enum name)
        {
            lock (_dataLock)
            {
                if (!_dataStore.ContainsKey(name))
                {
                    _dataStore[name] = new List<object> { };
                }

                _dataStore[name].Clear();
            }
        }

        /// <summary>
        ///     Sends a message to all listeners subscribed to the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="data">The message to send.</param>
        public static void SendMessage<TData>(Enum name, TData data)
        {
            SendMessageInternal<TData>(name, data);
        }

        /// <summary>
        ///     Sends a message to all listeners subscribed to the <paramref name="name" /> event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="data">The message to send.</param>
        public static void SendMessage(Enum name)
        {
            SendMessageInternal<object>(name, null);
        }

        static void SendMessageInternal<TData>(Enum name, TData data)
        {
            List<object> delegateList;

            if (_dataStore.TryGetValue(name, out delegateList))
            {
                var safeDelegateList = delegateList.ToArray();

                foreach (var listener in safeDelegateList)
                {
                    if (listener is SendMessage)
                    {
                        (listener as SendMessage)();
                    }

                    if (listener is SendMessage<TData>)
                    {
                        (listener as SendMessage<TData>)(data);
                    }
                }
            }
        }
    }
}