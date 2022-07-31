using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : UsableScript
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
        SceneMaster.sceneMaster.Pause();
        FindObjectOfType<PauseMenuScript>().OnButtonClicked("end");
    }
}
