using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SnapshotButton : MonoBehaviour
    {
        bool running;
        
        Button TheButton => GetComponent<Button>();
        TMP_Text ThePercent => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Percent");
        TMP_Text TheTimer => GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Timer");
        
        static SnapshotsRack TheSnapshots => FindObjectOfType<SnapshotsRack>();

        void Awake()
            => TheButton.onClick.AddListener(() => TheSnapshots.Snapshot(gameObject.name));

        void Update()
        {
            TheTimer.text = TheSnapshots.TimeOf(gameObject.name).ToString(@"hh\:mm\:ss");
            ThePercent.text = $"{TheSnapshots.PercentOf(gameObject.name):P0}";
        }

        public static Color ColorOf(string what)
            => FindObjectsOfType<SnapshotButton>()
                .SingleOrDefault(x => x.gameObject.name == what)
                ?.GetComponent<Image>().color ?? Color.clear;
    }
}