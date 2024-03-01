using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GraphAxis : MonoBehaviour
    {
        [SerializeField] float refreshRateInSeconds = 1f;
        
        readonly IList<Image> imageChildren = new List<Image>();
        
        void Awake()
        {
            for(var i = 0; i < transform.childCount; i++)
                imageChildren.Add(transform.GetChild(i).GetComponent<Image>());
        }

        void Update()
        {
            if(Time.time % refreshRateInSeconds < 0.1f)
                RefreshBars();
        }

        void RefreshBars()
        {
            var splits = FindAnyObjectByType<SnapshotsRack>()
                .SplitIn(howMany: transform.childCount)
                .Select(SnapshotButton.ColorOf)
                .ToList();

            imageChildren.Zip(splits, (image, color) => (image, color))
                .ToList()
                .ForEach(x => x.image.color = x.color);
        }
    }
}