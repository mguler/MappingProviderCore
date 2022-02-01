using MappingProviderCore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingProviderCore.Impl
{
    /// <summary>
    /// Mapping service implementation
    /// </summary>
    public class MappingService : IMappingServiceProvider
    {
        /// <summary>
        /// Default mapper 
        /// </summary>
        public Func<object, object> DefaultMapper { get => throw new NotImplementedException("Not yet implemented"); }

        /// <summary>
        /// Contains registered mappings for specific types
        /// </summary>
        private readonly IList<Delegate> _mappingsCache = new List<Delegate>();

        private Func<Type, object> _dependencyResolverCallback;

        /// <summary>
        /// constructor for injecting predefined mapping configurations.  
        /// </summary>
        /// <param name="mappingConfigurations">An array of mapping configuration</param>
        public MappingService(IMappingConfiguration[] mappingConfigurations)
        {
            foreach (var mappingConfiguration in mappingConfigurations)
            {
                mappingConfiguration.Configure(this);
            }
        }        
        /// <summary>
        /// This method creates an instance of the type given in the TTarget parameter and maps source object's properties to the target instance
        /// </summary>
        /// <typeparam name="TTarget">the target type</typeparam>
        /// <param name="source">object instance to be mapped to target type</param>
        /// <returns>an instance of target type</returns>
        public TTarget Map<TTarget>(object source)
        {
            var targetType = typeof(TTarget);
            var result = Map(source, targetType);
            return (TTarget)result;
        }
        /// <summary>
        /// This method creates an instance of the type given in the targetType parameter and maps source object's properties to the target instance
        /// </summary>
        /// <param name="source">source object</param>
        /// <param name="targetType">type of target instance</param>
        /// <returns>instance of the target type</returns>
        public object Map(object source, Type targetType)
        {
            var target = targetType.GetConstructor(Type.EmptyTypes).Invoke(null);
            var result = Map(source, target);
            return result;
        }

        public TTarget Map<TTarget>(object source, object target)
        {

            var result = Map(source, target);
            return (TTarget)result;
        }
        public object Map(object source, object target)
        {
            if (source == null)
            {
                throw new ArgumentException($"Source object cannot be null");
            }

            var sourceType = source.GetType();
            var targetType = target.GetType();

            var mapper = _mappingsCache.FirstOrDefault(mapping => mapping.Method.ReturnType == targetType &&
                                                                 mapping.Method.GetParameters().FirstOrDefault()
                                                                     ?.ParameterType == sourceType);
            if (mapper == null)
            {
                throw new Exception($"Mapping configuration for {sourceType} to {targetType} does not exists");
            }

            List<object> parameters = new List<object>();
            parameters.Add(source);
            parameters.Add(target);

            mapper.Method.GetParameters().Where(parameterInfo => parameterInfo.Position > 1).ToList().ForEach(parameterInfo =>
            {
                if (_dependencyResolverCallback == null) 
                {
                    throw new Exception("mapper need to dependencies to be resolved but no dependency resolver has set");
                }
                var parameter = _dependencyResolverCallback(parameterInfo.ParameterType);
                parameters.Add(parameter);
            });

            var result = mapper.DynamicInvoke((object?[]?)parameters.ToArray());
            return result;
        }

        internal void RegisterMapper(Delegate func)
        {
            //if (func.Method.GetParameters().Length < 2)
            //{
            //    throw new ArgumentException("mapper function must have minimum 2 input parameters");
            //}

            var alreadyExists = _mappingsCache.Any(mapping => mapping.Method.ReturnType == func.Method.ReturnType &&
                                                                 mapping.Method.GetParameters().FirstOrDefault()
                                                                     ?.ParameterType == func.Method.GetParameters().FirstOrDefault()
                                                                     ?.ParameterType);
            if (alreadyExists)
            {
                throw new Exception($"Mapping configuration for {func.Method.GetParameters().FirstOrDefault().ParameterType.FullName} to {func.Method.ReturnType.Name} has already been defined before");
            }
            _mappingsCache.Add(func);
        }

        /// <summary>
        /// Registers mapping configurations for specific types
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="func">the method which supplies mapping service between the types given in the type parameters</param>
        public void Register<TSource, TTarget>(Func<TSource, TTarget, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1>(Func<TSource, TTarget, TDependency1, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2>(Func<TSource, TTarget, TDependency1, TDependency2, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TTarget> func) => RegisterMapper(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TTarget> func) => RegisterMapper(func);
        public void SetDependencyResolver(Func<Type, object> func) => _dependencyResolverCallback = func;
    }
}

