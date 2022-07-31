using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public Ability[] abilities;
    public static Dictionary<string, KeyCode> controlKeys;
    public bool controlsFrozen = false;
    RaycastHit raycastHit;
    public UsableScript currentUsable;
    // Start is called before the first frame update
    void Start()
    {
        abilities = FindObjectsOfType<Ability>();
        controlKeys = new Dictionary<string, KeyCode>();
        GetControls();
        controlsFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsFrozen || SceneMaster.sceneMaster.gamePausedImperative) return;
        // General
        if (Input.GetKeyDown(controlKeys["use"]) && currentUsable != null) currentUsable.Use();
        // Abilities
        for (int i = 0; i < abilities.Length  && i < 3; i++)
        {
            if (Input.GetKeyDown(controlKeys[$"a{i}"])) abilities[i].Trigger(false);
            if (Input.GetKeyUp(controlKeys[$"a{i}"])) abilities[i].Trigger(true);
        }
    }

    public void GetControls()
    {
        controlKeys["forward"] = (KeyCode)PlayerPrefs.GetInt("GoForwardKey");
        controlKeys["backward"] = (KeyCode)PlayerPrefs.GetInt("GoBackwardKey");
        controlKeys["left"] = (KeyCode)PlayerPrefs.GetInt("GoLeftKey");
        controlKeys["right"] = (KeyCode)PlayerPrefs.GetInt("GoRightKey");
        controlKeys["sprint"] = (KeyCode)PlayerPrefs.GetInt("SprintKey");
        controlKeys["jump"] = (KeyCode)PlayerPrefs.GetInt("JumpKey");
        controlKeys["use"] = (KeyCode)PlayerPrefs.GetInt("UseKey");
        controlKeys["a0"] = (KeyCode)PlayerPrefs.GetInt("AbilityKey0");
        controlKeys["a1"] = (KeyCode)PlayerPrefs.GetInt("AbilityKey1");
        controlKeys["a2"] = (KeyCode)PlayerPrefs.GetInt("AbilityKey2");
    }
}
