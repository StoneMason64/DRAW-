using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] int pointsWhenDestroyed = 100;

    protected Transform player;
    protected Rigidbody rigidbody;

    public float TimeScale { get; set; } = 1f;
    public int Points { get => pointsWhenDestroyed; }

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();        
    }

    private void OnCollisionEnter(Collision collision)
    {     
        GameObject.Destroy(this.gameObject);
    }
}