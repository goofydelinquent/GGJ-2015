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
	private bool m_bIsFacingRight = true;

	private bool m_bIsEndCutscene = false;

	private void Awake()
	{
		m_instance = this;

		m_animator = GetComponent<Animator>();
#if !UNITY_WEBPLAYER
		Screen.showCursor = false;
#endif
	}

	bool bRight = false;
	bool bLeft = false;
	bool bIsMoving = false;

	void Update()
	{		
		if( !m_bIsEndCutscene ) {

			bRight = Input.GetKey( KeyCode.RightArrow );

			if( !bRight ) {
				bLeft = Input.GetKey( KeyCode.LeftArrow );
			}
			else {
				bLeft = false;
			}
		}

		bIsMoving = bRight || bLeft;

		if ( m_bIsWalkForced ) {
			bIsMoving = true;
			bRight = true;
		}

		m_animator.SetBool( "bWalk", bIsMoving );

		if ( bIsMoving && ( (bLeft && m_bIsFacingRight) || (!bLeft && !m_bIsFacingRight ) ) ) {
			Vector3 scale = this.transform.localScale;
			scale.x = bRight ? 1 : -1;
			this.transform.localScale = scale;
			m_bIsFacingRight = !m_bIsFacingRight;
		}
	}

	public void PlayEndCutscene()
	{
		MyCameraController.Instance.FocusToEndPanel();

		m_bIsEndCutscene = true;
		bRight = false;
		bLeft = false;
		bIsMoving = false;
		ForceSetWalking( true );
		Invoke( "EndCutsceneCallback", 3.2f );
	}

	private void EndCutsceneCallback()
	{
		ForceSetWalking( false );
		bRight = false;
		bLeft = false;
		bIsMoving = false;
		m_animator.SetBool( "bTurn", true );

		Invoke( "FadeToWhite", 5.0f );
	}

	private void FadeToWhite()
	{
		gameObject.SetActive( false );
		PanelManager.Instance.gameObject.SetActive( false );
		Invoke( "CreditsCallback", 2.0f );
	}
	
	private void CreditsCallback()
	{
		GameObject go = Instantiate (Resources.Load ("Prefabs/Credits")) as GameObject;
		go.transform.position = new Vector3( PanelManager.Instance.PanelSize.x * PanelManager.Instance.CurrentPanelIndex, 0, 0 );
		Invoke( "RestartLevel", 5.0f );
	}

	private void RestartLevel()
	{
		Application.LoadLevel (0);
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

		float distance = GetSpeed() * Time.fixedDeltaTime * ( bRight ? 1 : -1 );

		if ( bLeft ) {
			int index = PanelManager.Instance.CurrentPanelIndex;
			float minX = (PanelManager.Instance.PanelSize.x * index );
			if ( transform.position.x + distance < minX ) {
				bIsMoving = false;
			}
		}

		if( bIsMoving ) 
		{
			transform.Translate( Vector3.right * distance );
		}
	}

	private float GetSpeed()
	{
		if( transform.position.x < ( 2 * PanelManager.Instance.PanelSize.x ) ) {
			return 1.5f;
		}
		else {
			float percentage = ( transform.position.x - ( 2 * PanelManager.Instance.PanelSize.x ) ) / PanelManager.Instance.PanelSize.x;

			return 1.5f + Mathf.Min( 1.5f * percentage, 1.5f );
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
