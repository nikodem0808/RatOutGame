using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public float dashForce;
    // Start is called before the first frame update
    public override void Start()
    {
        dashForce = 10.0f;
        //
        abilityName = "Dash";
        abilityDisplayedName = "Dash";
        cooldown = 4.0f;
        localTimer = 0.0f;
        //
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //
        base.Update();
    }

    public override void Trigger(bool keyUp)
    {
        if (localTimer > 0.0f) return;
        if (keyUp) return;
        //
        if ((SceneMaster.sceneMaster.pMov.rb.velocity + SceneMaster.sceneMaster.pMov.cam.transform.forward.normalized * dashForce).magnitude < (SceneMaster.sceneMaster.pMov.cam.transform.forward.normalized * dashForce).magnitude) SceneMaster.sceneMaster.pMov.rb.velocity = Vector3.zero;
        SceneMaster.sceneMaster.pMov.rb.velocity += SceneMaster.sceneMaster.pMov.cam.transform.forward.normalized * dashForce * (1.0f - SceneMaster.sceneMaster.pMov.rb.velocity.magnitude / SceneMaster.sceneMaster.pMov.maxVelocity);
        SFXController.controller.PlaySFX("Splat");
        //
        localTimer = cooldown;
    }
}
