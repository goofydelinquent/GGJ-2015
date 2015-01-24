using UnityEngine;
using System.Collections;
using System;

public class MemoryPanel : Panel
{	
	public FocusBeacon m_trigger;

	private TextMesh m_text;
	
	private float m_distance;

	private string m_key;

	private bool m_bDone = false;

	private bool m_bCheckForInput = false;

	protected override void Start() 
	{
		m_key = GetLetter().ToString();

		GameObject bubbleObject = Instantiate( Resources.Load( "Prefabs/Bubble" ) ) as GameObject;
		bubbleObject.transform.parent = transform;
		bubbleObject.transform.position = m_trigger.transform.position;
		m_text = bubbleObject.GetComponent<TextMesh>();

		m_text.gameObject.SetActive( false );

		m_text.text = m_key;
	}

	protected override void Update() 
	{
		if( m_bDone ) { return; }

		if( PanelManager.Instance.CurrentPanelIndex != m_index ) { return; }

		m_distance = Vector2.Distance( PlayerController.Instance.transform.position, m_trigger.transform.position );

		m_bCheckForInput = m_distance < 1.5f;

		m_text.gameObject.SetActive( m_bCheckForInput );

		if( m_bCheckForInput )
		{
			if( Input.GetKeyDown( (KeyCode)Enum.Parse( typeof( KeyCode ), m_key ) ) )
			{
				PanelManager.Instance.RequestPanel();

				Debug.Log( "KEY DOWN" );

				m_text.gameObject.SetActive( false );

				MemoryController.Instance.ShowMemory( "" );

				Destroy( m_trigger.gameObject );

				m_bDone = true;

				PlayerController.Instance.Controllable = false;

				FadeController.Instance.FadeOut();

				Invoke( "PlayerDelayControllable", 4.0f );
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
