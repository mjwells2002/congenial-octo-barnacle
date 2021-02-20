using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class ZeroGMovement : MonoBehaviour
{
    private PlayerInput playerInput;
	private InputAction moveAction;
	private InputAction lookAction;
	private InputAction fireAction;
	private InputAction pauseAction;
	private Vector2 moveForce;
	private Vector3 lookVector;
	private Rigidbody2D rb;
	private Camera localCamera;
	private Vector2 lastForce;
	private AudioSource _a;
	private bool isFiring;
	public int currentShots = 0;

	public int maxShot = 1;
	public float velocityX = 10f;
	public float velocity = 100f;
	[Range(0f,100f)] public int EFireChance = 2;
	[Range(0f,1f)] public float EReloadSpeed = 1f;
	public GameObject bullet;

	public int explosiveShotChance = 1;
	public float defaultMaxHealth = 250f;
	public float maxHealth = 250f;
	public float currentHealth;
	public float healthRegenspeed = 10f;
	public bool isInMotion = false;
	public int score = 0;
	public static Transform playerInstance;
	public static ZeroGMovement instance;
	public static int EFirespeed;
	public static float EReloadspeed;
	
	private void Start()
	{
		_a = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D>();
		localCamera = Camera.main;
		playerInstance = transform;
		EFirespeed = EFireChance;
		EReloadspeed = EReloadSpeed;
		instance = this;
		currentHealth = maxHealth;
		Cursor.lockState = CursorLockMode.Confined; 
		_a.volume = AudioController.instance.SFXVolume;
	}
	
	private void Update()
	{
		Pterodyactl.finalScorePterodyactl = score;
		//doFire();
		EFirespeed = EFireChance;
		EReloadspeed = EReloadSpeed;
		if(playerInput == null)
		{
			playerInput = GetComponent<PlayerInput>();
			moveAction = playerInput.actions["Move"];
			lookAction = playerInput.actions["Aim"];
			fireAction = playerInput.actions["Fire"];
			pauseAction = playerInput.actions["Pause"];

			pauseAction.performed += ctx => PauseMenu.instance.doPause();

			playerInput.onControlsChanged += ctx => {
				Debug.Log($"Active Control Scheme {playerInput.currentControlScheme}");
			};

			moveAction.performed += ctx => {
				if(Time.timeScale == 0f)
					return;
				moveForce = ctx.ReadValue<Vector2>();
				rb.drag = 0.25f;
				isInMotion = true;
			};
			moveAction.canceled += ctx => {
				if(Time.timeScale == 0f)
					return;
				moveForce = Vector2.zero;
				rb.drag = 3.5f;
				isInMotion = false;
			};
			
			lookAction.performed += ctx => 
			{
				lookVector = ctx.ReadValue<Vector2>();
				
			};
			lookAction.canceled += ctx => lookVector = Vector2.zero;

			fireAction.performed += ctx => {
				if(Time.timeScale == 0f)
					return;
				isFiring = true;
			};
			fireAction.canceled += ctx => {
				if(Time.timeScale == 0f)
					return;
				isFiring = false;
				currentShots = 0;
			};
		}
		
		if(isFiring && currentShots < maxShot)
		{
			doFire();
			currentShots+=1;
			if(currentShots < maxShot)
			{
				doFire();
				currentShots+=1;
			}
		}

		if(currentHealth <= 0)
		{
			SceneManager.LoadScene("Death");
		}

		/* if(currentHealth >0f && currentHealth < maxHealth){
			currentHealth+=Time.deltaTime*healthRegenspeed;
		} */
	}
	private void FixedUpdate()
	{
		rb.AddForce(moveForce*velocityX);
		if (lookVector != Vector3.zero)
		{	
			if(playerInput.currentControlScheme == "Gamepad")
			{
				//TODO: interpolate this
				Vector2 v2 = flipVectorAxis(lookVector) - Vector2.zero;
				float angle = (Mathf.Atan2(v2.y, v2.x)*Mathf.Rad2Deg);
				transform.localRotation = Quaternion.Euler(0f,0f,angle);
			} else
			{
				Vector2 deltaV = localCamera.ScreenToWorldPoint(lookVector) - transform.position;
				deltaV.Normalize();
				float angle = (Mathf.Atan2(deltaV.x, -deltaV.y)*Mathf.Rad2Deg)+180;
				transform.localRotation = Quaternion.Euler(0f,0f,angle);
			}	
		}
		rb.velocity = clampVector(rb.velocity, velocity);   
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("BadBullet")){
			Debug.Log($"currentHealth={currentHealth}");
			currentHealth-=10f*Pterodyactl.difficultyPterodyactl;
			Vector2 direction = (transform.position-other.transform.position);
			direction.Normalize();
			rb.AddForce(direction*15f,ForceMode2D.Impulse);
			other.gameObject.GetComponent<BulletLogic>().DestroyAfterTrail();
		}
	}
	
	public void eat()
	{
		if(!_a.isPlaying){
			_a.clip = AudioController.instance.pickupSFX[Random.Range(0,AudioController.instance.pickupSFX.Length)];
			_a.Play();
		}
		score += 100;
		if(currentHealth+10<maxHealth)
			currentHealth+=10*Pterodyactl.difficultyPterodyactl*2f;
	}
	private void doFire()
	{
		GameObject b = Instantiate(bullet,transform.position,transform.rotation);
		Rigidbody2D bR = b.GetComponent<Rigidbody2D>();
		b.GetComponent<BulletLogic>().isExplsive = randomChance(explosiveShotChance);
		bR.AddForce(b.transform.up*40f,ForceMode2D.Impulse);
		rb.AddForce(-transform.up*3f,ForceMode2D.Impulse);
	}

	private Vector2 clampVector(Vector2 vector,float minmax)
	{
		return new Vector2(
			Mathf.Clamp(vector.x,-minmax,minmax),
			Mathf.Clamp(vector.y,-minmax,minmax)
		);
	}

	private Vector2 flipVectorAxis(Vector2 vector)
	{
		return new Vector2(vector.y,-vector.x);
	}

	private bool randomChance(int percentageChance)
	{
		int a = (int)Random.Range(0f,100f);
		return a<=percentageChance;
	}

}