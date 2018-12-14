using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Conductor : MonoBehaviour {

    public float offset;//time of the first beat
    public float bpm;
    public float beatLength;//length of time of each beat
    public float songPosition;//current song time
    public float noteDuration;//same as the field in Note.cs


    public Conductor(float offset, float bpm, float noteDuration)
    {
        this.offset = offset;
        this.bpm = bpm;
        this.beatLength = 60.0f / bpm;
        this.noteDuration = noteDuration;
    }

    public List<float> Generate(string path)
    {
        string[] readText = File.ReadAllLines(path);
        List<float> beatTimeList = new List<float>();
        for(int i = 0; i < readText.Length; i++)
        {
            string[] beats = readText[i].Split(' ');
            
            for(int j = 0; j < 4; j++)
            {
                if (beats[j].Equals("1"))
                {
                    float newBeat = offset + i * 4 * beatLength + j * beatLength - noteDuration;
                    beatTimeList.Add(newBeat);
                }
            }
        }


        return beatTimeList;
    }
}
