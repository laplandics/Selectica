using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selectica
{
    public enum EntityContainerType { PlantTilesContainer, SoilTilesContainer, MachineryTilesContainer }
    
    public static class EntityContainersManager
    {
        private static readonly Dictionary<EntityContainerType, Transform> ContainersMap = new();

        public static void SetContainers()
        {
            ContainersMap.Clear();
            var containerTypes = Enum.GetValues(typeof(EntityContainerType));
            foreach (EntityContainerType containerType in containerTypes)
            {
                var containerName = containerType.ToString().Replace("TilesContainer", "").Trim();
                var container = new GameObject(containerName);
                ContainersMap.Add(containerType, container.transform);
            }
        }
        
        public static void MoveIntoContainer(EntityContainerType containerType, Transform transform)
        { transform.SetParent(ContainersMap[containerType], true); }
    }
}