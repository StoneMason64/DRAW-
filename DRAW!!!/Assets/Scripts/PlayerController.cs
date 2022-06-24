using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform camera;
    public Transform revolver;
    public Transform tommahawk;

    public GameManager gameManager;

    [SerializeField]
    bool fireFromCamera = false;

    float meleeRadius = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(camera.position, camera.position + camera.forward * 50, Color.blue);

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
            fireOrigin = camera;
        else
            fireOrigin = revolver;

        //Debug.DrawRay(fireOrigin.position, fireOrigin.forward, Color.blue, 3);
        

        if(Physics.Raycast(fireOrigin.position, fireOrigin.forward, out hit))
        {
            //Debug.Log("Raycast hit " + hit.transform.name);

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

    }

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
