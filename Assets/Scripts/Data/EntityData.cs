using System;

namespace Selectica
{
    [Serializable]
    public struct EntityData
    {
        public string entityName;
        public ComponentData[] entityComponents;
    }
}