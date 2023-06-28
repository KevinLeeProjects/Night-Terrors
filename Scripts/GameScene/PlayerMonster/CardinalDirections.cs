using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardinalDirections : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject front;
    public GameObject back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getRight()
    {
        return right.transform.position;
    }

    public Vector3 getLeft()
    {
        return left.transform.position;
    }

    public Vector3 getFront()
    {
        return front.transform.position;
    }

    public Vector3 getBack()
    {
        return back.transform.position;
    }
}
