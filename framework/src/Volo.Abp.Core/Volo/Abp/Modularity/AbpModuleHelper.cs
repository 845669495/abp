using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Volo.Abp.Modularity
{
    internal static class AbpModuleHelper
    {
        /// <summary>
        /// 查找所有模块类型
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            var moduleTypes = new List<Type>();
            AddModuleAndDependenciesResursively(moduleTypes, startupModuleType);
            return moduleTypes;
        }

        /// <summary>
        /// 查找依赖模块类型
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            AbpModule.CheckAbpModuleType(moduleType);

            var dependencies = new List<Type>();

            var dependencyDescriptors = moduleType
                .GetCustomAttributes()
                .OfType<IDependedTypesProvider>();

            foreach (var descriptor in dependencyDescriptors)
            {
                foreach (var dependedModuleType in descriptor.GetDependedTypes())
                {
                    dependencies.AddIfNotContains(dependedModuleType);
                }
            }

            return dependencies;
        }
        /// <summary>
        /// 添加模块和递归依赖
        /// </summary>
        /// <param name="moduleTypes"></param>
        /// <param name="moduleType"></param>
        private static void AddModuleAndDependenciesResursively(List<Type> moduleTypes, Type moduleType)
        {
            AbpModule.CheckAbpModuleType(moduleType);

            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);

            foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
            {
                AddModuleAndDependenciesResursively(moduleTypes, dependedModuleType);
            }
        }
    }
}
