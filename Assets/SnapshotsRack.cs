using System;
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

        public void Snapshot(string whatStartsNow)
        {
            snapshots.Stamp(whatStartsNow);
            TheRack.text = snapshots.Stamps
                .Select(x => $"{x.when:HH:mm:ss} {x.what}")
                .Aggregate((x, y) => $"{x}\n{y}");
        }
    }
}