using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	public float duration = 1;
	float timer;
	Vector3 scaleDiff;
	sys system;
	public ParticleSystem goodEnd, badEnd;
	AudioSource goodSound, badSound;

    float score = 10;
    float neutral_score = 10;
    float same_score = 15;
    float diff_score = 5;
	//experiment

    
	void Start () {
		timer = duration;
		Color color = this.GetComponentsInChildren<MeshRenderer>()[1].material.color;
		color.a = .5f;
		this.GetComponentsInChildren<MeshRenderer>()[1].material.color = color;
		scaleDiff = transform.GetChild (0).gameObject.transform.localScale - gameObject.transform.localScale;
		system = FindObjectsOfType<sys> ()[0];
		goodSound = GetComponents<AudioSource> ()[0];
		badSound = GetComponents<AudioSource> ()[1];
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			handleResult (-1);
			Destroy (gameObject);
			system.comboCount = 0;
		}

		//shrink the timer
		transform.GetChild (0).gameObject.transform.localScale -= scaleDiff * (Time.deltaTime / duration);
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "goodEnd"){
            // do something different for the two ends?
            //maybe we play two different kinds of sounds 
            //Debug.Log("GOOD SOUND BING");
			goodEnd.Play();
			goodSound.Play();
            if (this.gameObject.tag == "neutral")
            {
                score = neutral_score;
            }
            else if (this.gameObject.tag == "good")
            {
                score = same_score;
                system.updateCombo();
            }
            else if (this.gameObject.tag == "bad")
            {
                score = diff_score;
            }
		}else if (col.gameObject.tag == "badEnd"){
			badEnd.Play();
			badSound.Play();
            if (this.gameObject.tag == "neutral")
            {
                score = neutral_score;
            }
            else if (this.gameObject.tag == "good")
            {
                score = diff_score;
            }
            else if (this.gameObject.tag == "bad")
            {
                score = same_score;
                system.updateCombo();
            }
        }
		system.updateCombo ();
        handleResult(timer);
		GetComponent<Renderer> ().enabled = false;
		GetComponentsInChildren<Renderer> ()[1].enabled = false;
		GetComponent<Collider> ().enabled = false;
		Destroy (gameObject, goodEnd.main.duration);
	}

	// diff is the remaining time on that note if hit, -1 if miss
	// diff is used in case we want to have a score based on that
	void handleResult(float diff = 0){
		if (diff == -1) {
            
        } else {
            float scoreToGive = score;
            system.updateScore(scoreToGive);
		}
	}
}
