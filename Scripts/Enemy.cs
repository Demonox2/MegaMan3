using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public Animator animator;

	public int health = 100;

	void Start()
    {
        
    }

    
    void Update()
    {
        
    }

	public void TakeDamage (int damage)
	{
		health -= damage;
		
		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
		Destroy(gameObject);
	}



}
