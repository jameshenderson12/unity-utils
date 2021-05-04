using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private Camera[] cameras = new Camera[3];
    int activeCameraIndex = 0;

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++) {
            cameras[i].enabled = false;
        }
        cameras[0].enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SwitchCamera(1);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SwitchCamera(-1);
    }

    private void SwitchCamera(int updown)
    {
        cameras[activeCameraIndex].enabled = false;
        activeCameraIndex= activeCameraIndex + updown;
        if (activeCameraIndex >= cameras.Length)
        {
            activeCameraIndex = 0;
        } else if (activeCameraIndex < 0)
        {
            activeCameraIndex = cameras.Length-1;
        }
        cameras[activeCameraIndex].enabled = true;
    }
}
