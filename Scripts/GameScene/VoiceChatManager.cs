using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;



public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    private List<string> listOfMics = new List<string>();
    public static bool isMicDetected;
    private bool restartMic = true;
    AudioSource mainMic;
    private float[] samplesBuffer;
    private string micName;
    [SerializeField]
    GameObject template;
    GameObject samplesRenderer;

    

    string appID = "ded13c9b6cf8436282c5c4e0c71a32a3";

    public static VoiceChatManager Instance;

    IRtcEngine rtcEngine;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        mainMic = GetComponent<AudioSource>();
        rtcEngine = IRtcEngine.GetEngine(appID);
        //rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += OnError;

        rtcEngine.EnableSoundPositionIndication(true);


        //rtcEngine.JoinChannel(QuickstartLobbyController.roomCodeStatic);
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
        //Debug.Log("jfoijaefadaw " + PhotonNetwork.CurrentRoom.Name);
    }


    void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined channel " + channelName);

        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
    }

    void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Left channel with duration " + stats.duration);
    }

    void OnError(int error, string msg)
    {
        Debug.LogError("Error with Agora " + error + " space " + msg);
    }

    public override void OnJoinedRoom()
    {
        //rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
        //Debug.Log("jfoijaefadaw");
    }

    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }

    private void OnDestroy()
    {
        IRtcEngine.Destroy();
    }

    void Update()
    {
        listOfMics.Clear();
        noMicDetectedCode();
    }

    public bool checkMic()
    {
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            listOfMics.Add(Microphone.devices[i]);
        }

        if (listOfMics.Count == 0)
        {
            isMicDetected = false;
            return false;
        }
        else
        {
            isMicDetected = true;
            return true;
        }
    }

    public void noMicDetectedCode()
    {
        if (checkMic())
        {
            if (restartMic)
            {
                int micLengthMinusOne = listOfMics[0].Length - 1;
                micName = listOfMics[0].Substring(0, micLengthMinusOne);
                micName = micName.Substring(12);
                Debug.Log(micName);

                //Unity mic system Uncomment if going to use again from here ->

                //mainMic.clip = Microphone.Start(listOfMics[0], true, 1, 44100);
                ////
                //mainMic.loop = true;
                //while (!(Microphone.GetPosition(null) > 0)) { }
                //mainMic.Play();
                //Debug.Log(Microphone.IsRecording(listOfMics[0]));
                //samplesBuffer = new float[mainMic.clip.channels];
                ////samplesRenderer = new GameObject[samplesBuffer.Length];
                ////for(int i = 0; i < samplesRenderer.Length; i++)
                ////{
                ////    samplesRenderer[i] = Instantiate(template, Vector3.right * i, Quaternion.identity);
                ////}
                //samplesRenderer = Instantiate(template, new Vector3(0,0,0), Quaternion.Euler(0,-90,90));

                // <- until here
                restartMic = false;
            }
            //Uncomment this also ->
            //int micPosition = Microphone.GetPosition(micName);
            //mainMic.clip.GetData(samplesBuffer, micPosition);
            //<-

            //for(int i = 0; i < samplesBuffer.Length; i++)
            //{
            //    sum += samplesBuffer[i];
            //}

            //Uncomment ->
            //for (int i = 0; i < samplesBuffer.Length; i++)
            //{
            //    float bufferSize = samplesBuffer[i] * soundScale;
            //    if (bufferSize < 0)
            //    {
            //        bufferSize *= -1;
            //    }
            //    samplesRenderer.transform.localScale = new Vector3(bufferSize, bufferSize, bufferSize);
            //}
            // <-
        }
        else
        {
            restartMic = true;
        }
    }
}
