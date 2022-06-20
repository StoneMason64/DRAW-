using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : Projectile
{
    Transform player;

    public float ThrowHeight { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").transform;
        rigidbody.velocity = CalculateLaunchData().initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    LaunchData CalculateLaunchData()
    {
        float gravity = Physics.gravity.y;
        float h = ThrowHeight;

        float displacementY = player.position.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

        // Uup = sqrt(-2gh)
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);

        // Uright = Px / ( sqrt(-2h/g) + sqrt(2(Py-h)/g) )
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

}
