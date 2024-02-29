using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    class Snapshots : IEnumerable<(DateTime when, string what)>
    {
        readonly List<(DateTime when, string what)> snapshots = new();
        public Snapshots() => Stamp("_begin_");
        
        public void Stamp(string whatStartsNow)
            => snapshots.Add((DateTime.Now, whatStartsNow));

        public double PercentOf(string what)
        {
            if (!this.Any())
                return 0;
            
            return TimeOf(what).TotalSeconds / TotalTime().TotalSeconds;
            
            TimeSpan TotalTime()
            {
                return UpdateToNow().Last().when - UpdateToNow().First().when;
            }
        }

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
            => this.Append((DateTime.Now, "_end_")).ToList();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<(DateTime when, string what)> GetEnumerator() => snapshots.Skip(1).GetEnumerator();
    }
}