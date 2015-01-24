using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private static PlayerController m_instance;
	public static PlayerController Instance { get { return m_instance; } }

	private void Awake()
	{
		m_instance = this;
	}

	bool bRight = false;

	void Update()
	{
		if( Input.GetKey( KeyCode.RightArrow ) ) 
		{
			bRight = true;
		}
		else
		{
			bRight = false;
		}
	}


	void FixedUpdate()
	{
		if( bRight ) 
		{
			transform.Translate( Vector3.right * 2.0f * Time.deltaTime );
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
