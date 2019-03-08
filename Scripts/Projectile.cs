using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public float speed = 20.0f;
	public int damage = 40;
	public Rigidbody2D rb;
    
    void Start()
    {
		rb.velocity = transform.right * speed;
    }

    
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			Destroy(gameObject);
		}

		if(collision.gameObject.tag == "Enemy")
		{
			Destroy(gameObject);
		}

		Enemy enemy = collision.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}
	}
}
