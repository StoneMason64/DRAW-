using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : Projectile
{   
    public float ThrowHeight { get; set; }

    LaunchData launchData;
    int resolution = 30;
    int timeStep = 0;

    Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = CalculateLaunchData().initialVelocity;
        //launchData = CalculateLaunchData();

        //previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(timeStep < resolution)
        {
            float simulationTime = timeStep / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Physics.gravity * simulationTime * simulationTime / 2f;
            //rigidbody.velocity = displacement * resolution;
            Vector3 translation = transform.position + displacement;

            transform.position = previousPos + translation;

            previousPos = transform.position;
            timeStep++;

            //Debug.Log(translation);
        }*/

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
