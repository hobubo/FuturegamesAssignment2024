using UnityEngine;
using Mechadroids;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Transform tank;
    Transform turret;
    Transform barrel;
    PlayerSettings settings;
    Vector2 input;
    Vector2 turretInput;

    // public Transform tankBody;
    // [Tooltip("Rotates horizontally relative to tankBody")]
    // public Transform turretBase;
    // [Tooltip("Rotates vertically relative to tankBody")]
    // public Transform barrel;
    // [Tooltip("Where the bullets come out")]
    // public Transform barrelEnd;
    //
    // [Header("Movement Parameters")]
    // public float moveSpeed =3f;
    // public float rotationSpeed = 10f;
    // public float acceleration = 2f;
    // public float deceleration = 2f;
    // public float maxSlopeAngle = 45f;
    //
    // [Header("Turret Parameters")]
    // public float turretRotationSpeed = 10f;
    // public float barrelRotationSpeed = 10f;
    // public float maxBarrelElevation = 25f;
    // public float minBarrelElevation = -45f;
    //
    // public float minTurretAngle = -180f;
    // public float maxTurretAngle = 180f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        var playerRef = GetComponent<PlayerReference>();
        settings = playerRef.playerSettings;
        tank = playerRef.tankBody;
        turret = playerRef.turretBase;
        barrel = playerRef.barrel;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        turretInput.x += Input.GetAxis("Mouse X");
        turretInput.y -= Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        Move();
        Aim();
    }

    void Move()
    {
        rb.position += input.y * tank.forward * Time.deltaTime * settings.moveSpeed;
        tank.Rotate(0, input.y * input.x * Time.deltaTime * settings.rotationSpeed, 0);
    }

    void Aim()
    {
        turretInput.x = Mathf.Clamp(turretInput.x, settings.minTurretAngle, settings.maxTurretAngle);
        turretInput.y = Mathf.Clamp(turretInput.y, settings.minBarrelElevation, settings.maxBarrelElevation);
        turret.rotation = Quaternion.Euler(0, turretInput.x, 0);
        barrel.rotation = Quaternion.Euler(turretInput.y, turretInput.x, 0);
        Camera.main.transform.position = barrel.position - turret.forward * 8;
        Camera.main.transform.rotation = turret.rotation;
    }
}
