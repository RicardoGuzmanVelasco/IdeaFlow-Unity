using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TotalTime : MonoBehaviour
    {
        IEnumerator Start()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                UpdateTimer();
                yield return new WaitForSeconds(1);
            }
        }

        void UpdateTimer()
        {
            var time = FindAnyObjectByType<SnapshotsRack>().TimeSinceFirstSnapshot();
            GetComponentInChildren<TMPro.TMP_Text>().text = $@"Total time {time:hh\:mm\:ss}";
        }
    }
}