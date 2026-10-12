using System;

namespace Selectica
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ButtonAttribute : Attribute { }
}