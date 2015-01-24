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
#if DEBUG_PAT
			transform.Translate( Vector3.right * 5f * Time.deltaTime );//2
#else
			transform.Translate( Vector3.right * 2f * Time.deltaTime );
#endif
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
