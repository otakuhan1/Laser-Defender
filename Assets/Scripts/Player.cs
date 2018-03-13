using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject laserPrefab;
	public float laserSpeed = 15.0f;
	public float speed = 7.0f;
	public float padding = 0.7f;
	public float firingRate = 0.3f;
	public float playerHealth = 250f;
	public AudioClip playerFireClip;
	
	private float minX;
	private float maxX;
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		minX = leftMost.x + padding;
		maxX = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement();
		PlayerFire();
		
	}
	
	void PlayerMovement(){
		if(Input.GetKey(KeyCode.LeftArrow)){
			this.transform.position+=Vector3.left*speed*Time.deltaTime;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			this.transform.position+=Vector3.right*speed*Time.deltaTime;
		}
		
		float newX = Mathf.Clamp(transform.position.x,minX,maxX);
		transform.position = new Vector3(newX,transform.position.y,transform.position.z);
		
	}
	
	void Fire(){
		GameObject playerLaser = Instantiate(laserPrefab,new Vector3(transform.position.x,-3.27f,0),Quaternion.identity) as GameObject;
		playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0,laserSpeed,0);
		AudioSource.PlayClipAtPoint(playerFireClip,transform.position);
	}
	
	void PlayerFire(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire",0.00001f,firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}	
	}
	
	void OnTriggerEnter2D (Collider2D col){
		EnemyLaser01 beam = col.gameObject.GetComponent<EnemyLaser01>();
		if(beam){
			playerHealth -= beam.getDamage();
			beam.Hit();
			if(playerHealth<=0){
				Destroy(gameObject);
				Application.LoadLevel("Lose Screen");
			}
		}
	}
}
