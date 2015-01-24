using UnityEngine;
using System.Collections;

public class MemoryPanel : Panel {
	
	public Transform m_trigger;
	public Transform m_tempPlayer;
	public GameObject m_prompt;
	
	private float m_distance;
	private TextMesh m_beaconLetter;

	// Use this for initialization
	protected override void Start () 
	{
		m_prompt.SetActive(false);
		if ( m_prompt )
		{
			m_beaconLetter = m_prompt.GetComponent<TextMesh>();
			m_beaconLetter.text = GetLetter().ToString();
		}
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		m_distance = Vector2.Distance (m_tempPlayer.position, m_trigger.position);
		//Debug.Log (m_distance);
		
		if (m_prompt) 
		{
			if (m_distance < 1.5f) 
			{
				m_prompt.SetActive (true);
			} 
			else 
			{
				m_prompt.SetActive (false);
			}
		}
	}

	public static char GetLetter()
	{
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		System.Random rand = new System.Random();
		int num = rand.Next(0, chars.Length -1);
		return chars[num];
	}
}
