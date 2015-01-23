using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

	public Transform m_trigger;
	public Transform m_tempPlayer;

	private float m_distance;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_distance = Vector2.Distance (m_tempPlayer.position, m_trigger.position);
	}
}
