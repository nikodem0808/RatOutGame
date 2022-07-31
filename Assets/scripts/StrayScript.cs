using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneMaster;

public class StrayScript : MonoBehaviour
{
    StrayShadowAbility strayShadowAbility;
    Rigidbody shell;
    float speed;
    Vector3 direction;
    //
    Vector3 pull;
    float force = 2.7f, launchSpeedMultiplier = 2.3f, radius;
    //
    bool phaseDown, phaseUp, plf = false;
    Vector3 t;
    //GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        speed = 30.0f;
        strayShadowAbility = FindObjectOfType<StrayShadowAbility>();
        //direction = sceneMaster.pMov.cam.transform.position - sceneMaster.pMov.transform.position;
        //direction = new Vector3(Mathf.Sin(sceneMaster.pMov.rot.y * Mathf.PI / 180.0f), -Mathf.Tan(sceneMaster.pMov.rot.x * Mathf.PI / 180.0f), Mathf.Cos(sceneMaster.pMov.rot.y * Mathf.PI / 180.0f));
        //direction = direction.normalized;
        direction = sceneMaster.pMov.cam.transform.forward;
        shell = GetComponent<Rigidbody>();
        SFXController.controller.PlaySFX("Zap");
        //
        //sphere = Resources.Load<GameObject>("Prefabs/Sphere");
        //sphere = Instantiate(sphere, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (sceneMaster.gamePausedImperative) return;
        pull = new Vector3(radius * Mathf.Cos(sceneMaster.pMov.rot.x / 180.0f * Mathf.PI) * Mathf.Sin(((sceneMaster.pMov.rot.y + 180.0f) % 360.0f) / 180.0f * Mathf.PI), radius * Mathf.Sin(sceneMaster.pMov.rot.x / 180.0f * Mathf.PI), radius * Mathf.Cos(sceneMaster.pMov.rot.x / 180.0f * Mathf.PI) * Mathf.Cos(((sceneMaster.pMov.rot.y + 180.0f) % 360.0f) / 180.0f * Mathf.PI)) + (sceneMaster.pMov.transform.position - sceneMaster.pMov.cam.transform.position);
        // pull = ShadowOffset + CamToBody
        phaseDown = strayShadowAbility.phasePause && !plf;
        phaseUp = (!strayShadowAbility.phasePause) && plf;
        //
        t = transform.position + pull - sceneMaster.pMov.transform.position;
        if (Input.GetKey(KeyCode.Mouse1) || (speed <= 0.1f && !strayShadowAbility.phasePause)) {
            if (strayShadowAbility.phasePause)
            {
                strayShadowAbility.phasePause = false;
                if (t.magnitude > 0.5f)
                    sceneMaster.pMov.SetSpeed(launchSpeedMultiplier * t);
                sceneMaster.pMov.rb.useGravity = true;
            }
            strayShadowAbility.strayPresent = false;
            strayShadowAbility.sa = false;
            //Destroy(sphere.gameObject);
            strayShadowAbility.localTimer = strayShadowAbility.cooldown;
            SFXController.controller.PlaySFX("Sheen");
            Destroy(gameObject);
            return;
        }

        if (phaseDown)
        {
            radius = (transform.position - sceneMaster.pMov.cam.transform.position).magnitude;
            sceneMaster.pMov.rb.velocity = Vector3.zero;
            sceneMaster.pMov.rb.useGravity = false;
            SFXController.controller.PlaySFX("Metalimpact");
            //
        }
        if (phaseUp)
        {
            if (t.magnitude > 0.5f)
                sceneMaster.pMov.SetSpeed(launchSpeedMultiplier * t);
            sceneMaster.pMov.rb.useGravity = true;
        }

        if (!strayShadowAbility.phasePause)
        {
            speed -= (0.7f * speed * Time.deltaTime);
            shell.velocity = speed * direction;
        }
        else
        {
            shell.velocity = Vector3.zero;
            //sphere.transform.position = transform.position + pull;
            sceneMaster.pMov.transform.position += t * (force * Time.deltaTime / Mathf.Clamp(Mathf.Sqrt(t.magnitude), 1.0f, float.PositiveInfinity) * (sceneMaster.pMov.isColliding?0.1f:1.0f));
        }
        //
        plf = strayShadowAbility.phasePause;
    }
}
