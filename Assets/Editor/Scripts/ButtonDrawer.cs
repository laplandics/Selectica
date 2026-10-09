using System;
using System.Collections.Generic;
using System.Reflection;
using Selectica.EditorTools;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class ButtonDrawer : Editor
{
    private const BindingFlags FLAGS =
        BindingFlags.Instance | BindingFlags.Static |
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.DeclaredOnly;

    private static void AddButtons(VisualElement root, Object[] targets)
    {
        if (targets == null || targets.Length == 0 || targets[0] == null) return;
        foreach (var method in GetButtonMethods(targets[0].GetType()))
            root.Add(CreateButton(method, targets));
    }
    
    private static List<MethodInfo> GetButtonMethods(Type type)
    {
        var result = new List<MethodInfo>();
 
        for (var t = type;
             t != null &&
             t != typeof(MonoBehaviour) &&
             t != typeof(ScriptableObject) &&
             t != typeof(Object);
             t = t.BaseType)
        {
            var declared = new List<MethodInfo>();
            foreach (var m in t.GetMethods(FLAGS))
            { if (m.IsDefined(typeof(ButtonAttribute), false)) declared.Add(m); }
            
            declared.Sort((a, b) => a.MetadataToken.CompareTo(b.MetadataToken));
            result.InsertRange(0, declared);
        }
 
        return result;
    }

    private static VisualElement CreateButton(MethodInfo method, Object[] targets)
    {
        if (method.GetParameters().Length > 0 || method.ContainsGenericParameters)
        {
            return new HelpBox(
                $"[Button] can be used only with parameterless methods: {method.Name}()",
                HelpBoxMessageType.Warning);
        }
        
        var label = ObjectNames.NicifyVariableName(method.Name);
        
        var button = new Button(() => Invoke(method, targets, label))
        { text = label };
        
        button.style.marginTop = 4;
        button.style.height = 24;
        button.focusable = false;
        
        return button;
    }
    
    private static void Invoke(MethodInfo method, Object[] targets, string label)
    {
        try
        {
            if (method.IsStatic) { method.Invoke(null, null); return; }
            foreach (var target in targets)
            {
                if (target == null) continue;
                Undo.RecordObject(target, label);
                method.Invoke(target, null);
                if (!Application.isPlaying) EditorUtility.SetDirty(target);
            }
        }
        catch (TargetInvocationException e)
        { Debug.LogException(e.InnerException ?? e); }
    }
    
    [CustomEditor(typeof(MonoBehaviour), true)] [CanEditMultipleObjects]
    public class ButtonMonoBehaviourEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            AddButtons(root, targets);
            return root;
        }
    }
    
    [CustomEditor(typeof(ScriptableObject), true)] [CanEditMultipleObjects]
    public class ButtonScriptableObjectEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            AddButtons(root, targets);
            return root;
        }
    }
}