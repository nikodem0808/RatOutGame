using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUsable : UsableScript
{
    public string info;
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
        SceneMaster.sceneMaster.guiControl.ShowInfo(info);
    }
}
