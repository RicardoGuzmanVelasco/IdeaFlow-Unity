using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RemoveLastAfterSomeClicks : MonoBehaviour
    {
        const int Times = 5;
        int remainingTimes = Times;

        void Awake() => GetComponent<Button>().onClick.AddListener(RemoveLastIfNoMoreClicks);
        void OnDestroy() => GetComponent<Button>().onClick.RemoveListener(RemoveLastIfNoMoreClicks);

        void RemoveLastIfNoMoreClicks()
        {
            if (--remainingTimes > 0)
                return;
            remainingTimes = Times;
            
            RemoveLast();
        }

        void RemoveLast() => FindAnyObjectByType<SnapshotsRack>().RemoveLast();

        void Update()
        {
            GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Counter").text =
                $"Press {remainingTimes} times to remove last";
        }
    }
}