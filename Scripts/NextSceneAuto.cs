using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class NextSceneAuto : MonoBehaviour
{
    //public VideoClip videoClip;
    public VideoPlayer video;

      void Start()
      {
        /*
          var videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
          var audioSource = gameObject.AddComponent<AudioSource>();

          videoPlayer.playOnAwake = false;
          videoPlayer.clip = videoClip;
          videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
          videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
          videoPlayer.targetMaterialProperty = "_MainTex";
          videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
          videoPlayer.SetTargetAudioSource(0, audioSource);
          */
      }

      void Update()
      {
          var video = GetComponent<UnityEngine.Video.VideoPlayer>();
          if (video.isPlaying)
          {

          }
          else
          {
            Debug.Log("The video has ended...");
            SceneManager.LoadScene("Scene01DoorChoice");
          }
      }
}
