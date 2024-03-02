using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GraphToClipboard : MonoBehaviour
    {
        void Awake() => GetComponent<Button>().onClick.AddListener(Copy);

        static void Copy()
        {
            var serialized = FindAnyObjectByType<SnapshotsRack>().Serialize();
            GUIUtility.systemCopyBuffer = serialized;
        }
    }
}