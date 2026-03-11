using MappingProviderCore.Abstract;
using MappingProviderCore.Impl;
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
        /// this is the abstract definition of map method with a generic type parameter
        /// </summary>
        /// <typeparam name="TTarget">target type</typeparam>
        /// <param name="source">source instance</param>
        /// <returns>an instance of target type</returns>
        TTarget Map<TSource, TTarget>(TSource source) where TTarget : new();
        void Register<T>() where T : IMappingConfiguration, new();
        void Register<TSource, TTarget>(Func<IMappingContext,TSource, TTarget, TTarget> mapper);

    }
}

