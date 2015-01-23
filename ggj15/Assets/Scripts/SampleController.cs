using UnityEngine;
using System.Collections;

public class SampleController : MonoBehaviour
{

	private static SampleController m_instance;
	public static SampleController Instance { get { return m_instance; } }

	private void Awake()
	{
		m_instance = this;
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
