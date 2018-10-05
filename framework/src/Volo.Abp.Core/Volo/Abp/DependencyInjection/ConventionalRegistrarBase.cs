using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Reflection;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 常规注册基类
    /// </summary>
    public abstract class ConventionalRegistrarBase : IConventionalRegistrar
    {
        public virtual void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = AssemblyHelper
                .GetAllTypes(assembly)
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType
                ).ToArray();  //获取所有非抽象、非泛型的类

            AddTypes(services, types);
        }

        public virtual void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(services, type);
            }
        }

        public abstract void AddType(IServiceCollection services, Type type);
    }
}