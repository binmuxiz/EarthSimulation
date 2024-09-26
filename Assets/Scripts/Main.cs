using Home;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(HomeManager.Instance.Process());
    }
}
