using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;

	void Start () {
		//if there is already another instance of MusicPlayer exist
		if(instance != null && instance != this){
			//destroy the one we are trying to create
			Destroy(gameObject);
			print ("Duplicate music player self destroied");
		}else{
			instance = this;
			//when loading a new scene, this object will not be destroied
			GameObject.DontDestroyOnLoad(gameObject);
			//get audio source component which is in MusicPlayer object
			music = GetComponent<AudioSource>();
			//at the start, the OnLevelWasLoaded() just not been called, so we add ourselves
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}
	
	//a method which takes care of different BGM when different scene was loaded
	void OnLevelWasLoaded(int level){
		Debug.Log ("MusicPlayer: loaded level" + level);
		//stop existing music
		music.Stop ();
		if (level == 0){
			music.clip = startClip;
		}
		if (level == 1){
			music.clip = gameClip;
		}
		if (level == 2){
			music.clip = endClip;
		}
		music.loop = true;
		music.Play();
	}
}
