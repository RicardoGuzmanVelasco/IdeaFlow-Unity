using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using static System.Linq.Enumerable;

namespace DefaultNamespace
{
    class Snapshots
    {
        [JsonIgnore] readonly List<(DateTime when, string what)> stamps = new();
        [field: JsonProperty] public int Wtf { get; private set; }

        [JsonIgnore] IEnumerable<(DateTime, string)> UpToNow => stamps.Append((DateTime.Now, "_end_"));
        [JsonIgnore] public IEnumerable<(DateTime when, string what)> Stamps => stamps.Skip(1);
        [JsonProperty] public IEnumerable<(TimeSpan howLong, string what)> Durations
        {
            get
            {
                var all = NowButTrunkingBegin();
                for (var i = 0; i < all.Count - 1; i++)
                    yield return (all[i + 1].when - all[i].when, all[i].what);
            }
        }

        public void Dafuck() => Wtf++;
        
        public Snapshots() => Stamp("_begin_");
        
        public void Stamp(string whatStartsNow)
        {
            if(IsTheSameThanIsActive(whatStartsNow))
                return;
            stamps.Add((DateTime.Now, whatStartsNow));
            
            bool IsTheSameThanIsActive(string whatStartsNow)
                => whatStartsNow != "_begin_" &&
                   Durations.Any() &&
                   Durations.Last().what == whatStartsNow;
        }

        public void RemoveLast()
        {
            if (stamps.Count > 1)
                stamps.Remove(stamps.Last());
        }

        public double PercentOf(string what)
            => Stamps.Any() ?
                TimeOf(what).TotalSeconds / TotalTime().TotalSeconds
                : 0;
        
        public TimeSpan TotalTime() => NowButTrunkingBegin().Last().when - NowButTrunkingBegin().First().when;

        public TimeSpan TimeOf(string what)
        {
            List<(DateTime when, string what)> all = NowButTrunkingBegin();

            var time = TimeSpan.Zero;
            for(var i = 0; i < all.Count - 1; i++)
            {
                if (all[i].what != what) continue;
                
                var begin = all[i].when;
                var end = all[i + 1].when;
                time += end - begin;
            }

            return time;
        }
        
        public IEnumerable<string> SplitIn(int howMany)
        {
            if(!Stamps.Any())
                return Repeat("_end_", howMany);
            
            
            var splits = Durations
                .Select(x => (percent: x.howLong / TotalTime(), x.what))
                .Select(x => (times: x.percent * howMany, x.what))
                .SelectMany(x => Repeat(x.what, Mathf.RoundToInt((float)x.times)))
                .ToList();

            return splits.Concat(Repeat(splits.Last(), howMany - splits.Count));
        }
        
        List<(DateTime when, string what)> NowButTrunkingBegin()
            => UpToNow.Skip(1).ToList();
    }
}