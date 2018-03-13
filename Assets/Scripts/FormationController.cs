using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 11f;
	public float height = 7.4f;
	public float enemyMoveSpeed = 3f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xmax;
	private float xmin;	
	 
	// Use this for initialization
	void Start () {
		float cameraDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0,cameraDistance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,cameraDistance));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		SpawnUntilFull();
	}
	
	/*
	void SpawnEnemies(){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	*/
	
	void OnDrawGizmos(){
		Gizmos.DrawWireCube(new Vector3(0,1.3f,0),new Vector3(width,height));
	}
	// Update is called once per frame
	void Update () {
		float rightEdgeFormation = transform.position.x + (width*0.5f);
		float leftEdgeFormation = transform.position.x - (width*0.5f);
		
		if(movingRight){
			transform.position += Vector3.right*enemyMoveSpeed*Time.deltaTime;
		}else{
			transform.position += Vector3.left*enemyMoveSpeed*Time.deltaTime;
		}
		
		if(leftEdgeFormation<xmin){
			movingRight = true;
		}else if(rightEdgeFormation>xmax){
			movingRight = false;
		}	
		
		//if  all enemies are dead
		if(AllMembersAreDead()){
			SpawnUntilFull();
		}
		
	}
	
	//go through all the child, if child exist, return null, if no child, return that
	//transform.
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount==0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	//spawn enemy one by one, until full
	void SpawnUntilFull(){
		Transform nextFreePos = NextFreePosition();
		if(nextFreePos){
			GameObject enemy = Instantiate(enemyPrefab,nextFreePos.position,Quaternion.identity) as GameObject;
			enemy.transform.parent = nextFreePos;
			Invoke("SpawnUntilFull",spawnDelay);
		}
		
	}
	
	bool AllMembersAreDead(){
		
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount>0){
				return false;
			}
		}
		return true;
	}
	
	
}
