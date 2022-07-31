using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableTeleport : UsableScript
{
    public GameObject teleportLocation;
    Vector3 tpLoc;
    // Start is called before the first frame update
    void Start()
    {
        tpLoc = teleportLocation.transform.position;
        Destroy(teleportLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        SceneMaster.sceneMaster.pMov.transform.position = tpLoc;
        SceneMaster.sceneMaster.pMov.rb.velocity = Vector3.zero;
        SFXController.controller.PlaySFX("Beamup");
    }
}
