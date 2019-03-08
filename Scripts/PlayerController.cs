using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Animator animator;

	public float speed;
	public float slidespeed = 10;
	private float HorizontalInput;
	private float VerticalInput;
	public float jumpForce;
	private Rigidbody2D rb;

	public Transform firePoint;
	public GameObject Projectile;

	public float health;
	public float MaxHealth = 100;

	public float knockback;
	public float knockbackLength;
	public float knockbackCount;

	public float distance;
	public float climbspeed;
	public LayerMask ladderrecog;
	public bool isClimbing;

	public bool knockFromRight;
	bool airborne;
	bool jump = false;

	

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		health = MaxHealth;
    }

    
    void Update()
    {
		if (knockbackCount <= 0)
		{
			Movement();
			Jump();
			//Slide();
		}
		else
		{
			if (knockFromRight) //determine what side player will be pushed
				rb.velocity = new Vector2(-knockback, knockback);
			if (!knockFromRight)
				rb.velocity = new Vector2(knockback, knockback);
			knockbackCount -= Time.deltaTime;
		}

		if (Input.GetButtonDown("Fire2"))//mouse 2 shoot
		{
			Fire();
		}
		else
		{
			animator.SetBool("Shooting", false);
		}
		
		animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

		//ladder mechanics
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, ladderrecog);

		if (hitInfo.collider != null)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)))
			{
				isClimbing = true;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.RightArrow)))
				isClimbing = false;
		}

		if (isClimbing == true && hitInfo.collider != null)
		{
			VerticalInput = Input.GetAxisRaw("Vertical");
			rb.velocity = new Vector2(rb.position.x, VerticalInput * climbspeed);
			rb.gravityScale = 0;
		}
		else
		{
			rb.gravityScale = 1;
		}
	}
	
	void Movement()//horizontal movement
	{
		HorizontalInput = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(HorizontalInput * speed, rb.velocity.y);
	}

	/// <summary>
	/// will ask for assistance on slide part
	/// </summary>

	/*void Slide() 
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S)))
		{
			rb.velocity = transform.right * slidespeed;
			animator.SetBool("Sliding", true);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow) || (Input.GetKeyUp(KeyCode.S)))
		{
		
			animator.SetBool("Sliding", false);
		}
	}*/

		
		/// question about variable height based on key press


	void  Jump()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.Space)))
		{
			if (!airborne)
			{
				jump = true;
				rb.velocity = Vector2.up * jumpForce;
				animator.SetBool("Jumping", true);
			}
		}	
		else
		{
			animator.SetBool("Jumping", false);
		}
		
	}

	void Fire()
	{
		Instantiate(Projectile, firePoint.position, firePoint.rotation);
		animator.SetBool("Shooting", true);
	}

	void Die()
	{
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			airborne = false;
			animator.SetBool("Jumping", false);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			airborne = true;
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.tag == "Collectable")
		{
			health = health + 10;
		}

		if (collision.gameObject.tag == "Enemy")
		{
			health = health - 10;
		}
	}

}
