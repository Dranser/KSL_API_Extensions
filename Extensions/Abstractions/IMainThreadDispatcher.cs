using System;
using System.Collections;
using UnityEngine;

namespace KSL.API.Extensions
{
    public interface IMainThreadDispatcher
    {
        void Invoke(Action action);
        void InvokeNextFrame(Action action);
        Coroutine Start(IEnumerator coroutine);
    }
}
