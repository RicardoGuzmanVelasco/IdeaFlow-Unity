using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class SnapshotsRack : MonoBehaviour
    {
        readonly Snapshots snapshots = new();

        TMP_Text TheRack => GetComponentInChildren<TMP_Text>();
        
        public TimeSpan TimeOf(string what) => snapshots.TimeOf(what);
        public double PercentOf(string what) => snapshots.PercentOf(what);
        public IEnumerable<(TimeSpan howLong, string what)> Durations => snapshots.Durations;

        public void Snapshot(string whatStartsNow)
        {
            snapshots.Stamp(whatStartsNow);
            TheRack.text = snapshots.Stamps
                .Select(ColorizeStamp)
                .Aggregate((x, y) => $"{x}\n{y}");
        }

        static string ColorizeStamp((DateTime when, string what) x)
        {
            var color = SnapshotButton.ColorOf(x.what);
            var stamp = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{x.when:HH:mm:ss} {x.what}</color>";
            return stamp;
        }
    }
}