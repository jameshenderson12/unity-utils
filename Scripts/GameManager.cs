using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//The Game Manager keeps track of which scenes to load, handles loading scenes, fading between scenes and also video playing/pausing

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    private Dictionary<string, HotspotInformation> hotspots = new Dictionary<string, HotspotInformation>();

    Scene scene;
    VideoPlayer video;
    Animator anim;
    Image fadeImage;

    AsyncOperation operation;


    [Header("Scene Management")]
    public string[] scenesToLoad;
    public string activeScene;

    [Space]
    [Header("UI Settings")]

    public bool useFade;
    public GameObject MainCamera;
    public GameObject fadeOverlay;
    public GameObject ControlUI;

    //make sure that we only have a single instance of the game manager
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(ControlUI);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    //set the initial active scene
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        activeScene = scene.buildIndex + " - " + scene.name;

        //Set all pivots to CameraPosition
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Pivot");
        foreach (GameObject gameobject in gameobjects)
            gameobject.transform.localPosition =  MainCamera.transform.localPosition;

        HotspotInformation flur1 = new HotspotInformation("Hotspot_Scene_Flur1");
        flur1.addHotspotInformation("Hotspot_Scene_Flur2", new Vector3(0,-90,0));

        HotspotInformation flur2 = new HotspotInformation("Hotspot_Scene_Flur2");
        flur2.addHotspotInformation("Hotspot_Scene_Flur1", new Vector3(0,-5,0));
        flur2.addHotspotInformation("Hotspot_Scene_Buero", new Vector3(0,-84,0));

        HotspotInformation buero = new HotspotInformation("Hotspot_Scene_Buero");
        buero.addHotspotInformation("Hotspot_Scene_Flur2", new Vector3(0,164,8));
        buero.addHotspotInformation("Hotspot_Scene_Druckerraum", new Vector3(0,52,2));
        buero.addHotspotInformation("Hotspot_Scene_Kueche", new Vector3(0,34,7));

        HotspotInformation druckerraum = new HotspotInformation("Hotspot_Scene_Druckerraum");
        druckerraum.addHotspotInformation("Hotspot_Scene_Buero", new Vector3(0,-140,0));

        HotspotInformation kueche = new HotspotInformation("Hotspot_Scene_Kueche");
        kueche.addHotspotInformation("Hotspot_Scene_Buero", new Vector3(0,-78,-22));
        kueche.addHotspotInformation("Hotspot_Scene_Flur3", new Vector3(0,100,-20));

        HotspotInformation flur3 = new HotspotInformation("Hotspot_Scene_Flur3");
        flur3.addHotspotInformation("Hotspot_Scene_Flur1", new Vector3(0,4,0));
        flur3.addHotspotInformation("Hotspot_Scene_Kueche", new Vector3(0,-165,3));
        flur3.addHotspotInformation("Hotspot_Scene_VRRaum", new Vector3(0,-90,-10));

        HotspotInformation vrraum = new HotspotInformation("Hotspot_Scene_VRRaum");
        vrraum.addHotspotInformation("Hotspot_Scene_Flur3", new Vector3(0,30,0));

        hotspots.Add("Hotspot_Scene_Flur1", flur1);
        hotspots.Add("Hotspot_Scene_Flur2", flur2);
        hotspots.Add("Hotspot_Scene_Buero", buero);
        hotspots.Add("Hotspot_Scene_Druckerraum", druckerraum);
        hotspots.Add("Hotspot_Scene_Kueche", kueche);
        hotspots.Add("Hotspot_Scene_Flur3", flur3);
        hotspots.Add("Hotspot_Scene_VRRaum", vrraum);
    }


    //Select scene is called from either the menu manager or hotspot manager, and is used to load the desired scene
    public void SelectScene(string sceneToLoad)
    {

        //if we want to use the fading between scenes, start the coroutine here
        if (useFade)
        {
            StartCoroutine(FadeOutAndIn(sceneToLoad));
        }
        //if we dont want to use fading, just load the next scene
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        //set the active scene to the next scene
        activeScene = sceneToLoad;
        Debug.Log(activeScene);
        if(hotspots.ContainsKey(activeScene))
        {
            for (int i = 0; i < ControlUI.transform.childCount; i++)
            {
                GameObject child = ControlUI.transform.GetChild(i).gameObject;
                if (hotspots[activeScene].hasHotspotInformation(child.transform.GetChild(0).name))
                {
                    child.SetActive(true);
                    child.transform.eulerAngles = hotspots[activeScene].getHotspotInformation(child.transform.GetChild(0).name);
                }
                else
                {
                    child.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < ControlUI.transform.childCount; i++)
            {
                ControlUI.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FadeOutAndIn(string sceneToLoad)
    {            //get references to animatior and image component
        anim = fadeOverlay.GetComponent<Animator>();
        fadeImage = fadeOverlay.GetComponent<Image>();

        //turn control UI off and loading UI on
        ControlUI.SetActive(false);

        //set FadeOut to true on the animator so our image will fade out
        anim.SetBool("FadeOut", true);

        //wait until the fade image is entirely black (alpha=1) then load next scene
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(sceneToLoad);
        Scene scene = SceneManager.GetSceneByName(sceneToLoad);
        Debug.Log("loading scene:" + scene.name);
        yield return new WaitUntil(() => scene.isLoaded);

        // grab video and wait until it is loaded and prepared before starting the fade out
        video = FindObjectOfType<VideoPlayer>();
        yield return new WaitUntil(() => video.isPrepared);

        //set FadeOUt to false on the animator so our image will fade back in
        anim.SetBool("FadeOut", false);

        //wait until the fade image is completely transparent (alpha = 0) and then turn loading UI off and control UI back on
        yield return new WaitUntil(() => fadeImage.color.a == 0);

        //if we have not destroyed the control UI, set it to active
        if (ControlUI)
        ControlUI.SetActive(true);



    }

    //Find the video in the scene and pause it
    public void PauseVideo()
    {
        if (!video)
        {
            video = FindObjectOfType<VideoPlayer>();
        }
        video.Pause();
    }

    //Find the video in the scene and play it
    public void PlayVideo()
    {
        if (!video)
        {
            video = FindObjectOfType<VideoPlayer>();
        }
        video.Play();
    }
}
