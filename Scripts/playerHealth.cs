using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

	public float fullHealth;

    //	public restartGame theGameManager;

    public GameObject restartButton;

	float currentHealth;

	public AudioClip playerDamaged;
	AudioSource playerAS;

	//HUD references
	public Image healthSlider;
	public Image damageIndicator;


    public Image healthImage;
    public Sprite topHealth;
    public Sprite medHealth;
    public Sprite lowHealth;

    public Text rubyCount;
	public CanvasGroup endGameCanvas;
	public Text EndGameText;

	Color flashColour = new Color (255f, 255f, 255f, 0.5f);
	float indicatorSpeed = 5f;

	//Player death
	playerControllerScript controlMovement;
	bool isDead;
	bool damaged;
	public GameObject playerDeathFX;


	//ruby collection
	int collectedRubies = 0;


	// Use this for initialization
	void Start () {
		currentHealth = fullHealth;
		healthSlider.fillAmount = 0f;
		controlMovement = GetComponent<playerControllerScript> ();
		playerAS = GetComponent<AudioSource> ();
		//rubyCount.text = collectedRubies.ToString();
	}
	
	// Update is called once per frame
	void Update () {

		//are we damaged?
        if(currentHealth >= 8)
        {
            healthImage.sprite = topHealth;
        } else if( currentHealth >= 4)
        {
            healthImage.sprite = medHealth;
        }
        else if (currentHealth > 0)
        {
            healthImage.sprite = lowHealth;
        }

    }

	public void addDamage(float damage){
		if (damage <= 0)
			return;
		currentHealth -= damage;
		healthSlider.fillAmount = 1 - currentHealth/fullHealth;
		playerAS.clip = playerDamaged;
		playerAS.PlayOneShot (playerDamaged);

		damaged = true;
		if (currentHealth <= 0)
			makeDead ();
	}

	public void addHealth(float health){
		currentHealth += health;
		if (currentHealth > fullHealth)
			currentHealth = fullHealth;
		healthSlider.fillAmount = 1 - currentHealth/fullHealth;
	}

	public void makeDead(){
		//kill the player - death particles - destroy character - sound
		isDead = true;
		//Instantiate (playerDeathFX, transform.position, transform.rotation);
		damageIndicator.color = flashColour;
		EndGameText.text = "You Died!";
        restartButton.SetActive(true);
        winGame();
		Destroy (gameObject);
	}

	public void addRuby(){
		collectedRubies +=1;
		rubyCount.text = collectedRubies.ToString();
		if(collectedRubies>2){
			EndGameText.text = "You Win!";
			GetComponent<playerControllerScript>().toggleCanMoveFalse();
			winGame();
		}
	}

	public void winGame(){
		//endGameCanvas.alpha = 1f;
		//endGameCanvas.interactable = true;
	}
}
