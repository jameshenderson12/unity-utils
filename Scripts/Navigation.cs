using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Navigation : MonoBehaviour
{

    public void NextScene(string sceneName)
    {
      // Load the next scene
      SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
/*
    public void ReturnToHome()
    {
      ControlUI.SetActive(false);
      ControlUI.GetComponent<Renderer>().enabled = false;
      Destroy(ControlUI);
      // Stop any current video playing
      if (!video)
      {
          video = FindObjectOfType<VideoPlayer>();
      }
      video.Stop();
      // Load the next scene
      NextScene("MenuScene");
      // Disable the control UI

      Debug.Log("Succesfully returning to home");
    }
    */
    
}
