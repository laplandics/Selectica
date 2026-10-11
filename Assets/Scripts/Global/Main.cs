using UnityEngine;

namespace Selectica
{
    public static class Main
    {
        public static bool IsPlaying { get; set; }

        public static bool IsCursorVisible
        {
            get => Cursor.visible;
            set => Cursor.visible = value;
        }
    }
}