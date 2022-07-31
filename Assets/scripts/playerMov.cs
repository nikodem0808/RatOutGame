using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMov : MonoBehaviour
{
    public bool movementFrozen = false, isColliding = true;
    //
    public GameObject cam, camPivot;
    public float Xspeed = 3.0f, Yspeed = 3.0f, Sens = 1.2f, camR = 0.25f, camHitboxRadius = 0.15f;
    public float camDistance = 2.0f, maxCamDistance = 3.5f, deadSpeed = 0.005f;
    public Vector3 rot;
    Vector3 mov;
    public float playerSpeed = 10.0f, yhold = 0.0f, jumpForce = 15.0f, gcounter = 0.0f, controlledDampeningFactor = 0.35f, collisionDetectionFactor = 0.01f, maxVelocity;
    public Rigidbody rb;
    public bool canJump = true, maxg = false, cmaxg = false, gallow = false, offGround = false;
    //
    public Vector3 externalMov;
    public Vector3 positionLastFrame;
    RaycastHit raycastHit;
    //
    public Vector3 playerFrontPoint, pivotLocalCamDirection;
    float v, h;
    //
    float __t, soundTimer;
    //
    public const float COLLIDER_OFFSET = 0.04239154f, COLLIDER_RADIUS = 0.45f;

    void Start()
    {
        soundTimer = 0.0f;
        movementFrozen = false;
        rot.z = 0.0f;
        Xspeed = 3.0f;
        Yspeed = 3.0f;
        Sens = 1.2f;
        camR = 0.25f;
        controlledDampeningFactor = 0.35f;
        collisionDetectionFactor = 0.05f;
        maxVelocity = 20.0f;
        deadSpeed = 3.0f;
        rb = GetComponent<Rigidbody>();
        mov = Vector3.zero;
        pivotLocalCamDirection = cam.transform.localPosition.normalized;
        rb.mass = 0.01f;
        //
    }

    void Update()
    {
        if (movementFrozen || SceneMaster.sceneMaster.gamePausedImperative) return;
        soundTimer += Time.deltaTime;

        // Camera Input Calculations
        
        rot.x = Mathf.Clamp(rot.x - Sens * Yspeed * Input.GetAxis("Mouse Y"), -90.0f, 90.0f);
        rot.y = rot.y + Sens * Xspeed * Input.GetAxis("Mouse X");
        rot.y %= 360.0f;
        camDistance = Mathf.Clamp(camDistance + 0.25f * Input.mouseScrollDelta.y, 0.0f, maxCamDistance);
        camPivot.transform.eulerAngles = rot;
        camPivot.transform.position = transform.position + Vector3.up * 0.5f;
        cam.transform.eulerAngles = rot;
        cam.transform.localPosition = pivotLocalCamDirection * camDistance;
        playerFrontPoint = new Vector3(camR * Mathf.Sin(rot.y * Mathf.PI / 180.0f), 0.0f, camR * Mathf.Cos(rot.y * Mathf.PI / 180.0f));
        transform.eulerAngles = new Vector3(0.0f, rot.y, 0.0f);
        //cam.transform.position = transform.position + playerFrontPoint;

        if (Physics.SphereCast(camPivot.transform.position, camHitboxRadius, (cam.transform.position - camPivot.transform.position).normalized, out raycastHit, camDistance, ~LayerMask.GetMask("Player"))){
            cam.transform.position = raycastHit.point + raycastHit.normal * camHitboxRadius;
        }

        // movement

        if (rb.velocity.magnitude > maxVelocity) rb.velocity = rb.velocity.normalized * maxVelocity;

        mov = Vector3.zero;

        if (Input.GetKey(playerControl.controlKeys["forward"]))
        {
            mov.x += Mathf.Sin(rot.y * Mathf.PI / 180.0f);
            mov.z += Mathf.Cos(rot.y * Mathf.PI / 180.0f);
        }
        if (Input.GetKey(playerControl.controlKeys["backward"]))
        {
            mov.x -= Mathf.Sin(rot.y * Mathf.PI / 180.0f);
            mov.z -= Mathf.Cos(rot.y * Mathf.PI / 180.0f);
        }
        if (Input.GetKey(playerControl.controlKeys["left"]))
        {
            mov.x -= Mathf.Cos(rot.y * Mathf.PI / 180.0f);
            mov.z += Mathf.Sin(rot.y * Mathf.PI / 180.0f);
        }
        if (Input.GetKey(playerControl.controlKeys["right"]))
        {
            mov.x += Mathf.Cos(rot.y * Mathf.PI / 180.0f);
            mov.z -= Mathf.Sin(rot.y * Mathf.PI / 180.0f);
        }
        if (!(mov.magnitude == 0))
        {
            mov = mov.normalized * playerSpeed * ((Input.GetKey(playerControl.controlKeys["sprint"])) ? (1.4f) : (1.0f));
            rb.velocity -= controlledDampeningFactor * (new Vector3(rb.velocity.x, 0.0f, rb.velocity.z)) * Time.deltaTime;
        }

        // mov determined

        if (!Physics.CheckBox(transform.position + Vector3.down, new Vector3(COLLIDER_RADIUS / Mathf.Sqrt(2.0f), COLLIDER_OFFSET / 2.0f, COLLIDER_RADIUS / Mathf.Sqrt(2.0f)), Quaternion.identity, ~LayerMask.GetMask("NoGroundCollision"), QueryTriggerInteraction.Ignore)){
            offGround = true;
            canJump = false;
            cmaxg = true;
        }
        else if(offGround)
        {
            offGround = false;
            canJump = true;
            cmaxg = false;
            maxg = false;
            gallow = false;
            gcounter = 0.0f;
        }

        // start of movement

        if (Input.GetKeyDown(playerControl.controlKeys["jump"]) && canJump)
        {
            rb.velocity += Vector3.up * jumpForce;
            SFXController.controller.PlaySFX("Jump");
            canJump = false;
            gallow = true;
        }
        if (Input.GetKeyUp(playerControl.controlKeys["jump"]) && gallow && !Input.GetKey(playerControl.controlKeys["jump"]))
        {
            gallow = false;
            cmaxg = true;
        }

        if(Input.GetKey(playerControl.controlKeys["jump"]) && rb.velocity.y < 0)
        {
            rb.velocity -= 0.05f * rb.velocity.y * Vector3.up;
            SFXController.controller.PlaySFX("Steam");
        }

        if (maxg)
        {
            gcounter += Time.deltaTime;
            if (gcounter >= 0.2f)
            {
                maxg = false;
                gcounter = 0.0f;
            }
            if (Input.GetKeyDown(playerControl.controlKeys["jump"]))
            {
                cmaxg = false;
                maxg = false;
                gcounter = 0.0f;
                rb.velocity -= 100.0f * Vector3.up;
                SFXController.controller.PlaySFX("Beamdown");
            }
        }
        if (Input.GetKeyDown(playerControl.controlKeys["jump"]) && !canJump && !maxg && cmaxg)
        {
            maxg = true;
        }
        //
    }

    void FixedUpdate()
    {
        rb.position = rb.position + externalMov;
        //
        if (Physics.CapsuleCast(transform.position + Vector3.up * 0.5f, transform.position + Vector3.down * 0.5f, COLLIDER_RADIUS, mov.normalized, (1.6f * mov.magnitude + rb.velocity.magnitude) * Time.deltaTime, ~LayerMask.GetMask("Player"))) rb.position = rb.position + mov.normalized * (raycastHit.distance * Time.deltaTime + 0.1f * collisionDetectionFactor);
        else rb.position = rb.position + mov * Time.deltaTime;
        //
        externalMov = Vector3.zero;
        //
        Debug.Log(mov);
        if ((rb.velocity + mov).sqrMagnitude < deadSpeed * deadSpeed + rb.velocity.y * rb.velocity.y) rb.velocity = rb.velocity.y * Vector3.up;
        positionLastFrame = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dead"))
        {
            SceneMaster.sceneMaster.OnGameOver();
            return;
        }
        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    public void SetSpeed(Vector3 arg)
    {
        rb.velocity = arg;
    }
}
