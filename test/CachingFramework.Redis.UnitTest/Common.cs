﻿using System;
using System.Linq;
using System.Threading;
using CachingFramework.Redis.Serializers;

namespace CachingFramework.Redis.UnitTest
{
    public static class Common
    {
        // A context using a raw serializer
        private static Context _rawContext;
        // A context using a binary serializer
        private static Context _binaryContext;
        // A context using json
        private static Context _jsonContext;
        // A context using msgpack
        private static Context _msgPackContext;


        // TestCases
        public static Context[] JsonAndRaw { get { return new[] { _jsonContext, _rawContext }; } }
        public static Context[] Json { get { return new[] { _jsonContext }; } }
        public static Context[] MsgPack { get { return new[] { _msgPackContext }; } }
        public static Context[] Raw { get { return new[] { _rawContext }; } }
        public static Context[] Bin { get { return new[] { _binaryContext }; } }
        public static Context[] All { get; set; }
        public static Context[] BinAndRawAndJson { get; set; }

        public static DateTime ServerNow
        {
            get
            {
                var cnn = _rawContext.GetConnectionMultiplexer();
                var serverNow = cnn.GetServer(cnn.GetEndPoints()[0]).Time();
                return serverNow;
            }
        }

        public static int[] VersionInfo { get; set; }
        public static string Config = "localhost:6379, allowAdmin=true"; 

        static Common()
        {
            
            _rawContext = new Context(Config, new RawSerializer());
            _jsonContext = new Context(Config, new JsonSerializer());
            _msgPackContext = new Context(Config, new MsgPack.MsgPackSerializer());
#if (NET45 || NET461)
            _binaryContext = new Context(Config, new BinarySerializer());
            All = new[] { _binaryContext, _rawContext, _jsonContext, _msgPackContext };
            BinAndRawAndJson = new[] { _binaryContext, _rawContext, _jsonContext };
#else
            BinAndRawAndJson = new[] { _rawContext, _jsonContext };
            All = new[] { _rawContext, _jsonContext, _msgPackContext };
#endif

            Thread.Sleep(1500);
            _rawContext.Cache.FlushAll();
            // Get the redis version
            var server = _rawContext.GetConnectionMultiplexer().GetServer(_rawContext.GetConnectionMultiplexer().GetEndPoints()[0]);
            VersionInfo = server.Info("Server")[0].First(x => x.Key == "redis_version").Value.Split('.').Take(2).Select(x => int.Parse(x)).ToArray();
            // Enable keyspace notifications
            var eventsConfig = server.ConfigGet("notify-keyspace-events");
            if (!eventsConfig[0].Value.ToUpper().Contains('K') || !eventsConfig[0].Value.ToUpper().Contains('E'))
            {
                server.ConfigSet("notify-keyspace-events", "KEA");
            }
            
        }
    }
}
