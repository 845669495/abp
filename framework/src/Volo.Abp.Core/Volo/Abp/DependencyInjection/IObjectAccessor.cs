using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// ¶ÔÏó·ÃÎÊÆ÷
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectAccessor<out T>
    {
        [CanBeNull]
        T Value { get; }
    }
}