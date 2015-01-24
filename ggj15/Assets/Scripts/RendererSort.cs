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
		renderer.sortingLayerName = m_sortingLayer;	
		renderer.sortingOrder = m_oderInLayer;
	}
}
