using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class SnapshotsRack : MonoBehaviour
    {
        readonly List<(DateTime when, string what)> snapshots = new();
        
        TMP_Text TheRack => GetComponentInChildren<TMP_Text>();
        
        public TimeSpan TimeOf(string what)
        {
            var time = TimeSpan.FromSeconds(0);
            for (var i = 0; i < snapshots.Count; i++)
            {
                var (theWhen, theWhat) = snapshots[i];
                if (theWhat != what)
                    continue;
                
                var timeOfTheLastSnapshot = i == 0 ? TimeSpan.FromSeconds(0) : snapshots[i - 1].when - theWhen;
                time += timeOfTheLastSnapshot;
            }

            return time;
        }
        
        public double PercentOf(string what)
        {
            if (snapshots.Count == 0)
                return 0;
            
            var time = TimeOf(what);
            var totalTime = snapshots[^1].when - snapshots[0].when;
            return (time.TotalSeconds / totalTime.TotalSeconds);
        }

        public void Snapshot(string what)
        {
            snapshots.Add((DateTime.Now, what));
            
            TheRack.text = string.Join("\n", snapshots);
        }
    }
}