using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevelUsable : UsableScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        PlayerPrefs.SetInt("Progress", Mathf.Max(SceneManager.GetActiveScene().buildIndex, PlayerPrefs.GetInt("Progress")));
        PlayerPrefs.Save();
        SceneMaster.sceneMaster = null;
        SFXController.controller = null;
        int _t = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadScene(_t);
        if (_t == 5) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(_t + 1);
    }
}
