using UnityEngine;
using System.Collections;

public class EnemyLaser01 : MonoBehaviour {

	public float damage = 100f;
	public void Hit(){
		Destroy(gameObject);
	}
	public float getDamage(){
		return damage;
	} 
}
