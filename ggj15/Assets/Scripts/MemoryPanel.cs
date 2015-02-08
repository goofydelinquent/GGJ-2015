using UnityEngine;
using System.Collections;
using System;

public class MemoryPanel : Panel
{	
	public FocusBeacon m_trigger;

	private Bubble m_bubble;
	
	private float m_distance;

	private string m_key;

	private bool m_bDone = false;

	private bool m_bCheckForInput = false;

	Quote m_quote = null;
	public Quote Quote { get { return m_quote; } set{ m_quote = value; } }

	protected override void Start() 
	{
		if( m_trigger == null ) { return; }

		m_key = GetLetter().ToString();

		GameObject bubbleObject = Instantiate( Resources.Load( "Prefabs/Bubble" ) ) as GameObject;
		bubbleObject.transform.parent = transform;

		Beacon beacon = m_trigger.GetComponent<Beacon>();

		if( beacon != null ) {
			bubbleObject.transform.position = beacon.GetPosition() + Quaternion.AngleAxis( UnityEngine.Random.value * 360.0f, Vector3.back ) * Vector3.up;
		}

		m_bubble = bubbleObject.GetComponent<Bubble>();

		m_bubble.SetText( m_key );
	}

	public bool HasTrigger () {
		return (m_trigger != null || m_bDone );
	}

	public bool HasTriggerActivated() {
		return m_bDone;
	}

	protected override void Update() 
	{
		if( m_trigger == null ) { return; }

		if( m_bDone ) { return; }

		if( PanelManager.Instance.CurrentPanelIndex != m_index ) { return; }

		//m_distance = Vector2.Distance( PlayerController.Instance.transform.position, m_trigger.transform.position );

		m_bCheckForInput = Mathf.Abs(PlayerController.Instance.transform.position.x - m_trigger.transform.position.x) < 1.5f;

		m_bubble.Fade( m_bCheckForInput );
		//m_text.gameObject.SetActive( m_bCheckForInput );

		if( m_bCheckForInput )
		{
			bool bPass = false;

#if UNITY_ANDROID && !UNITY_EDITOR
			if( Input.touchCount == 1 )
			{
				if( Vector3.Distance( Camera.main.WorldToScreenPoint( m_bubble.transform.position ), Input.GetTouch( 0 ).position ) < 200 ) {
					bPass = true;
				}
			}
#else
			bPass = Input.GetKeyDown( (KeyCode)Enum.Parse( typeof( KeyCode ), m_key ) );
#endif
			if( bPass )
			{
				PanelManager.Instance.AddSequence();

				Debug.Log( "KEY DOWN" );
				ChimePlayer.Instance.PlaySound();

				m_bubble.gameObject.SetActive( false );

				Sprite memory = m_trigger.transform.GetComponent<Beacon>().Memory;

				MemoryController.Instance.ShowMemory( memory );

				Destroy( m_trigger.gameObject );

				m_bDone = true;

				PlayerController.Instance.Controllable = false;

				FadeController.Instance.FadeOut();

#if DEBUG_PAT
				Invoke( "PlayerDelayControllable", 1.0f );
#else
				Invoke( "PlayerDelayControllable", 0.35f );
#endif
			}
		}
	}

	private void PlayerDelayControllable()
	{
		PlayerController.Instance.Controllable = true;
	}

	public string GetLetter()
	{
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		return chars[ UnityEngine.Random.Range( 0, chars.Length )].ToString();
	}
}
