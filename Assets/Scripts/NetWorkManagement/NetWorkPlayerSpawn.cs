using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkPlayerSpawn : MonoBehaviourPunCallbacks
{
    private GameObject spawnPlayerPrefabs;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnPlayerPrefabs = PhotonNetwork.Instantiate("NetWork Player", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnPlayerPrefabs);
    }
}
