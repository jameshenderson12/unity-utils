﻿using System.Collections;
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

}
