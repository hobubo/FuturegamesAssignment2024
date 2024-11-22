using UnityEngine;
using Mechadroids;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Transform tank;
    Transform turret;
    Transform barrel;
    new Transform camera;
    PlayerSettings settings;
    Vector2 input;
    Vector2 rotate;
    float speed;

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
        camera = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        rotate.x += Input.GetAxis("Mouse X");
        rotate.y -= Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        Move();
        Aim();
        Look();
    }

    void Move()
    {
        speed += input.y * settings.acceleration;
        speed -= Mathf.Sign(speed) * settings.deceleration;
        speed = Mathf.Clamp(speed, - settings.moveSpeed, + settings.moveSpeed);
        rb.position += speed * tank.forward * Time.deltaTime;
        tank.Rotate(0, speed * input.x * Time.deltaTime * settings.rotationSpeed, 0);
    }

    void Aim()
    {
        rotate.x = Mathf.Clamp(rotate.x, settings.minTurretAngle, settings.maxTurretAngle);
        rotate.y = Mathf.Clamp(rotate.y, settings.minBarrelElevation, settings.maxBarrelElevation);
        turret.localRotation = Quaternion.Euler(0, rotate.x, 0);
        barrel.localRotation = Quaternion.Euler(rotate.y, 0, 0);
    }

    void Look()
    {
        camera.rotation = Quaternion.Euler(rotate.y/3 + 15, turret.eulerAngles.y, 0);
        camera.position = barrel.position - camera.forward * 10 + Vector3.up;
    }
}
