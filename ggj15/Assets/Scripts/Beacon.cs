using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Beacon : MonoBehaviour {

	public GameObject m_beacon;
	public Transform m_position;

	//public List<int> m_memory = new List<int>();
	public Sprite[] m_memories;
	private List<Transform> m_positions = new List<Transform>();
	private Sprite m_memory;

	// Use this for initialization
	void Awake () 
	{
		foreach (Transform child in m_position)
		{
			if (child) 
			{
				m_positions.Add(child);
			}
		}
		m_memory = m_memories[Random.Range(0, m_memories.Length)];

		m_beacon.transform.SetParent (m_positions [Random.Range (0, m_positions.Count)]);
		m_beacon.transform.localPosition = Vector3.zero;
	}

	public Sprite Memory
	{
		get { return m_memory; }
	}
}
