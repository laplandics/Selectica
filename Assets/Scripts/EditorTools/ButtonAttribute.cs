using System;

namespace Selectica.EditorTools
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ButtonAttribute : Attribute { }
}