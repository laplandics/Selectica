using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Selectica
{
    public static class ComponentsRegistry
    {
        private static readonly Dictionary<string, Type> ComponentsTypesMap = new();
        private static bool Initialized { get; set; }
        
        public static void CacheComponents()
        {
            if (Initialized) return;
            
            var assemblies = UnityEngine.Assemblies.CurrentAssemblies.GetLoadedAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] typesInAssembly;
                try { typesInAssembly = assembly.GetTypes(); }
                catch (ReflectionTypeLoadException ex)
                { typesInAssembly = ex.Types.Where(t => t != null).ToArray(); }

                foreach (var type in typesInAssembly)
                {
                    if (type == null || type.IsAbstract || type.IsInterface) continue;
                    if (!type.IsSubclassOf(typeof(EntityComponent_d))) continue;

                    var key = type.Name;
                    if (ComponentsTypesMap.TryGetValue(key, out var existingType))
                    { Debug.LogError($"{nameof(ComponentsRegistry)} type {existingType.FullName} is already registered"); continue; }
                    
                    ComponentsTypesMap.Add(key, type);
                }
            }
            
            Initialized = true;
        }
        
        private static Type GetType(string key) => ComponentsTypesMap.TryGetValue(key, out var type)
            ? type : throw new KeyNotFoundException($"{nameof(ComponentsRegistry)} key '{key}' was not found.");

        public static EntityComponent_d CreateInstance(ComponentData data)
        { var type = GetType(data.componentName); return (EntityComponent_d)Activator.CreateInstance(type, data); }
    }
}