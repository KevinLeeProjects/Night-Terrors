using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class QuickstartLobbyController : MonoBehaviourPunCallbacks
{
    //public static QuickstartLobbyController qlc;
    [SerializeField]
    private GameObject quickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private GameObject noMicDetectedErrorScreen;
    [SerializeField]
    private GameObject enableMicScreen;
    [SerializeField]
    private int RoomSize;
    [SerializeField]
    private GameObject monsterOptionButton;
    [SerializeField]
    private GameObject playerOptionButton;
    [SerializeField]
    private GameObject loadingButton;
    [SerializeField]
    private GameObject joinRoomButton;
    [SerializeField]
    private GameObject createRoomButton;
    [SerializeField]
    private GameObject roomCodeInputField;

    private DatabaseReference reference;

    int randomRoomNumber;
    public static string roomCodeStatic;

    public static int monsterOrPlayer; // 0 for monster, 1 for player
    public Transform[] spawnPointMonster;
    public Transform[] spawnPointPlayer;

    private int joinOrCreate; //0 for create, 1 for join

    private List<string> listOfMics = new List<string>();

    public void Start()
    {
        quickStartButton.SetActive(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public override void OnConnectedToMaster()
    {
        //if (QuickstartLobbyController.qlc == null)
        //{
        //    QuickstartLobbyController.qlc = this;
        //}
        //Debug.Log(PhotonNetwork.IsMasterClient);
        //debugText.text = PhotonNetwork.IsMasterClient.ToString();

        //checkMicButton();
    }

    public void enableMicButtonYes()
    {
        enableMicScreen.SetActive(false);
        checkMicButton();
    }

    public void enableMicButtonNo()
    {
        Application.Quit();
    }
 
    public bool checkMic()
    {
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            listOfMics.Add(Microphone.devices[i]);
            Debug.Log(Microphone.devices[i]);
        }

        if (listOfMics.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void checkMicButton()
    {
        if (checkMic())
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            quickStartButton.SetActive(true);
            noMicDetectedErrorScreen.SetActive(false);
        }
        else
        {
            noMicDetectedErrorScreen.SetActive(true);
            //Application.Quit();
        }
    }

    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(false);
        //PhotonNetwork.JoinRandomRoom();
        //debugText.text += "Quick Start";
        Debug.Log("Quick start");
        CreateRoom();
    }

    private void writeNewUser(string roomNumber, string numberOfMonsters, string numberOfPlayers)
    {
        RoomManager roomManager = new RoomManager(numberOfMonsters, numberOfPlayers);
        string json = JsonUtility.ToJson(roomManager);

        reference.Child("rooms").Child(roomNumber).SetRawJsonValueAsync(json);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        //debugText.text +=  "Failed to join a room";
        CreateRoom();
    }

    void CreateRoom()
    {
        

        Debug.Log("Creating room now");
        //debugText.text += "Creating room now";
        
        RoomOptions roomOps = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)RoomSize
        };
        Debug.Log(randomRoomNumber);
        roomCodeStatic = randomRoomNumber.ToString();
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        CreateRoomFirebase(randomRoomNumber.ToString());
    }

    private void CreateRoomFirebase(string roomNumber)
    {
        RoomManager roomManager = new RoomManager(roomNumber);
        string json = JsonUtility.ToJson(roomManager);

        reference.Child("rooms").Child(roomNumber).SetRawJsonValueAsync(json);
        Debug.Log("ERJUIOE");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room... trying again");
        CreateRoom();
    }

    public void QuickCancel()
    {
        //quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public void ChooseOption()
    {
        randomRoomNumber = Random.Range(0, 10000000);
        roomCodeStatic = randomRoomNumber.ToString();
        //TODO (Only for beta+ versions. In prototype, we aare using random room numbers. In beta+, the room number will be the player's ID, which has not been set in prototype.
        //Create an array with all room numbers from firebase database
        //Loop through each element in array 
        //If randomRoomNumber == an element, roll a new random number and restart loop
        
        
        
        Debug.Log("Choose");
        createRoomButton.SetActive(true);
        joinRoomButton.SetActive(true);
        playerOptionButton.SetActive(false);
        monsterOptionButton.SetActive(false);
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(false);
        loadingButton.SetActive(false);
    }

    public void MonsterOption()
    {
        monsterOrPlayer = 0;
        SetNumberOfMonster();
        if (joinOrCreate == 0)
        {
            QuickStart();
        }
        else
        {
            roomCodeStatic = roomCodeInputField.GetComponent<InputField>().text;
            Debug.Log(roomCodeInputField.GetComponent<InputField>().text);
            PhotonNetwork.JoinRoom("Room" + roomCodeInputField.GetComponent<InputField>().text);
        }
    }

    private void SetNumberOfMonster()
    {
        Debug.Log("HERE");
        //TODO 1
        //Get numberOfMonster from child(roomCodeStatic).
        //If numberOfMonster > 0 then give off error "There is already a monster in this lobby"
        //else, make the numberOfMonster = 1

       FirebaseDatabase.DefaultInstance.GetReference("rooms").GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
               Debug.Log("ERROR");
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              if(snapshot.Value == null)
               {

               }
              else
               {

               }
          }
      });

        //if()
        //{

        //}
        //else
        //{
        //    writeNewUser(randomRoomNumber.ToString(), );
        //}
    }

    public void PlayerOption()
    {
        monsterOrPlayer = 1;
        if (joinOrCreate == 0)
        {
            QuickStart();
        }
        else
        {
            roomCodeStatic = roomCodeInputField.GetComponent<InputField>().text;
            Debug.Log(roomCodeInputField.GetComponent<InputField>().text);

            PhotonNetwork.JoinRoom("Room" + roomCodeInputField.GetComponent<InputField>().text);
        }
    }

    public void JoinRoomCode()
    {
        createRoomButton.SetActive(false);
        joinRoomButton.SetActive(false);
        playerOptionButton.SetActive(false);
        monsterOptionButton.SetActive(false);
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(false);
        loadingButton.SetActive(false);
        roomCodeInputField.SetActive(true);
        joinOrCreate = 1;
    }

    public void CreateRoomCode()
    {
        createRoomButton.SetActive(false);
        joinRoomButton.SetActive(false);
        playerOptionButton.SetActive(true);
        monsterOptionButton.SetActive(true);
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(false);
        loadingButton.SetActive(false);
        joinOrCreate = 0;
    }

    void Update()
    {
        if(roomCodeInputField.GetComponent<InputField>().text.Length > 0 && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            createRoomButton.SetActive(false);
            joinRoomButton.SetActive(false);
            playerOptionButton.SetActive(true);
            monsterOptionButton.SetActive(true);
            quickStartButton.SetActive(false);
            quickCancelButton.SetActive(false);
            loadingButton.SetActive(false);
            roomCodeInputField.SetActive(false);
        }
    }

    public class RoomManager
    {
        public string numberOfMonsters;
        public string numberOfPlayers;
        public string roomNumber;

        public RoomManager()
        {
        }

        public RoomManager(string numberOfMonsters, string numberOfPlayers)
        {
            this.numberOfMonsters = numberOfMonsters;
            this.numberOfPlayers = numberOfPlayers;
        }

        public RoomManager(string roomNumber)
        {
            this.roomNumber = roomNumber;
        }

        public RoomManager(string roomNumber, string numberOfMonsters, string numberOfPlayers)
        {
            this.numberOfMonsters = numberOfMonsters;
            this.numberOfPlayers = numberOfPlayers;
            this.roomNumber = roomNumber;
        }
    }
}
