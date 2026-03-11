using MappingProviderCore.Abstract;
using System;

namespace MappingProviderCore.Impl
{
    internal class MappingContext : IMappingContext
    {
        private readonly Func<Type, object> _factory;
        public MappingContext(Func<Type,object> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            _factory = factory;
        }
        public T ResolveService<T>()
        {
            var service =_factory(typeof(T));

            if (service is null)
                throw new InvalidOperationException($"{typeof(T).FullName} cannot be resolved.");
            return (T)service;
        }
    }
}
