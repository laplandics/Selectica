namespace Selectica
{
    public abstract class EntitySystem
    {
        public abstract void RegisterEntity(Entity entity, params object[] args);
        public abstract void UnregisterEntity(Entity entity);
    }
}