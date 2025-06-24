using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSL.API.Extensions
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<Action> _mainQueue = new Queue<Action>();
        private static readonly Queue<Action> _nextFrameQueue = new Queue<Action>();

        private static MainThreadDispatcher _instance;
        private static bool _initialized;

        public static void Init()
        {
            if (_initialized) return;
            var go = new GameObject("MainThreadDispatcher");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<MainThreadDispatcher>();
            _initialized = true;
        }

        private void Update()
        {
            lock (_mainQueue)
            {
                while (_mainQueue.Count > 0)
                    _mainQueue.Dequeue()?.Invoke();
            }

            lock (_nextFrameQueue)
            {
                var list = new List<Action>(_nextFrameQueue);
                _nextFrameQueue.Clear();
                foreach (var action in list)
                    action?.Invoke();
            }
        }

        public static void Invoke(Action action)
        {
            if (!_initialized || action == null) return;
            lock (_mainQueue) _mainQueue.Enqueue(action);
        }

        public static void InvokeNextFrame(Action action)
        {
            if (!_initialized || action == null) return;
            lock (_nextFrameQueue) _nextFrameQueue.Enqueue(action);
        }

        public static Coroutine Start(IEnumerator coroutine)
        {
            return _initialized && _instance != null && coroutine != null
                ? _instance.StartCoroutine(coroutine)
                : null;
        }
    }
}
