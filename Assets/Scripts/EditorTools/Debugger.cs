using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Selectica
{
    public static class Debugger
    {
        private static SceneDebugger _sceneDebugger;
        private static HashSet<Action> _onDrawGizmos;
        
        public static void Run()
        {
            _onDrawGizmos = new HashSet<Action>();
            _sceneDebugger = new GameObject("[DEBUGGER]").AddComponent<SceneDebugger>();
            Object.DontDestroyOnLoad(_sceneDebugger.gameObject);
        }

        public static void RegisterOnDrawGizmos(Action onDrawGizmos) => _onDrawGizmos.Add(onDrawGizmos);
        
        public static void CallOnDrawGizmos()
        { foreach (var action in _onDrawGizmos) { action?.Invoke(); } }
    }

    public class SceneDebugger : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.mediumVioletRed;
            Debugger.CallOnDrawGizmos();
        }
    }
}