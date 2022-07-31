using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneMaster;

public class PauseMenuScript : MenuScript
{
    public bool paused = false;
    public GameObject _controlsEditDisplayText;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        paused = false;
        KeyEditScript.editDisplayText = _controlsEditDisplayText;
        _controlsEditDisplayText.SetActive(false);
        DisplayView("GUIView");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentView.name == "GameOverView" || currentView.name == "GameEndView") return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_controlsEditDisplayText.activeSelf) return;
            if (currentView.name == "SureView")
            {
                OnButtonClicked("no");
                return;
            }
            if (paused)
            {
                DisplayView("GUIView");
                sceneMaster.Unpause();
                paused = false;
                return;
            }
            DisplayView("PauseView");
            sceneMaster.Pause();
            paused = true;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (sceneMaster == null || currentView.name != "GUIView") return;
        if (focus && !paused)
        {
            sceneMaster.Unpause();
            return;
        }
        sceneMaster.Pause();
    }

    public override void OnButtonClicked(string _arg)
    {
        if (_arg == "end" && SceneManager.GetActiveScene().name == "Level5")
        {
            PlayerPrefs.SetInt("Progress", 5);
            PlayerPrefs.Save();
            DisplayView("GameEndView");
        }
        switch (_arg)
        {
            // Generic
            case "return":
                DisplayView("PauseView");
                break;
            case "reload":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "back":
                DisplayView(lastView);
                break;
            // PauseView
            case "resume":
                DisplayView("GUIView");
                sceneMaster.Unpause();
                paused = false;
                break;
            case "options":
                DisplayView("OptionsView");
                sceneMaster.audioVolume = new Vector3(PlayerPrefs.GetFloat("MasterVolume"), PlayerPrefs.GetFloat("Music"), PlayerPrefs.GetFloat("SFX"));
                break;
            case "main":
                DisplayView("SureView");
                break;
            // OptionsView
            case "audio":
                DisplayView("AudioView");
                break;
            case "controls":
                DisplayView("ControlsView");
                break;
            case "fullscreen":
                ToggleFullscreen();
                break;
            // ControlsView
            // ~ghost town
            // SureView
            case "yes":
                switch (lastArg)
                {
                    case "main":
                        SceneManager.LoadScene("MainMenu");
                        break;
                    default:
                        break;
                }
                PlayerPrefs.Save();
                DisplayView(lastView);
                break;
            case "no":
                DisplayView(lastView);
                break;
            default:
                return;
        }
        lastArg = _arg;
    }

    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1366, 720, false);
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
