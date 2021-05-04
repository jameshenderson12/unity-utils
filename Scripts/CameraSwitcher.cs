using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras = new Camera[4]; // Various cameras defined

    int activeCameraIdx = 0;

    // Use this for initialization
    void Start ()
    {
        // Initial setup - enable the first one, disable the others
        cameras[0].enabled = true;
        cameras[1].enabled = false;
        cameras[2].enabled = false;
        cameras[3].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      // This cycles the cameras forward on space. Replace with whatever button you want.
      if (Input.GetKeyDown(KeyCode.Space))
        SwitchCameras();
    }

    private void SwitchCameras()
    {
        // Disable the current camera
        cameras[activeCameraIdx].enabled = false;

        // Increment the index of the active camera
        // "%" is the modulo-operator. It returns division remainder, and causes activeCameraIdx to cycle between 0 and 3
        activeCameraIdx++;
        activeCameraIdx %= 5;

        // Enable the new one
        cameras[activeCameraIdx].enabled = true;
    }
}
