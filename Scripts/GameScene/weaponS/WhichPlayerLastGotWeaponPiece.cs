using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WhichPlayerLastGotWeaponPiece : MonoBehaviour
{
    PhotonView pv;

    public List<int> playerViewIDs = new List<int>();

    string chosen;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void ToRPC(int id)
    {
        Debug.Log("here amde it");
        pv.RPC("RPC_addPlayerToWeaponPieceList", RpcTarget.All, id);
    }

    [PunRPC]
    void RPC_addPlayerToWeaponPieceList(int id)
    {
        Debug.Log("ID: " + id);
        playerViewIDs.Add(id);
        if(playerViewIDs.Count >= 3)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<PhotonView>().ViewID == playerViewIDs[playerViewIDs.Count-1])
                {
                    players[i].GetComponentInChildren<WeaponCompletedCode>().WeaponActive();
                    //FindGameObjectWithTag("WeaponComplete");
                }
            }
        }
    }
}
