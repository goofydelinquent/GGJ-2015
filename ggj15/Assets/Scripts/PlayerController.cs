using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private static PlayerController m_instance;
	public static PlayerController Instance { get { return m_instance; } }

	private bool m_bControllable = true;
	public bool Controllable { get { return m_bControllable; } set { m_bControllable = value; } }

	private Animator m_animator;

	private void Awake()
	{
		m_instance = this;

		m_animator = GetComponent<Animator>();
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

		m_animator.SetBool( "bWalk", bRight );
	}


	void FixedUpdate()
	{
		if( !m_bControllable ) { return; }

		if( bRight ) 
		{
			transform.Translate( Vector3.right * 5f * Time.deltaTime );
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
