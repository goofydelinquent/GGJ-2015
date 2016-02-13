using UnityEngine;
using System.Collections;

public class RendererSort : MonoBehaviour
{
	[SerializeField] string m_sortingLayer;
	[SerializeField] int m_oderInLayer;

	private void Reset()
	{
		m_sortingLayer = "Default";
		m_oderInLayer = 0;
	}

	private void Awake()
	{
		GetComponent<Renderer>().sortingLayerName = m_sortingLayer;	
		GetComponent<Renderer>().sortingOrder = m_oderInLayer;
	}
}
