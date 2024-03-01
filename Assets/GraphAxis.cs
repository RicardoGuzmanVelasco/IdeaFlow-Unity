using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static DefaultNamespace.SnapshotButton;

namespace DefaultNamespace
{
    public class GraphAxis : MonoBehaviour
    {
        readonly IList<Image> imageChildren = new List<Image>();
        
        void Awake()
        {
            for(var i = 0; i < transform.childCount; i++)
                imageChildren.Add(transform.GetChild(i).GetComponent<Image>());
        }

        void Update()
        {
            if(Time.time % RefreshRateInSeconds() < 0.1f)
                RefreshBars();
        }

        public double RefreshRateInSeconds()
            => Time.realtimeSinceStartup switch
            {
                < 60f => .25f,
                < 60f * 5 => 1f,
                < 60f * 15 => 5f,
                _ => 15f
            };

        void RefreshBars()
        {
            var splits = FindAnyObjectByType<SnapshotsRack>()
                .SplitIn(howMany: transform.childCount)
                .Select(TheColor);

            imageChildren.Zip(splits, (image, color) => (image, color))
                .ToList()
                .ForEach(x => x.image.color = x.color);
        }
    }
}