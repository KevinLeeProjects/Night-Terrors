using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    //private PhotonView pv;
    public int whichTeam;
    private GameObject player;
    public static GameSetupController gs;

    //Microphone Settings
    [SerializeField]
    private GameObject noMicDetectedScreen;

    Camera cam;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        if(GameSetupController.gs == null)
        {
            GameSetupController.gs = this;
        }
        CreatePlayer();
        //pv = GetComponent<PhotonView>();
        //if (pv.IsMine)
        //{
        //    pv.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        //    //cam = player.GetComponent<Camera>();
        //    //cam = Camera.main;
        //}
        //else
        //{
        //    Debug.Log("NO");
        //    //cam.enabled = false;
        //}
        //CreatePlayer();
    }

   public void CreatePlayer()
    {
        whichTeam = QuickstartLobbyController.monsterOrPlayer;
        //whichTeam = QuickstartLobbyController.qlc.monsterOrPlayer;
        Debug.Log(whichTeam);
        if (whichTeam == 0)
        {
            player = PhotonNetwork.Instantiate(Path.Combine("MonsterPrefab", "PhotonMonster"), new Vector3(0, 5, 0), Quaternion.identity);
        }
        else if (whichTeam == 1)
        {
            player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(0, 2, 0), Quaternion.identity);
        }
        //Debug.Log("Creating Player");
        // PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    public void Update()
    {
        if (VoiceChatManager.isMicDetected)
        {
            noMicDetectedScreen.SetActive(false);
        }
        else
        {
            noMicDetectedScreen.SetActive(true);
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        whichTeam = QuickstartLobbyController.monsterOrPlayer;
        //whichTeam = QuickstartLobbyController.qlc.monsterOrPlayer;
        Debug.Log(whichTeam);
        if(whichTeam == 0)
        {
            player = PhotonNetwork.Instantiate(Path.Combine("MonsterPrefab", "PhotonMonster"), new Vector3(0,5,0), Quaternion.identity);
        }
        else if(whichTeam == 1)
        {
            player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(0, 2, 0), Quaternion.identity);
        }
    }
}
