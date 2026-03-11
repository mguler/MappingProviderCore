using MappingProviderCore.Abstract;
using System;
using System.Collections.Concurrent;

namespace MappingProviderCore.Impl
{
    /// <summary>
    /// Mapping service implementation
    /// </summary>
    public class MappingService : IMappingServiceProvider
    {
        /// <summary>
        /// Contains registered mappings for specific types
        /// </summary>
        private readonly ConcurrentDictionary<Key,Delegate> _mappers = new();

        private readonly MappingContext _mappingContext;
        public MappingService(Func<Type,object> callback )
        {
            _mappingContext = new MappingContext( callback );
        }

        /// <summary>
        /// This method creates an instance of the type given in the TTarget parameter and maps source object's properties to the target instance
        /// </summary>
        /// <typeparam name="TTarget">the target type</typeparam>
        /// <param name="source">object instance to be mapped to target type</param>
        /// <returns>an instance of target type</returns>
        public TTarget Map<TSource, TTarget>(TSource source) where TTarget : new() 
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var key = new Key(sourceType, targetType);

            if (!_mappers.TryGetValue(key, out var mapper))
            {
                throw new InvalidOperationException($"Mapping configuration for {sourceType} to {targetType} does not exists");
            }

            var target = new TTarget();
            var result = ((Func<IMappingContext, TSource, TTarget, TTarget>)mapper)(_mappingContext, source, target);

            return result;
        }

        public void Register<T>() where T : IMappingConfiguration, new() 
        {
            var mapper = new T();
            mapper.Configure(this);
        }

        public void Register<TSource, TTarget>(Func<IMappingContext, TSource, TTarget, TTarget> mapper)
        {
            Register(mapper, typeof(TSource), typeof(TTarget));
        }

        internal void Register(Delegate func,Type sourceType,Type targetType)
        {
            var key = new Key(sourceType, targetType);

            if (!_mappers.TryAdd(key,func))
            {
                throw new InvalidOperationException($"Mapping configuration for {sourceType.FullName} to {targetType.FullName} has already been defined");
            }
        }
    }
}

