using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public static MenuScript menuScript;
    public string buttonMessage;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Click()
    {
        SFXController.controller.PlaySFX("Bzing");
        menuScript.OnButtonClicked(buttonMessage);
    }
}
