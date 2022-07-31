using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MenuScript
{
    public GameObject _controlsEditDisplayText;
    // Start is called before the first frame update
    public override void Start()
    {
        Cursor.visible = true;
        SceneMaster.sceneMaster = null;
        base.Start();
        KeyEditScript.editDisplayText = _controlsEditDisplayText;
        _controlsEditDisplayText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnButtonClicked(string _arg)
    {
        switch (_arg)
        {
            // Generic
            case "return":
                DisplayView("MainView");
                break;
            case "back":
                DisplayView(lastView);
                break;
            // MainView
            case "new":
                if (PlayerPrefs.GetInt("Progress") > 0) DisplayView("SureView");
                else SceneManager.LoadScene("Level1");
                break;
            case "continue":
                if (PlayerPrefs.GetInt("Progress") >= 5) SceneManager.LoadScene("Level5");
                else SceneManager.LoadScene($"Level{PlayerPrefs.GetInt("Progress") + 1}");
                break;
            case "choose":
                DisplayView("ChooseLevelView");
                break;
            case "options":
                DisplayView("OptionsView");
                break;
            case "exit":
                DisplayView("SureView");
                break;
            // OptionsView
            case "audio":
                DisplayView("AudioView");
                break;
            case "controls":
                DisplayView("ControlsView");
                break;
            case "saves":
                DisplayView("SavesView");
                break;
            case "fullscreen":
                ToggleFullscreen();
                break;
            // SavesView
            case "clearSave":
                DisplayView("SureView");
                break;
            case "clearSettings":
                DisplayView("SureView");
                break;
            case "clearAll":
                DisplayView("SureView");
                break;
            case "clearControls":
                DisplayView("SureView");
                break;
            // ControlsView
            // ~ghost town
            // SureView
            case "yes":
                switch (lastArg)
                {
                    case "clearSave":
                        PlayerPrefs.SetInt("Progress", 0);
                        break;
                    case "clearSettings":
                        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
                        PlayerPrefs.SetFloat("Music", 1.0f);
                        PlayerPrefs.SetFloat("SFX", 1.0f);
                        break;
                    case "clearAll":
                        PlayerPrefs.SetInt("Progress", 0);
                        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
                        PlayerPrefs.SetFloat("Music", 1.0f);
                        PlayerPrefs.SetFloat("SFX", 1.0f);
                        PlayerPrefs.SetFloat("MouseSensitivityX", 0.5f);
                        PlayerPrefs.SetFloat("MouseSensitivityY", 0.5f);
                        PlayerPrefs.SetInt("AbilityKey0", (int)KeyCode.E);
                        PlayerPrefs.SetInt("AbilityKey1", (int)KeyCode.Q);
                        PlayerPrefs.SetInt("AbilityKey2", (int)KeyCode.C);
                        PlayerPrefs.SetInt("GoForwardKey", (int)KeyCode.W);
                        PlayerPrefs.SetInt("GoBackwardKey", (int)KeyCode.S);
                        PlayerPrefs.SetInt("GoLeftKey", (int)KeyCode.A);
                        PlayerPrefs.SetInt("GoRightKey", (int)KeyCode.D);
                        PlayerPrefs.SetInt("JumpKey", (int)KeyCode.Space);
                        PlayerPrefs.SetInt("SprintKey", (int)KeyCode.LeftShift);
                        PlayerPrefs.SetInt("UseKey", (int)KeyCode.F);
                        break;
                    case "clearControls":
                        PlayerPrefs.SetFloat("MouseSensitivityX", 0.5f);
                        PlayerPrefs.SetFloat("MouseSensitivityY", 0.5f);
                        PlayerPrefs.SetInt("AbilityKey0", (int)KeyCode.E);
                        PlayerPrefs.SetInt("AbilityKey1", (int)KeyCode.Q);
                        PlayerPrefs.SetInt("AbilityKey2", (int)KeyCode.C);
                        PlayerPrefs.SetInt("GoForwardKey", (int)KeyCode.W);
                        PlayerPrefs.SetInt("GoBackwardKey", (int)KeyCode.S);
                        PlayerPrefs.SetInt("GoLeftKey", (int)KeyCode.A);
                        PlayerPrefs.SetInt("GoRightKey", (int)KeyCode.D);
                        PlayerPrefs.SetInt("JumpKey", (int)KeyCode.Space);
                        PlayerPrefs.SetInt("SprintKey", (int)KeyCode.LeftShift);
                        PlayerPrefs.SetInt("UseKey", (int)KeyCode.F);
                        break;
                    case "new":
                        PlayerPrefs.SetInt("Progress", 0);
                        PlayerPrefs.Save();
                        SceneManager.LoadScene("Level1");
                        break;
                    case "exit":
                        Application.Quit(0);
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
