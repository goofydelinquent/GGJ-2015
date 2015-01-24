using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private static PlayerController m_instance;
	public static PlayerController Instance { get { return m_instance; } }

	private bool m_bControllable = true;
	public bool Controllable { get { return m_bControllable; } set { m_bControllable = value; } }

	private Animator m_animator;

	private bool m_bIsWalkForced = false;

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

		if ( m_bIsWalkForced ) {
			bRight = true;
		}

		m_animator.SetBool( "bWalk", bRight );
	}

	public void ForceSetWalking( bool p_bIsWalking ) {
		m_bIsWalkForced = p_bIsWalking;
	}


	void FixedUpdate()
	{
		if ( !m_bIsWalkForced && !m_bControllable) { return; }

		//bypass debug
		if ( m_bIsWalkForced ) {
			transform.Translate( Vector3.right * 2f * Time.deltaTime );
			return;
		}


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
