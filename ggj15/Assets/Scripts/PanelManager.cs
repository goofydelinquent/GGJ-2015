﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{

	private static PanelManager m_instance;
	public static PanelManager Instance { get { return m_instance; } }

	[SerializeField] private GameObject[] m_triggerPanels = null;
	[SerializeField] private GameObject[] m_fillerPanels = null;
	[SerializeField] private GameObject m_requiredPanel = null;
	[SerializeField] private GameObject m_endPanel = null;

	private LinkedList<Panel> m_list = null;
	private LinkedListNode<Panel> m_node = null;

	// Total count of panels from start of game.
	private int m_totalPanelCounter = 0;
	public int TotalPanelCounter { get { return m_totalPanelCounter; } }

	// Index of panel where the player is currently in. Triggered on Filmstrip entry.
	private int m_currentPanelIndex = 0;
	public int CurrentPanelIndex { get { return m_currentPanelIndex; } }
	
	private Vector2 m_panelSize = new Vector3( 10.24f, 7.68f );
	public Vector2 PanelSize { get { return m_panelSize; } }

	private bool m_bTryToEnd = false;

	private bool m_bHasEnding = false;

	private bool m_bEndGame = false;

	GameObject m_endPanelObject = null;
	public GameObject EndPanelObject { get { return m_endPanelObject; } }

	private int m_lastFillers = 0;

	private void Awake()
	{
		m_instance = this;

		m_list = new LinkedList<Panel>();

		RequestPanel( false );
		RequestRequiredPanel();
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );

	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	public void AddSequence() {

		int sequenceType = Random.Range( 0, 3 );
		switch( sequenceType ) {
			case 0: {
				RequestPanel( true );
				RequestPanel( false );
				RequestPanel( false );
				break;
			}
			case 1: {
				RequestPanel( false );
				RequestPanel( true );
				RequestPanel( false );
				break;
			}
			case 2: default: {
				RequestPanel( true );
				RequestPanel( false );
				break;
			}
		}

	}

	public void RequestPanel( bool p_bWithTrigger = true )
	{
		if( m_list.Count > 11 ){

			if( p_bWithTrigger ) {
				return;
			}

		}

		if ( !p_bWithTrigger ) {
			m_lastFillers ++;
			if ( m_lastFillers >= 2 ) {
				return;
			}
		} else {
			m_lastFillers = 0;
		}


		// Instantiate specific panel here.
		GameObject[] panels = p_bWithTrigger ? m_triggerPanels : m_fillerPanels;
		GameObject panelObject = Instantiate( panels[Random.Range( 0, panels.Length )] ) as GameObject;
		panelObject.transform.parent = transform;
		panelObject.transform.position = new Vector3( m_panelSize.x * m_totalPanelCounter, 0, 0 );

		GameObject stripObject = Instantiate( Resources.Load( "Prefabs/Filmstrip" ) ) as GameObject;
		stripObject.transform.parent = panelObject.transform;
		stripObject.transform.position = new Vector3( m_panelSize.x * ( m_totalPanelCounter + 1 ), m_panelSize.y * 0.5f, 0 );

		Panel panel = panelObject.GetComponent<Panel>();
		panel.Index = m_totalPanelCounter;

		if( !p_bWithTrigger && m_totalPanelCounter > 2 ) {
			GameObject quoteObject = Instantiate( Resources.Load( "Prefabs/Quote" ) ) as GameObject;
			quoteObject.transform.parent = panelObject.transform;
			quoteObject.transform.position = new Vector3( m_panelSize.x * ( m_totalPanelCounter + 0.5f ), m_panelSize.y * 0.8f, 0 );

			MemoryPanel memoryPanel = panel as MemoryPanel;
			if( memoryPanel != null )
			{
				memoryPanel.Quote = quoteObject.GetComponent<Quote>();
			}
		}

		m_list.AddLast( panel );

		m_totalPanelCounter++;

		//Debug.Log( "List count: " + m_list.Count + " Panel Counter: " + m_totalPanelCounter );

		if( m_bTryToEnd )
		{
			m_bTryToEnd = false;

			m_bHasEnding = false;

			DestroyEndingPanel();

			Debug.Log( "CANCEL END" );
		}
	}

	public void RequestRequiredPanel()
	{
		// Instantiate specific panel here.
		GameObject panelObject = Instantiate( m_requiredPanel ) as GameObject;
		panelObject.transform.parent = transform;
		panelObject.transform.position = new Vector3( m_panelSize.x * m_totalPanelCounter, 0, 0 );
		
		GameObject stripObject = Instantiate( Resources.Load( "Prefabs/Filmstrip" ) ) as GameObject;
		stripObject.transform.parent = panelObject.transform;
		stripObject.transform.position = new Vector3( m_panelSize.x * ( m_totalPanelCounter + 1 ), m_panelSize.y * 0.5f, 0 );
		
		Panel panel = panelObject.GetComponent<Panel>();
		panel.Index = m_totalPanelCounter;
		
		m_list.AddLast( panel );
		
		m_totalPanelCounter++;
		
		//Debug.Log( "List count: " + m_list.Count + " Panel Counter: " + m_totalPanelCounter );
		
		
		if( m_bTryToEnd )
		{
			m_bTryToEnd = false;
			
			m_bHasEnding = false;
			
			DestroyEndingPanel();
			
			Debug.Log( "CANCEL END" );
		}
	}

	private void AddEndingPanel()
	{
		m_endPanelObject = Instantiate( m_endPanel ) as GameObject;
		m_endPanelObject.transform.parent = transform;
		m_endPanelObject.transform.position = new Vector3( m_panelSize.x * m_totalPanelCounter, 0, 0 );
	}

	private void DestroyEndingPanel()
	{
		if( m_endPanelObject != null )
		{
			Destroy( m_endPanelObject );
		}
	}

	private int endCounter = 1;	
	
	public void IncrementCurrentIndex()
	{
		m_currentPanelIndex++;

		//Debug.Log( "Incrementing Current Index. Now at: " + m_currentPanelIndex );

		// Current Node set.
		if( m_node == null ) {
			m_node = m_list.First.Next;
		}
		else {
			m_node = m_node.Next;
		}

		//PASTA CODE!
		if ( m_node != null && m_node.Previous != null && m_node.Previous.Value != null) {
			MemoryPanel mPanel = m_node.Previous.Value as MemoryPanel;
			if ( mPanel != null ) {
				if ( mPanel.HasTrigger() ){
					if ( mPanel.HasTriggerActivated() ) {
						RandomTextPool.AddTriggeredMemory();
					} else {
						RandomTextPool.AddMissedTrigger();
					}
				}
			}
		}

		if( m_node != null){
			if( m_node.Value != null){
				MemoryPanel memoryPanel = m_node.Value as MemoryPanel;
				if( memoryPanel != null )
				{
					if( memoryPanel.Quote != null ) {
						memoryPanel.Quote.RandomizeText();
					}
				}
			}
		}

		if( m_currentPanelIndex > 1 )
		{
			LinkedListNode<Panel> node = m_list.First;
			m_list.RemoveFirst();
			Destroy( node.Value.gameObject );
			//Debug.Log( "DESTROY" );
		}

		//Debug.Log("COUNT A: " + m_list.Count);

		//Debug.Log( "Current panel index: " + m_currentPanelIndex );

		if( m_bHasEnding )
		{
			// Disable Control of Player on next entry of filmstrip.

			if( endCounter == 0 ){
				if( !m_bEndGame ) {

					BgmManager.Instance.SetNextTrack( BgmManager.MusicType.Ending, true, true );

					PlayerController.Instance.PlayEndCutscene();
					m_bEndGame = true;
				}
			}
			endCounter--;

			return;
		}

		// End Check.
		if( m_bTryToEnd )
		{
			if( CheckForNode( m_node, 1 ) ) {
				m_bHasEnding = true;
				AddEndingPanel();

				Debug.Log( "DONE" );

			}
		}
		else
		{
			if( m_list.Count <= 4 ) {
				m_bTryToEnd = true;
				//.Log( "TRY TO END" );
			}
		}
	}

	private bool CheckForNode( LinkedListNode<Panel> p_startNode, int p_count )
	{
		if ( p_startNode == null ) { return false; }

		LinkedListNode<Panel> node = p_startNode;

		for( int i = 0; i < p_count; i++ ){
			node = node.Next;

			if( node == null ) {
				return false;
			}
		}

		return true;
	}
}
