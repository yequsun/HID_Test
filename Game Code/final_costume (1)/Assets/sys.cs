using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sys : MonoBehaviour {
	public float radius = 6f;
	float timer;
	public GameObject note;
    public GameObject gNote;
    public GameObject bNote;
	public Text comboTxt;
	public int comboCount;

    public Text scoreText;
    public float score;
    float currentBeat;

    [Header("Song Settings")]
    public float offset;
    public float bpm;
    public float note_Duration;
    public string path;
    //
    Conductor c1;
    List<float> beatMapping;
    
    Queue<float> beatQueue;
	// Use this for initialization
	void Start () {
		timer = 0f;
		score = 0;
		comboCount = 0;

        /////////
        c1 = new Conductor(offset, bpm, note_Duration);
        beatMapping = c1.Generate(path);
        Debug.Log(beatMapping);
        beatQueue = new Queue<float>(beatMapping);
        currentBeat = beatQueue.Dequeue();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (comboCount > 0) {
			comboTxt.text = comboCount + " Combo";
		} else {
			comboTxt.text = "";
		}

		// for the prototype it randomly spawns a note every second.
		// in the end we should design this to be in sync with the music
        /*
		if (timer >= 1) {
			spawnNote ();
			timer = 0;
		}
        */
        if(timer >= currentBeat && beatQueue.Count>0)
        {
            spawnNote();
            currentBeat = beatQueue.Dequeue();
        }
	}

	void spawnNote(){
		var position = Random.onUnitSphere * radius; // if we only want the note to appear in front of the player, take the abs of the z value
        //we need to rng pick a note to spawn
        float r = Random.Range(1.0f, 10.0f);
        if (r < 7.0f){
            Instantiate(note, position, Quaternion.identity);
        }
        else if (r < 8.5f)
        {
            Instantiate(gNote, position, Quaternion.identity);
            //Debug.Log("good Note");
        }
        else
        {
            Instantiate(bNote, position, Quaternion.identity);
            //Debug.Log("Bad note");
        }
        
	}

	public void updateCombo(){
		comboCount++;
		StartCoroutine (comboEff ());
	}

	IEnumerator comboEff()
	{
		comboTxt.fontSize = 72;
		yield return new WaitForSeconds(.1f);
		comboTxt.fontSize = 54;
	}

    public void updateScore(float toAdd)
    {
        if (toAdd > 0)
        {
            if (comboCount <= 5)
            {
                toAdd = toAdd * 1;
                comboTxt.color = Color.white;
            }
            else if(comboCount <= 10)
            {
                toAdd = toAdd * 2;
                comboTxt.color = Color.yellow;
            }
            else if (comboCount <= 15)
            {
                toAdd = toAdd * 3;
                comboTxt.color = Color.red;
            }
            else 
            {
                toAdd = toAdd * 4;
                comboTxt.color = Color.cyan;
            }
        }
        score += toAdd;
        scoreText.text = "Score: " + score;
    }
    IEnumerator healthEff()
    {
        scoreText.fontSize = 72;
        yield return new WaitForSeconds(0.1f);
        scoreText.fontSize = 54;
    }
}
