using Volo.Abp.Collections;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// 模块生命周期选项
    /// </summary>
    public class ModuleLifecycleOptions
    {
        public ITypeList<IModuleLifecycleContributer> Contributers { get; }

        public ModuleLifecycleOptions()
        {
            Contributers = new TypeList<IModuleLifecycleContributer>();
        }
    }
}
