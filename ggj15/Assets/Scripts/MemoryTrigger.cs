using UnityEngine;
using System;
using System.Collections;

public class MemoryTrigger : MonoBehaviour {

	public TextMesh m_text;

	// Use this for initialization
	void Start () 
	{
		m_text.text = GetLetter().ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public static char GetLetter()
	{
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		System.Random rand = new System.Random();
		int num = rand.Next(0, chars.Length -1);
		return chars[num];
		
	}
}
