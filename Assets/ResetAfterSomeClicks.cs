using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ResetAfterSomeClicks : MonoBehaviour
    {
        int remainingTimes = 10;

        void Awake() => GetComponent<Button>().onClick.AddListener(ResetIfNoMoreClicks);
        void OnDestroy() => GetComponent<Button>().onClick.RemoveListener(ResetIfNoMoreClicks);

        void ResetIfNoMoreClicks()
        {
            if(--remainingTimes <= 0)
                Reset();
        }
        
        void Reset() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        void Update()
        {
            GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Counter").text =
                $"Press {remainingTimes} times to reset";
        }
    }
}