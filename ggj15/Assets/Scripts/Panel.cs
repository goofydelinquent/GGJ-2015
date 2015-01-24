using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

	public Transform m_trigger;
	public Transform m_tempPlayer;
	public GameObject m_prompt;

	private float m_distance;

	// Use this for initialization
	void Start () 
	{
		m_prompt.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_distance = Vector2.Distance (m_tempPlayer.position, m_trigger.position);
		//Debug.Log (m_distance);

		if (m_distance < 1.5f) 
		{
			Debug.Log ("TRIGGER~!");
			m_prompt.SetActive (true);
		} 
		else 
		{
			m_prompt.SetActive(false);
		}
	}
}
