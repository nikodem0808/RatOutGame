using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStartScript : MonoBehaviour
{
    static bool gameStarted = false;
    void Awake()
    {
        SceneMaster.sceneMaster = null;
        if (gameStarted) {
            FindObjectOfType<AudioSource>().clip = SceneMaster.musicData[SceneMaster.trackID];
            FindObjectOfType<AudioSource>().loop = true;
            FindObjectOfType<AudioSource>().Play();
            FindObjectOfType<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume") * PlayerPrefs.GetFloat("Music");
            return;
        }
        Debug.Log("starting");
        // init setting if not defined
        // floats
        if (!PlayerPrefs.HasKey("MasterVolume")) PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        if (!PlayerPrefs.HasKey("Music")) PlayerPrefs.SetFloat("Music", 1.0f);
        if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1.0f);
        if (!PlayerPrefs.HasKey("MouseSensitivityX")) PlayerPrefs.SetFloat("MouseSensitivityX", 0.5f);
        if (!PlayerPrefs.HasKey("MouseSensitivityY")) PlayerPrefs.SetFloat("MouseSensitivityY", 0.5f);
        // ints
        if (!PlayerPrefs.HasKey("Progress")) PlayerPrefs.SetInt("Progress", 0);
        if (!PlayerPrefs.HasKey("AbilityKey0")) PlayerPrefs.SetInt("AbilityKey0", (int)KeyCode.E);
        if (!PlayerPrefs.HasKey("AbilityKey1")) PlayerPrefs.SetInt("AbilityKey1", (int)KeyCode.Q);
        if (!PlayerPrefs.HasKey("AbilityKey2")) PlayerPrefs.SetInt("AbilityKey2", (int)KeyCode.C);
        if (!PlayerPrefs.HasKey("GoForwardKey")) PlayerPrefs.SetInt("GoForwardKey", (int)KeyCode.W);
        if (!PlayerPrefs.HasKey("GoBackwardKey")) PlayerPrefs.SetInt("GoBackwardKey", (int)KeyCode.S);
        if (!PlayerPrefs.HasKey("GoLeftKey")) PlayerPrefs.SetInt("GoLeftKey", (int)KeyCode.A);
        if (!PlayerPrefs.HasKey("GoRightKey")) PlayerPrefs.SetInt("GoRightKey", (int)KeyCode.D);
        if (!PlayerPrefs.HasKey("JumpKey")) PlayerPrefs.SetInt("JumpKey", (int)KeyCode.Space);
        if (!PlayerPrefs.HasKey("SprintKey")) PlayerPrefs.SetInt("SprintKey", (int)KeyCode.LeftShift);
        if (!PlayerPrefs.HasKey("UseKey")) PlayerPrefs.SetInt("UseKey", (int)KeyCode.F);
        // strings
        ;
        // finish init
        // [DEBUG]
        ;
        // [DEBUG]
        PlayerPrefs.Save();
        //
        SceneMaster.musicData = Resources.LoadAll<AudioClip>("Audio/Music");
        AudioClip[] __tclips = Resources.LoadAll<AudioClip>("Audio/SFX");
        SceneMaster.sfxData = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in __tclips) {
            SceneMaster.sfxData.Add(clip.name, clip);
        }
        FindObjectOfType<AudioSource>().clip = SceneMaster.musicData[SceneMaster.trackID];
        FindObjectOfType<AudioSource>().loop = true;
        FindObjectOfType<AudioSource>().Play();
        FindObjectOfType<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume") * PlayerPrefs.GetFloat("Music");
        //
        gameStarted = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
