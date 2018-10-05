using System.Linq;
using System.Reflection;

namespace Volo.Abp.DynamicProxy
{
    public static class ProxyHelper
    {
        /// <summary>
        /// 如果这是一个代理对象，则返回动态代理目标对象，否则返回给定对象。
        /// 它支持 Castle Dynamic Proxies.
        /// </summary>
        public static object UnProxy(object obj)
        {
            if (obj.GetType().Namespace != "Castle.Proxies")
            {
                return obj;
            }

            var targetField = obj.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.Name == "__target");

            if (targetField == null)
            {
                return obj;
            }

            return targetField.GetValue(obj);
        }
    }
}
