using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public static Dictionary<string, AudioClip> sfxData;
    public static AudioClip[] musicData;
    public static SceneMaster sceneMaster;
    //
    public bool gamePausedImperative;
    public playerControl pControl;
    public playerMov pMov;
    public GUIScript guiControl;
    public Vector3 audioVolume;
    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;
    public static int trackID = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneMaster = this;
        gamePausedImperative = false;
        Cursor.visible = false;
        pControl = FindObjectOfType<playerControl>(true);
        pMov = FindObjectOfType<playerMov>(true);
        guiControl = FindObjectOfType<GUIScript>(true);
        audioVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("Music"), PlayerPrefs.GetFloat("SFX"));
        SceneManager.sceneLoaded += Unpause;
        foreach (var i in SceneMaster.sfxData.Keys) sfxData[i].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (pMov.camPivot.transform.position.y < -100.0f)
        {
            OnGameOver();
        }
        musicPlayer.volume = audioVolume.x * audioVolume.y;
        if (Input.GetKeyDown(KeyCode.X)) guiControl.ShowInfo("TEST");
        if (!musicPlayer.isPlaying)
        {
            musicPlayer.clip = musicData[trackID];
            if (++trackID >= musicData.Length) trackID = 0;
            musicPlayer.Play();
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        gamePausedImperative = true;
        Time.timeScale = 0.0f;
    }

    public void Unpause()
    {
        Cursor.visible = false;
        gamePausedImperative = false;
        Time.timeScale = 1.0f;
        pControl.GetControls();
        audioVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("Music"), PlayerPrefs.GetFloat("SFX"));
    }

    public void Unpause(Scene _A, LoadSceneMode _B)
    {
        Cursor.visible = false;
        gamePausedImperative = false;
        Time.timeScale = 1.0f;
        pControl.GetControls();
        audioVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("Music"), PlayerPrefs.GetFloat("SFX"));
    }

    public void OnGameOver()
    {
        ButtonScript.menuScript.DisplayView("GameOverView");
        Pause();
    }
}
