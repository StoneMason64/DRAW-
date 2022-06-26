using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public Transform revolver;
    public Transform tommahawk;
    public Transform crosshair;

    public GameManager gameManager;

    [SerializeField]
    bool fireFromCamera = false;
    [SerializeField]
    bool showLineRenderer = false;

    float meleeRadius = 0.001f;

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        if(showLineRenderer)
            line = revolver.GetComponent<LineRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(camera.position, camera.position + camera.forward * 50, Color.blue);

        //UseMeleeWeapon();          
    }

    public void FireGun()
    {
        if (!gameManager.GameRunning)
            return;

        //Debug.Log("Firing gun from position:" + revolver.position);        

        RaycastHit hit;
        Transform fireOrigin;

        if (fireFromCamera)
            fireOrigin = camera.transform;
        else
            fireOrigin = revolver;

        if (showLineRenderer)
        {
            line.positionCount = 2;
            line.SetPosition(0, fireOrigin.position);
        }

        //Debug.DrawRay(fireOrigin.position, fireOrigin.forward, Color.blue, 1);

        //Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 fireDirection = (crosshair.position - fireOrigin.position).normalized;

        if (Physics.Raycast(fireOrigin.position, fireDirection, out hit))
        {
            //Debug.Log("Raycast hit " + hit.transform.name);
            if (showLineRenderer)
                line.SetPosition(1, hit.point);

            var enemyObject = hit.transform.gameObject;

            if (enemyObject.layer == 6) // projectile layer
            {            
                var projectile = enemyObject.GetComponent<Projectile>();

                if (projectile.CanBeShot)
                {
                    gameManager.AddPoints(projectile.Points);
                    GameObject.Destroy(enemyObject);
                    Debug.Log(enemyObject.name + "Was destroyed by revolver");
                }
                else
                {
                    Debug.Log("Object cannot be shot");
                }
            }
        }
        else
        {
            if (showLineRenderer)
                line.SetPosition(1, fireOrigin.position + (fireDirection * 50));
        }

    }

    public void ClearLineRenderer()
    {
        line.positionCount = 0;
    }

    // Not currently used
    private void UseMeleeWeapon()
    {
        Ray ray = new Ray(tommahawk.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, meleeRadius, out hit))
        {
            GameObject enemyObject = hit.transform.gameObject;

            if (enemyObject.layer == 6)
            {
                var projectile = enemyObject.GetComponent<Projectile>();

                if (projectile.DestroyedByMelee)
                {
                    gameManager.AddPoints(projectile.Points);
                    GameObject.Destroy(enemyObject);
                    Debug.Log(enemyObject.name + "Was destroyed by tomahawk");
                }
                else
                {
                    Debug.Log("Object cannot be destroyed by tomahawk");
                }

            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Player was hit by a projectile");
            gameManager.LoseLife();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(tommahawk.position + transform.forward, meleeRadius);
    }

}
