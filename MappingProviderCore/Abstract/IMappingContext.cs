namespace MappingProviderCore.Abstract
{
    public interface IMappingContext
    {
        T ResolveService<T>();
    }
}
