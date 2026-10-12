using System;
using UnityEngine;

namespace Selectica
{
    [Serializable]
    public class Cell
    {
        public CellIndex index;
        [SerializeReference] public ReactiveList<string> entities = new();
        
        public Cell(CellIndex index) { this.index = index; }
    }
}