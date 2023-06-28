using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSChecker : MonoBehaviour
{
    public string fpsString;
    public float deltaTime;
    public Text fpsCheckertext;

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsString = Mathf.Ceil(fps).ToString();
        fpsCheckertext.text = "FPS: " + fpsString;
    }
}
