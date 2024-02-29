using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GraphAxis : MonoBehaviour
    {
        Transform Template => transform.GetChild(0);
        void Update()
        {
            Clean();
            
            var durations = FindObjectOfType<SnapshotsRack>().Durations;
            foreach (var (howLong, what) in durations)
            {
                var percent = Percent(howLong.TotalSeconds);
                var color = SnapshotButton.ColorOf(what);
                DrawBar(percent, color);
            }
        }

        void Clean()
        {
            foreach(Transform child in transform)
                if (child != Template)
                    Destroy(child.gameObject);
        }

        void DrawBar(double percent, Color color)
        {
            
        }

        double Percent(double duration) => duration / FindObjectOfType<SnapshotsRack>().TotalTime().TotalSeconds;
    }
}