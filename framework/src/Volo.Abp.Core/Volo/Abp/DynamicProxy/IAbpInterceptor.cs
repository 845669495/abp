using System.Threading.Tasks;

namespace Volo.Abp.DynamicProxy
{
    /// <summary>
    /// 拦截器
    /// </summary>
	public interface IAbpInterceptor
    {
        void Intercept(IAbpMethodInvocation invocation);

        Task InterceptAsync(IAbpMethodInvocation invocation);
	}
}
