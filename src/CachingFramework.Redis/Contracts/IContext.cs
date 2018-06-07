﻿using System;
using CachingFramework.Redis.Contracts.Providers;

namespace CachingFramework.Redis.Contracts
{
    /// <summary>
    /// Interface for Context class containing the public APIs.
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// Gets the cache API.
        /// </summary>
        ICacheProvider Cache { get; }
        /// <summary>
        /// Gets the collection API.
        /// </summary>
        /// <value>The collections.</value>
        ICollectionProvider Collections { get; }
        /// <summary>
        /// Gets the geo spatial API.
        /// </summary>
        IGeoProvider GeoSpatial { get; }
        /// <summary>
        /// Gets the pub sub API.
        /// </summary>
        IPubSubProvider PubSub { get; }
        /// <summary>
        /// Gets the Key/Event space notifications API.
        /// </summary>
        IKeyEventsProvider KeyEvents { get; }

        /// <summary>
        /// Gets the serializer for this context.
        /// </summary>
        ISerializer GetSerializer();
    }
}