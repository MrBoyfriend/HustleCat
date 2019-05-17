using UnityEngine;
using System.Collections;

public class enemyMovementController : MonoBehaviour {

	Animator enemyAnimator;

	public float enemySpeed;
	public float maxSpeed = 20f;

	//facing
	bool canFlip = true;
	bool facingRight = false;
	float flipTime = 5f;
	float nextFlipChance = 0f;
	Transform theGraphic;

	//attack
	public float chargeTime;
	float startChargeTime;
	bool charging = false;
	Rigidbody2D enemyRB;

	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponentInChildren<Animator> ();
//		theGraphic = GetComponentInChildren<Transform>();
		theGraphic = GetComponent<Transform>();
		enemyRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextFlipChance) {
			if (Random.Range (0, 10) >= 5)
				flipFacing ();
			nextFlipChance = Time.time + flipTime;
		}

		if(charging && Time.time>startChargeTime && enemyRB.velocity.x < maxSpeed){
			if(!facingRight)  enemyRB.AddForce (new Vector2 (-1,0)*enemySpeed);
			else enemyRB.AddForce (new Vector2 (1,0)*enemySpeed);
		}
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			//are we facing the player?
			if(facingRight && other.transform.position.x < transform.position.x){
				flipFacing();
			}else if(!facingRight && other.transform.position.x > transform.position.x){
				flipFacing();
			}
			canFlip = false;
			charging = true;
			startChargeTime = Time.time + chargeTime;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			charging = true;
		}
	}
			

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			canFlip = true;
			charging = false;
			enemyRB.velocity = new Vector2(0f,0f);
			enemyAnimator.SetBool("isCharging", charging);

		}
	}


	void flipFacing(){
		if (!canFlip)
			return;
		float facingX = theGraphic.localScale.x;
		facingX *= -1f;
		theGraphic.localScale = new Vector3(facingX, theGraphic.localScale.y, theGraphic.localScale.z);
		facingRight = !facingRight;
	}
}
