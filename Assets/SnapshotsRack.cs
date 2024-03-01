using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public TimeSpan TotalTime() => snapshots.TotalTime();
        public IEnumerable<string> SplitIn(int howMany) => snapshots.SplitIn(howMany);

        public void Snapshot(string whatStartsNow)
        {
            snapshots.Stamp(whatStartsNow);

            var built = new StringBuilder();
            for(var i = 0; i < snapshots.Stamps.ToList().Count; i++)
                built.AppendLine(NumberedStamp(i));
            
            TheRack.text = built.ToString();
        }

        string NumberedStamp(int i)
            => ColorizeStamp(i+1, snapshots.Stamps.ToList()[i].when, snapshots.Stamps.ToList()[i].what);

        static string ColorizeStamp(int number, DateTime when, string what)
        {
            var color = SnapshotButton.TheColor(what);
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{number}. {when:HH:mm:ss} {what} </color>";
        }
    }
}