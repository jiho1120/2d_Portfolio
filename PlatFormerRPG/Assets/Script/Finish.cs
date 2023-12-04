using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public class Finish : MonoBehaviour
{
    public void OnFinish()
    {
        Application.Quit();
    }
}
