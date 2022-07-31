using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUsable : UsableScript
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
        SFXController.controller.PlaySFX("Boom");
        Destroy(gameObject);
    }
}
