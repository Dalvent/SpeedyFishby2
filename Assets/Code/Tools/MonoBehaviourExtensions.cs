using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Tools
{
    public static class MonoBehaviourExtensions
    {
        public static void StartCoroutineWithCallback(this MonoBehaviour monoBehaviour, IEnumerator coroutine, Action callback)
        {
            monoBehaviour.StartCoroutine(RunCoroutine(monoBehaviour, coroutine, callback));
        }

        public static void StartCoroutinesWithCallback(this MonoBehaviour monoBehaviour, IEnumerator[] coroutines, Action callback)
        {
            monoBehaviour.StartCoroutine(RunCoroutines(monoBehaviour, coroutines, callback));
        }

        public static Task StartCoroutineWithCallbackAsync(this MonoBehaviour monoBehaviour, IEnumerator coroutine)
        {
            var completionSource = new TaskCompletionSource<bool>();
            monoBehaviour.StartCoroutine(RunCoroutine(monoBehaviour, coroutine, () => completionSource.SetResult(true)));
            return completionSource.Task;
        }

        public static Task StartCoroutinesWithCallbackAsync(this MonoBehaviour monoBehaviour, IEnumerator[] coroutines)
        {
            var completionSource = new TaskCompletionSource<bool>();
            monoBehaviour.StartCoroutine(RunCoroutines(monoBehaviour, coroutines, () => completionSource.SetResult(true)));
            return completionSource.Task;
        }

        private static IEnumerator RunCoroutine(MonoBehaviour monoBehaviour, IEnumerator coroutine, Action callback)
        {
            yield return monoBehaviour.StartCoroutine(coroutine);
            callback.Invoke();
        }
        
        private static IEnumerator RunCoroutines(MonoBehaviour monoBehaviour, IEnumerator[] coroutines, Action callback)
        {
            var started = new List<Coroutine>();
            foreach (var coroutine in coroutines)
            {
                started.Add(monoBehaviour.StartCoroutine(coroutine));
            }
            foreach (var coroutine in started)
            {
                yield return coroutine;
            }
            callback.Invoke();
        }
    }
}