using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Beacon : MonoBehaviour {

	public GameObject m_beacon;
	public Transform m_position;
	public List<Transform> m_positions = new List<Transform>();

	// Use this for initialization
	void Start () 
	{
		foreach (Transform child in m_position)
		{
			if (child) 
			{
				m_positions.Add(child);
			}
		}
		m_beacon.transform.SetParent (m_positions [Random.Range (0, m_positions.Count)]);
		m_beacon.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
