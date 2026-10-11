using System;

namespace Selectica
{
    [Serializable]
    public class EntityComponent
    { public string tag; public string[] values; }
    
    [Serializable] public class CellBindingComponent : EntityComponent
    { public CellIndex CellIndex => CellIndex.Parse(values[0]); }
}