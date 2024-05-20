using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasDisplay : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++){
            Display.displays[i].Activate();
        }       
    }
}
