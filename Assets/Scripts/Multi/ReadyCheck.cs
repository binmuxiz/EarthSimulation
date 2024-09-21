using System;
using Fusion;
using UnityEngine;

public class ReadyCheck : MonoBehaviour
{
    //LoadingScene Ready Button에서 사용됨
    public void ReadyButton()
    {
        GameObject[] networkObjects = GameObject.FindGameObjectsWithTag("NetworkObject");

        foreach (var obj in networkObjects)
        {
            if (obj.GetComponent<NetworkObject>().HasStateAuthority)
                obj.GetComponent<SharedData>().CheckReadyRpc();
        }
    }
}
