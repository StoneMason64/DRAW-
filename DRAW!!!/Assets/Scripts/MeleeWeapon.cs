using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyObject = other.gameObject;

        if (enemyObject.layer == 6) // projectile layer
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
