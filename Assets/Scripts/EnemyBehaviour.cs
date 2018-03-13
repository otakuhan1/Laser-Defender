using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject EnemyLaser01;
	public float enemyLaserSpeed = 2f;
	public float health = 200f;
	public int scoreValue = 2;
	public AudioClip enemyFire;
	public AudioClip enemyDie;
	
	private float fireInterval;
	private float timeCounter = 0f;
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void OnTriggerEnter2D (Collider2D col){
		PlayerLaser01 beam = col.gameObject.GetComponent<PlayerLaser01>();
		if(beam){
			health -= beam.getDamage();
			beam.Hit();
			if(health<=0){
				AudioSource.PlayClipAtPoint(enemyDie,transform.position);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}
	
	//fire EnemyLaser01 every update
	void Update(){
		EnemyFire();
	}
	
	void EnemyFire(){
		fireInterval = 3f+Random.value*4f;
		timeCounter+=Time.deltaTime;
		if(timeCounter>=fireInterval){
			GameObject enemyLaser;
			enemyLaser = Instantiate(EnemyLaser01,new Vector3(transform.position.x,transform.position.y-0.5f,0),Quaternion.identity) as GameObject;
			enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-enemyLaserSpeed,0);
			AudioSource.PlayClipAtPoint(enemyFire,transform.position);
			timeCounter=0f;
		}
			
	}

}


