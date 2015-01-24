using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Beacon : MonoBehaviour {

	public GameObject m_beacon;
	public Transform m_position;

	//public List<int> m_memory = new List<int>();
	public string[] m_memoryName;
	private List<Transform> m_positions = new List<Transform>();
	private string m_memoryAddress = "";

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

		m_memoryAddress = m_memoryName[Random.Range(0, m_memoryName.Length)];

		m_beacon.transform.SetParent (m_positions [Random.Range (0, m_positions.Count)]);
		m_beacon.transform.localPosition = Vector3.zero;
	}

	public Vector3 GetPosition()
	{
		return m_beacon.transform.position;
	}

	public string MemoryFileAddress
	{
		get { return m_memoryAddress; }
	}
}
