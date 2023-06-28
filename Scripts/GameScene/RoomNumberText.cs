using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNumberText : MonoBehaviour
{
    public Text roomCodeText;

    void Start()
    {
        roomCodeText.text = QuickstartLobbyController.roomCodeStatic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
