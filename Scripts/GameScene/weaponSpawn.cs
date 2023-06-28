using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;

public class weaponSpawn : MonoBehaviour
{
    public PhotonView pv;
    public static weaponSpawn ws;

    [SerializeField]
    public static List<GameObject> listOfWeapons = new List<GameObject>();
    [SerializeField]
    private GameObject weaponOne;
    [SerializeField]
    private GameObject weaponTwo;
    [SerializeField]
    private GameObject weaponThree;
    private List<int> keepTrackOfRoom = new List<int>();

    //Text asd;
    public int syncVariable = 0;

    private float yAxis;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;

        if (weaponSpawn.ws == null)
        {
            weaponSpawn.ws = this;
        }



        yAxis = 3;
        listOfWeapons.Add(weaponOne);
        listOfWeapons.Add(weaponTwo);
        listOfWeapons.Add(weaponThree);
        for(int i = 0; i < 3; i++)
        {
            int ran = Random.Range(0, 6);
            while(keepTrackOfRoom.Contains(ran))
            {
                ran = Random.Range(0, 6);
            }
            keepTrackOfRoom.Add(ran);
            SpawnWeapon(ran, i);
        }
    }

    public void SpawnWeapon(int roomNumber, int index)
    {
        listOfWeapons[index].name = "Weapon" + index.ToString();
        //178, 2, 88
        //178, 2, -35
        //56, 2, -35
        //56, 2, 88

        //50, 2, 88
        //50, 2, -88
        //-16, 2, -88
        //-16, 2, 88
        if (roomNumber == 0)
        {
            int ran = Random.Range(0, 2);
            if(ran == 0)
            {
                //player = PhotonNetwork.Instantiate(Path.Combine("MonsterPrefab", "PhotonMonster"), new Vector3(0, 1, 0), Quaternion.identity);
                listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(56, 178), yAxis, Random.Range(-35, 88)), Quaternion.identity);
            }
            else
            {
                //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(-16, 50), yAxis, Random.Range(-88, 88)), Quaternion.identity);
                listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(-16, 50), yAxis, Random.Range(-88, 88)), Quaternion.identity);
            }
            
        }
        else if(roomNumber == 1)
        {
            //180, 2, -48
            //180, 2, -89
            //75, 2, -89
            //75, 2, -48
            //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(75, 180), yAxis, Random.Range(-89, -48)), Quaternion.identity);
            listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(75, 180), yAxis, Random.Range(-89, -48)), Quaternion.identity);
        }
        else if(roomNumber == 2)
        {
            //-42, 2, 85
            //-42, 2, 35
            //-174, 2, 35
            //-174, 2, 85
            //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(-178, - 39), yAxis, Random.Range(35, 85)), Quaternion.identity);
            listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(-174, - 42), yAxis, Random.Range(30, 88)), Quaternion.identity);

        }
        else if(roomNumber == 3)
        {
            //-39, 2, 15
            //-39, 2, -34
            //-179, 2, -34
            //-179, 2, 15
            //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(-179, -39), yAxis, Random.Range(15, 34)), Quaternion.identity);
            listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(-179, -39), yAxis, Random.Range(-30, 15)), Quaternion.identity);

        }
        else if(roomNumber == 4)
        {
            //-39, 2, -48
            //-105, 2, -88
            //-39, 2, -88
            //-105, 2, -48
            //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(-105, -39), yAxis, Random.Range(-88, -48)), Quaternion.identity);
            listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(-105, -39), yAxis, Random.Range(-88, -48)), Quaternion.identity);

        }
        else
        {
            //-130, 2, -88
            //-178. 2. -48
            //-130, 2, -48
            //-178, 2, -88
            //listOfWeapons[index] = Instantiate(listOfWeapons[index], new Vector3(Random.Range(-178, -130), yAxis, Random.Range(-88, -48)), Quaternion.identity);
            listOfWeapons[index] = PhotonNetwork.Instantiate(Path.Combine("WeaponPrefab", listOfWeapons[index].name), new Vector3(Random.Range(-178, -130), yAxis, Random.Range(-88, -48)), Quaternion.identity);
        }
        listOfWeapons[index].GetComponentInChildren<Animation>().Play();
    }
    public void WeaponPickedUpCounter()
    {
        pv.RPC("RPC_weaponPickUpNumberPlusPlus", RpcTarget.All, 1);
    }

    [PunRPC]
    void RPC_weaponPickUpNumberPlusPlus(int counter)
    {
        syncVariable += counter;
        Debug.Log("Counter " + syncVariable);
        if (syncVariable >= 3)
        {
            Debug.Log("Done");
           // asd.text = "done";
        }
        //PlayerMovement.pickUpWeaponCode += counter;
        //asd.text = PlayerMovement.pickUpWeaponCode.ToString();
        //Debug.Log("Number of weapons: " + PlayerMovement.pickUpWeaponCode);
        //PlayerMovement pm = new PlayerMovement();
        //pm.pickUpWeaponCode++;
    }


}
   