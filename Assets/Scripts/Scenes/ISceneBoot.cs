using System.Collections;

namespace Selectica
{
    public interface ISceneBoot
    {
        public string SceneName { get; }
        public IEnumerator Boot();
    }
}