using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerControllerScript : MonoBehaviour {

    public GameObject cashPrefeb;
    public Transform startPosition;
    public Transform cashFallPosition;

    private ParticleSystem moneyBurst;
    public Text moneyText;

    public float pushBackForce;    

    public float cash;

    //movement
    public float maxSpeed;
	public bool facingRight = true;
	public bool canMove = true;

	Rigidbody2D myRB;
	Animator myAnim;
	SpriteRenderer myRenderer;

	//for jumping 
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;


	// Use this for initialization
	void Awake () {

        cash = 0;
		myRB = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		myRenderer = GetComponent<SpriteRenderer>();
        moneyBurst = gameObject.GetComponent<ParticleSystem>();

    }


    void Update(){

        print(startPosition.position);

        moneyText.text ="$"+cash.ToString();

		if(canMove && grounded && Input.GetAxis("Jump")>0){
			myAnim.SetBool("isGrounded", false);
			myRB.velocity = new Vector2(myRB.velocity.x, 0f);
			myRB.AddForce(new Vector2(0,jumpHeight), ForceMode2D.Impulse);
			grounded=false;
		}

		//check if grounded
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		myAnim.SetBool("isGrounded",grounded);

		//jumping code
		myAnim.SetFloat ("verticalVelocity", myRB.velocity.y);

        //attacking code
        if (Input.GetKeyDown("right ctrl") == true && myAnim.GetBool("punch") == false)
        {
            myAnim.SetBool("punch", true);
            Invoke("PunchReset", 0.3f);
        }

        //running code
        float move = Input.GetAxis("Horizontal");

		if(canMove){
			if(move>0 && !facingRight) Flip ();
			else if (move < 0 && facingRight) Flip ();
	

			myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);
			myAnim.SetFloat ("moveVelocity", Mathf.Abs(move));
		}else{
			myRB.velocity = new Vector2(0, myRB.velocity.y);
			myAnim.SetFloat ("moveVelocity", 0);
		}
	}


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            gameObject.transform.position = startPosition.position;
        }

        if (other.gameObject.CompareTag("Win"))
        {
            Invoke("CashFall", 0.1f);
        }
    }


    void Flip(){
		facingRight = !facingRight;
		myRenderer.flipX = !myRenderer.flipX;
	}

	public void toggleCanMoveTrue(){
		canMove = true;
	}

	public void toggleCanMoveFalse(){
		canMove = false;
	}

	public void increaseSpeed(){
		maxSpeed *= 2;
	}

    public void PunchReset()
    {
        myAnim.SetBool("punch", false);
    }

    public void PlayParticle()
    {
        cash += 50;
        moneyBurst.Emit(8);
    }

    public void CashFall()
    {
        Instantiate(cashPrefeb, cashFallPosition);
    }

}
