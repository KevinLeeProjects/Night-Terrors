using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCompletedCode : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    public void WeaponActive()
    {
        weapon.SetActive(true);
    }
}
