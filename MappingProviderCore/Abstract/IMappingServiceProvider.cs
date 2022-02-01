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
        /// <summary>
        /// this definition supplies abstraction for mapper registration 
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="func">mapper function</param>
        void Register<TSource, TTarget>(Func<TSource, TTarget, TTarget> func);

    }
}
