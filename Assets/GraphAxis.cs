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
        readonly IList<Image> imageChildren = new List<Image>();
        
        void Awake()
        {
            for(var i = 0; i < transform.childCount; i++)
                imageChildren.Add(transform.GetChild(i).GetComponent<Image>());
        }

        void Update()
        {
            var splits = FindAnyObjectByType<SnapshotsRack>()
                .SplitIn(howMany: transform.childCount)
                .Select(SnapshotButton.ColorOf)
                .ToList();
            
            Assert.AreEqual(splits.Count, imageChildren.Count);
            
            imageChildren.Zip(splits, (image, color) => (image, color))
                .ToList()
                .ForEach(x => x.image.color = x.color);
        }
    }
}