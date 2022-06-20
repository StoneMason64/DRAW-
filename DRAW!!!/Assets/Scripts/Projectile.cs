using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] 
    protected float speed = 1;

    [SerializeField]
    private float force = 100;

    protected Transform player;
    protected Rigidbody rigidbody;

    Vector3 direction;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        direction = (player.position - transform.position).normalized;

        rigidbody.AddForce(direction * force * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GameObject.Destroy(this.gameObject);
    }
}