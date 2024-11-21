using UnityEngine;
using Mechadroids;

public class PlayerMovment : MonoBehaviour
{
    Rigidbody rb;
    Transform tank;
    Transform turret;
    PlayerSettings settings;
    Vector2 input;

    // public Transform tankBody;
    // [Tooltip("Rotates horizontally relative to tankBody")]
    // public Transform turretBase;
    // [Tooltip("Rotates vertically relative to tankBody")]
    // public Transform barrel;
    // [Tooltip("Where the bullets come out")]
    // public Transform barrelEnd;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        var playerRef = GetComponent<PlayerReference>();
        settings = playerRef.playerSettings;
        tank = playerRef.tankBody;
        turret = playerRef.turretBase;
    }

    void Update()
    {
        input.y = Input.GetAxisRaw("Vertical");
        input.x = Input.GetAxisRaw("Horizontal");
        rb.position += input.y * tank.forward * Time.deltaTime * settings.moveSpeed;
        tank.Rotate(0, input.y * input.x, 0);
    }
}
