using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField][Range(0, 25f)]
    float throwHeight = 10;

    [SerializeField] bool showPath = true;

    Transform player;
    private LaunchData data;
    private float h;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        var spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
        CurvedProjectile projectile = spawnedObject.GetComponent<CurvedProjectile>();
        projectile.ThrowHeight = throwHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public LaunchData CalculateLaunchData()
    {
        float gravity = Physics.gravity.y;
        h = throwHeight;

        float displacementY = player.position.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);

        // Uup = sqrt(-2gh)
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);

        // Uright = Px / ( sqrt(-2h/g) + sqrt(2(Py-h)/g) )
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    private void OnDrawGizmos()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (!showPath || player == null)
            return;

        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = transform.position;

        int resolution = 30;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;

            // s = ut + (at^2)/2
            Vector3 displacement = launchData.initialVelocity * simulationTime + Physics.gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(previousDrawPoint, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }

}

public struct LaunchData
{
    public readonly Vector3 initialVelocity;
    public readonly float timeToTarget;

    public LaunchData(Vector3 initialVelocity, float timeToTarget)
    {
        this.initialVelocity = initialVelocity;
        this.timeToTarget = timeToTarget;
    }

}