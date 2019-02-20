using CachingFramework.Redis.Contracts;
using CachingFramework.Redis.Contracts.Providers;
using CachingFramework.Redis.Contracts.RedisObjects;
using CachingFramework.Redis.RedisObjects;

namespace CachingFramework.Redis.Providers
{
    /// <summary>
    /// Collection provider using Redis
    /// </summary>
    internal class RedisCollectionProvider : RedisProviderBase, ICollectionProvider
    {
        #region Fields
        private readonly ICacheProvider _cacheProvider;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCollectionProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cacheProvider">The cache provider instance.</param>
        public RedisCollectionProvider(RedisProviderContext context, ICacheProvider cacheProvider)
            : base(context)
        {
            _cacheProvider = cacheProvider;
        }
        #endregion

        #region IRedisObjectsProvider Implementation
        /// <summary>
        /// Returns an IList implemented using a Redis List
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="key">The redis key</param>
        public IRedisList<T> GetRedisList<T>(string key)
        {
            return new RedisList<T>(_context, key);
        }
        /// <summary>
        /// Returns an IDictionary implemented using a Redis Hash
        /// </summary>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <typeparam name="TValue">The object type</typeparam>
        /// <param name="key">The redis key</param>
        /// <param name="scanPageSize">The page size for Scan operations.</param>
        public IRedisDictionary<TKey, TValue> GetRedisDictionary<TKey, TValue>(string key, int scanPageSize = 10)
        {
            return new RedisDictionary<TKey, TValue>(_context, key, _cacheProvider, scanPageSize);
        }
        /// <summary>
        /// Returns an ISet implemented using a Redis Set
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="key">The redis key</param>
        /// <param name="scanPageSize">The page size for Scan operations.</param>
        public IRedisSet<T> GetRedisSet<T>(string key, int scanPageSize = 10)
        {
            return new RedisSet<T>(_context, key, _cacheProvider, scanPageSize);
        }
        /// <summary>
        /// Returns an ICollection implemented using a Redis Sorted Set
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="key">The redis key</param>
        /// <param name="scanPageSize">The page size for Scan operations.</param>
        public IRedisSortedSet<T> GetRedisSortedSet<T>(string key, int scanPageSize = 10)
        {
            return new RedisSortedSet<T>(_context, key, _cacheProvider, scanPageSize);
        }
        /// <summary>
        /// Returns an ICollection implemented using a Redis string as a bitmap
        /// </summary>
        /// <param name="key">The redis key</param>
        public IRedisBitmap GetRedisBitmap(string key)
        {
            return new RedisBitmap(_context, key);
        }
        /// <summary>
        /// Returns an ICollection(string) implemented using a Redis sorted set with lexicographical order
        /// </summary>
        /// <param name="key">The redis key</param>
        /// <param name="scanPageSize">The page size for Scan operations.</param>
        public IRedisLexicographicSet GetRedisLexicographicSet(string key, int scanPageSize = 10)
        {
            return new RedisLexicographicSet(_context, key, scanPageSize);
        }
        /// <summary>
        /// Returns an ICollection(char) implemented using a Redis string
        /// </summary>
        /// <param name="key">The redis key</param>
        public IRedisString GetRedisString(string key)
        {
            return new RedisString(_context, key);
        }
        #endregion
    }
}