using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndSceneScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text scoreText = GetComponent<Text>();
		scoreText.text = ScoreKeeper.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
