using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GraphAxis : MonoBehaviour
    {
        void Update()
        {
            foreach(Transform child in transform)
                Destroy(child.gameObject);
            
            var durations = FindObjectOfType<SnapshotsRack>().Durations;
            foreach (var (howLong, what) in durations)
            {
                var percent = Percent(howLong.TotalSeconds);
                var color = SnapshotButton.ColorOf(what);
                DrawBar(percent, color);
            }
        }

        void DrawBar(double percent, Color color)
        {
            var bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bar.transform.localScale = new Vector3(0.1f, (float)percent, 0.1f);
            bar.transform.position = new Vector3((float)percent, 0, 0);
            bar.GetComponent<Renderer>().material.color = color;
        }

        double Percent(double duration) => duration / FindObjectOfType<SnapshotsRack>().TotalTime().TotalSeconds;
    }
}