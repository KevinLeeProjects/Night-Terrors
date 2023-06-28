using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    //public Text text;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Application.targetFrameRate = 60;
    }

    public override void OnConnectedToMaster()
    {
        //text.text = "We are now connected to the " + PhotonNetwork.CloudRegion + " server!";
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
