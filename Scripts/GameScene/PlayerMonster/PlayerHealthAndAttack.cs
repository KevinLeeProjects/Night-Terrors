using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerHealthAndAttack : MonoBehaviour
{
    //Photon variables
    private PhotonView pv;

    //HP variables
    public float playerHP;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            playerHealthCode();
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    void playerHealthCode()
    {
        playerHP = 3;
    }
}
