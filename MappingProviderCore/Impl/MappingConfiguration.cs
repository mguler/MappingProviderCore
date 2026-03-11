using MappingProviderCore.Abstract;

namespace MappingProviderCore.Impl
{
    public abstract class MappingConfiguration : IMappingConfiguration
    {
 
        protected MappingConfiguration() { }


        public void Configure(IMappingServiceProvider mappingServiceProvider)
        {
            mappingServiceProvider.Register<string, string>((ctx, a, b) => {

                return b;
            });
        }
    }
}
