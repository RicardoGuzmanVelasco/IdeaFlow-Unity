using UnityEngine;

namespace DefaultNamespace
{
    public class KeepTheScreenAwaken : MonoBehaviour
    {
        void Awake() => Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}