﻿using UnityEngine;
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
		if( m_trigger == null ) { return; }

		m_key = GetLetter().ToString();

		GameObject bubbleObject = Instantiate( Resources.Load( "Prefabs/Bubble" ) ) as GameObject;
		bubbleObject.transform.parent = transform;

		Beacon beacon = m_trigger.GetComponent<Beacon>();

		if( beacon != null ) {
			bubbleObject.transform.position = beacon.GetPosition() + new Vector3( 1, -1, 0 );
		}

		m_text = bubbleObject.GetComponent<TextMesh>();

		m_text.gameObject.SetActive( false );

		m_text.text = m_key;
	}

	protected override void Update() 
	{
		if( m_trigger == null ) { return; }

		if( m_bDone ) { return; }

		if( PanelManager.Instance.CurrentPanelIndex != m_index ) { return; }

		//m_distance = Vector2.Distance( PlayerController.Instance.transform.position, m_trigger.transform.position );

		m_bCheckForInput = Mathf.Abs(PlayerController.Instance.transform.position.x - m_trigger.transform.position.x) < 1.5f;

		m_text.gameObject.SetActive( m_bCheckForInput );

		if( m_bCheckForInput )
		{
			if( Input.GetKeyDown( (KeyCode)Enum.Parse( typeof( KeyCode ), m_key ) ) )
			{
				PanelManager.Instance.RequestPanel();
				PanelManager.Instance.RequestPanel( false );

				Debug.Log( "KEY DOWN" );

				m_text.gameObject.SetActive( false );

				Sprite memory = m_trigger.transform.GetComponent<Beacon>().Memory;

				MemoryController.Instance.ShowMemory( memory );

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
