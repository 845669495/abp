using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// ∂‘œÛ∑√Œ ∆˜
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T Value { get; set; }

        public ObjectAccessor()
        {
            
        }

        public ObjectAccessor([CanBeNull] T obj)
        {
            Value = obj;
        }
    }
}