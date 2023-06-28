using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerPickUpWeapons : MonoBehaviour
{
    PhotonView pv;
    public static List<float> listOfWeaponsPickedUp = new List<float>();

    GameObject ws;

    GameObject lastWeaponTracker;

    //weaponSpawn ws;

    public Text asd;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        //ws = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<weaponSpawn>();
    }

    void Start()
    {
        
        if (!pv.IsMine)
            return;

    }

    private void OnCollisionEnter(Collision collision)
    {
        string colName = collision.gameObject.name;
        //int whichWeapon;
        if (colName.Length >= 6 && colName.Substring(0, 6) == "Weapon")
        {
            //whichWeapon = int.Parse(colName.Substring(colName.IndexOf("n") + 1, 1));
            //if(!listOfWeaponsPickedUp.Contains(whichWeapon))
            //{
            //    listOfWeaponsPickedUp.Add(whichWeapon);
            //    //Debug.Log(whichWeapon);
            //}
            ////pv.RPC("RPC_weaponPickUp", RpcTarget.All, whichWeapon);
            //GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
            PhotonView colPV = collision.gameObject.transform.parent.gameObject.GetComponent<PhotonView>();
            int colViewID = colPV.ViewID;
            ws = GameObject.FindGameObjectWithTag("WeaponSpawner");
            ws.GetComponent<weaponSpawn>().WeaponPickedUpCounter();
            Debug.Log("Made it1");
            lastWeaponTracker = GameObject.FindGameObjectWithTag("LastWeaponTracker");
            Debug.Log("Made it");
            Debug.Log(transform.gameObject.GetComponent<PhotonView>().ViewID);
            lastWeaponTracker.GetComponent<WhichPlayerLastGotWeaponPiece>().ToRPC(transform.gameObject.GetComponent<PhotonView>().ViewID);
            //pv.RPC("RPC_weaponPickUpNumberPlusPlus", RpcTarget.AllBuffered, 1);
            //pv.RPC("RPC_weaponPickUpNumberPlusPlus", RpcTarget.All, 1);
            pv.RPC("RPC_weaponPickUp", RpcTarget.MasterClient, colViewID);
            

            //if (colPV.IsMine)
            //    pv.RPC("RPC_weaponPickUp", RpcTarget.MasterClient, colViewID);
            ////if(colPV.IsMine)
            ////    PhotonNetwork.Destroy(collision.gameObject.transform.parent.gameObject);
            //else
            //{
            //    Debug.Log("Not mine");
            //    //pv.RPC("RPC_weaponPickUp", RpcTarget.All, colViewID);
            //    Debug.Log(allPlayers.Length);
            //    for(int i = 0; i < allPlayers.Length; i++)
            //    {
            //        pv.RPC("RPC_weaponPickUp", RpcTarget.MasterClient, colViewID);
            //        float allPlayersWeaponCode = allPlayers[i].GetComponent<PlayerMovement>().pickUpWeaponCode;
            //        if(allPlayersWeaponCode == 1)
            //        {
            //            allPlayers[i].GetComponent<PlayerPickUpWeapons>().pv.RPC("RPC_weaponPickUp", RpcTarget.All, colViewID);
            //        }
            //    }
            //}
            

            //give all players a variable, get all gameobjects with tag 'player', set the variable to something, so we know who the master client is, then have master client destroy

            //collision.gameObject.transform.parent.gameObject.SetActive(false);
            //ws.destroyWeapon(collision.gameObject.transform.parent.gameObject);
            //PhotonNetwork.Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    [PunRPC]
    

    void RPC_weaponPickUp(int viewID)
    {
        //if (!pv.IsMine)
        //    return;
        Debug.Log("JIJI");
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
        //WeaponPickUpNumberPlusPlus(1);
        //weaponPickUp(ind);
    }

    

    //void weaponPickUp(int ind)
    //{
    //    Debug.Log("IJIJ");
    //    DestroyWeapon(ind);
    //}

    //public void DestroyWeapon(int ind)
    //{
    //    if (!pv.IsMine)
    //    {
    //        Debug.Log("Not mine");
    //        return;
    //    }
    //    Debug.Log("OWOW");
    //    GameObject[] objectToDestroy = GameObject.FindGameObjectsWithTag("Weapon");
    //    Debug.Log(objectToDestroy.Length);

    //    for (int i = 0; i < objectToDestroy.Length; i++)
    //    {
    //        Debug.Log(objectToDestroy[i].name == "Weapon" + ind.ToString() + "(Clone)");
    //        //objectToDestroy[i].name == "Weapon" + ind.ToString() + "(Clone)";
    //        if (objectToDestroy[i].name == "Weapon" + ind.ToString() + "(Clone)")
    //        {
    //            PhotonNetwork.Destroy(objectToDestroy[i]);
    //        }
    //    }
    //    //for(int i = 0; i < listOfWeapons.Count; i++)
    //    //{
    //    //    Debug.Log("NO NULL: " + listOfWeapons[i]);
    //    //    Debug.Log("NO NULLY: " + listOfWeapons[ind]);
    //    //    if (listOfWeapons[i] == listOfWeapons[ind])
    //    //    {
    //    //        Debug.Log("RIGHT HERE");
    //    //        PhotonNetwork.Destroy(listOfWeapons[ind]);
    //    //    }
    //    //}
    //    Debug.Log("OWOWU");
    //    //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Weapon"))
    //    //{
    //    //    //Debug.Log(obj.name);
    //    //    if (weapon.name == obj.name)
    //    //    {
    //    //PhotonNetwork.Destroy(listOfWeapons[ind]);
    //    //    }
    //    //}
    //    //Debug.Log("nw: " + listOfWeapons.Count);
    //    //for(int i = 0; i < listOfWeapons.Count; i++)
    //    //{
    //    //    Debug.Log("here: " + listOfWeapons[i].name);
    //    //    if(listOfWeapons[i].name + "(Clone)" == weapon.name)
    //    //    {
    //    //        Debug.Log("IT WORKED");
    //    //    }
    //    //}
    //    //PhotonNetwork.Destroy(weapon);
    //}
}
