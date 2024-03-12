using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WtfCounter : MonoBehaviour
    {
        void Awake() => GetComponent<Button>().onClick.AddListener(CountOneDafuck);
        void OnDestroy() => GetComponent<Button>().onClick.AddListener(CountOneDafuck);

        void CountOneDafuck()
        {
            FindAnyObjectByType<SnapshotsRack>().Dafuck();
            GetComponentsInChildren<TMP_Text>().Single(x => x.name == "Counter").text =
                FindAnyObjectByType<SnapshotsRack>().Dafucks.ToString();
        }
    }
}