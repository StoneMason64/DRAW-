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

    public bool fireFromCamera = false;    

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(camera.position, camera.position + camera.forward * 50, Color.blue);        
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
                gameManager.AddPoints(enemyObject.GetComponent<Projectile>().Points);
                GameObject.Destroy(enemyObject);
                //Debug.Log("You Destroyed " + enemyObject.name);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Player was hit by a projectile");
            gameManager.LoseLife();
        }

    }
}
