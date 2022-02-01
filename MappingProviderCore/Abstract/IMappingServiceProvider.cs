using System;
/// <summary>
/// this namespace contains abstract structures for mapping
/// </summary>
namespace MappingProviderCore.Abstract
{
    /// <summary>
    /// Supplies abstract structure for mapping service providers
    /// </summary>
    public interface IMappingServiceProvider
    {
        /// <summary>
        /// default mapper instance
        /// </summary>
        Func<object, object> DefaultMapper { get; }
        /// <summary>
        /// this is the abstract definition of map method with a generic type parameter
        /// </summary>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="source">source instance</param>
        /// <returns>an instance of target type</returns>
        TTarget Map<TTarget>(object source);
        /// <summary>
        /// this is the abstract definition of map method with a generic type parameter
        /// </summary>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="source">source instance</param>
        /// <param name="target">target instance</param>
        /// <returns>an instance of target type</returns>
        TTarget Map<TTarget>(object source, object target);
        /// <summary>
        /// this is the abstract definition of map method
        /// </summary>
        /// <param name="source">source instance</param>
        /// <param name="targetType">target type</param>
        /// <returns>an instance of target type</returns>
        object Map(object source, Type targetType);
        object Map(object source, object target);

        void SetDependencyResolver(Func<Type, object> func); 

        /// <summary>
        /// this definition supplies abstraction for mapper registration 
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="func">mapper function</param>
        void Register<TSource, TTarget>(Func<TSource, TTarget, TTarget> func);
        public void Register<TSource, TTarget, TDependency1>(Func<TSource, TTarget, TDependency1, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2>(Func<TSource, TTarget, TDependency1, TDependency2, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TTarget> func) => Register(func);
        public void Register<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7>(Func<TSource, TTarget, TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TTarget> func) => Register(func);
    }
}
