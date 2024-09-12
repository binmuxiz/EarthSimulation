using System;
using Fusion;
using UnityEngine;

public class TempManager : MonoBehaviour
{
    public void ReadyButton()
    {
        GameObject[] networkObjects = GameObject.FindGameObjectsWithTag("NetworkObject");

        foreach (var i in networkObjects)
        {
            if (i.GetComponent<NetworkObject>().HasStateAuthority)
                i.GetComponent<SharedData>().CheckReadyRpc();
        }
    }
    
}
