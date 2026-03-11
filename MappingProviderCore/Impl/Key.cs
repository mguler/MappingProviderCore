using System;

namespace MappingProviderCore.Impl
{
    public readonly record struct Key(Type Source, Type Target);
}
