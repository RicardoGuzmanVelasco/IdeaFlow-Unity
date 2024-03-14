using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
        public IEnumerable<string> SplitIn(int howMany) => snapshots.SplitIn(howMany);

        public void Dafuck() => snapshots.Dafuck();
        public int Dafucks => snapshots.Wtf;

        public TimeSpan TimeSinceLastSnapshot()
            => snapshots.Stamps.Any()
                ? DateTime.Now - snapshots.Stamps.Last().when
                : TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        
        public TimeSpan TimeSinceFirstSnapshot()
            => snapshots.Stamps.Any()
                ? DateTime.Now - snapshots.Stamps.First().when
                : TimeSpan.Zero;

        public void RemoveLast()
        {
            snapshots.RemoveLast();
            RefreshText();
        }

        public string Serialize() => JsonConvert.SerializeObject(snapshots);

        public void Snapshot(string whatStartsNow)
        {
            snapshots.Stamp(whatStartsNow);

            RefreshText();
        }

        void RefreshText()
        {
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