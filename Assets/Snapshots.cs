using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
    class Snapshots
    {
        readonly List<(DateTime when, string what)> stamps = new();

        public IEnumerable<(DateTime when, string what)> Stamps => stamps.Skip(1);
        public IEnumerable<(TimeSpan howLong, string what)> Durations
        {
            get
            {
                var all = UpdateToNow();
                for (var i = 0; i < all.Count - 1; i++)
                    yield return (all[i + 1].when - all[i].when, all[i].what);
            }
        }
        
        public Snapshots() => Stamp("_begin_");
        
        public void Stamp(string whatStartsNow)
            => stamps.Add((DateTime.Now, whatStartsNow));

        public double PercentOf(string what)
            => Stamps.Any() ?
                TimeOf(what).TotalSeconds / TotalTime().TotalSeconds
                : 0;
        
        public TimeSpan TotalTime() => UpdateToNow().Last().when - UpdateToNow().First().when;


        public TimeSpan TimeOf(string what)
        {
            List<(DateTime when, string what)> all = UpdateToNow();

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
        
        List<(DateTime when, string what)> UpdateToNow()
            => Stamps.Append((DateTime.Now, "_end_")).ToList();
    }
}