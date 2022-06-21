using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{ 
    protected Transform player;
    protected Rigidbody rigidbody;

    public float TimeScale { get; set; } = 1f;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.layer == 3)
            GameObject.Destroy(this.gameObject);
    }
}