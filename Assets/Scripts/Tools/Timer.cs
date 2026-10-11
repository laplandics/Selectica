using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    public static class Timer
    {
        private static readonly List<Action> OnTick = new();
        
        public static void Run()
        {
            OnTick.Clear();
            var sceneTimer = new GameObject("[TIMER]").AddComponent<SceneTimer>();
            Object.DontDestroyOnLoad(sceneTimer);
            sceneTimer.Activate(Tick);
        }

        public static void RegisterOnTick(Action tickAction) { OnTick.Add(tickAction); }
        private static void Tick() { OnTick.ForEach(t => t?.Invoke()); }
    }

    public class SceneTimer : MonoBehaviour
    {
        private Action _tick;
        private bool _isActive;
        
        public void Activate(Action tickAction) { _tick = tickAction; _isActive = true; }
        private void Update() { if (!_isActive) return; _tick.Invoke(); }
    }
}