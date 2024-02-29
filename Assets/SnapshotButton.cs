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
        
        static SnapshotButton AllOfUs => FindObjectOfType<SnapshotButton>();
        static SnapshotsRack TheSnapshots => FindObjectOfType<SnapshotsRack>();

        void Awake()
        {
            TheButton.onClick.AddListener(() =>
            {
                AllOfUs.Stop();
                this.Play();
                TheSnapshots.Snapshot(gameObject.name);
            });
        }

        void Play() => running = true;
        void Stop()
        {
            running = false;
            Refresh();
        }

        void Refresh()
        {
            TheTimer.text = TheSnapshots.TimeOf(gameObject.name).ToString(@"hh\:mm\:ss");
            ThePercent.text = $"{TheSnapshots.PercentOf(gameObject.name):P}";
        }

        void Update()
        {
            if(running)
                Refresh();
        }
    }
}