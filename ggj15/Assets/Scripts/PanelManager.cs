using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{

	private static PanelManager m_instance;
	public static PanelManager Instance { get { return m_instance; } }

	[SerializeField] private GameObject[] m_triggerPanels = null;
	[SerializeField] private GameObject[] m_fillerPanels = null;
	[SerializeField] private GameObject m_requiredPanel = null;

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

	private bool m_bDone = false;


	private void Awake()
	{
		m_instance = this;

		m_list = new LinkedList<Panel>();

		RequestPanel( false );
		RequestPanel( false );
		RequestRequiredPanel();
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );

		RequestPanel();
		RequestPanel( false );

		RequestPanel();
		RequestPanel( false );
		RequestPanel( false );
		RequestPanel();
		RequestPanel( false );

	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	public void AddSequence() {
		int sequenceType = Random.Range( 0, 5 );
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
			case 2: {
				RequestPanel( false );
				RequestPanel( false );
				RequestPanel( true );
				break;
			}
			case 3: {
				RequestPanel( false );
				RequestPanel( true );
				break;
			}
			case 4: default: {
				RequestPanel( true );
				RequestPanel( false );
				break;
			}
		}

	}

	public void RequestPanel( bool p_bWithTrigger = true )
	{
		if( m_list.Count > 15 ){
			return;
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

		m_list.AddLast( panel );

		m_totalPanelCounter++;

		//Debug.Log( "List count: " + m_list.Count + " Panel Counter: " + m_totalPanelCounter );

		if( m_bTryToEnd )
		{
			m_bTryToEnd = false;

			m_bDone = false;

			Debug.Log( "CANCEL END" );
		}
	}

	public void RequestRequiredPanel()
	{
		if( m_list.Count > 15 ){
			return;
		}
		
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
			
			m_bDone = false;
			
			Debug.Log( "CANCEL END" );
		}
	}
	
	public void IncrementCurrentIndex()
	{
		m_currentPanelIndex++;

		Debug.Log( "Incrementing Current Index. Now at: " + m_currentPanelIndex );

		// Current Node set.
		if( m_node == null ) {
			m_node = m_list.First.Next;
		}
		else {
			m_node = m_node.Next;
		}

		if( m_currentPanelIndex > 1 )
		{
			LinkedListNode<Panel> node = m_list.First;
			m_list.RemoveFirst();
			Destroy( node.Value.gameObject );
			Debug.Log( "DESTROY" );
		}

		Debug.Log("COUNT A: " + m_list.Count);

		//Debug.Log( "Current panel index: " + m_currentPanelIndex );

		if( m_bDone )
		{
			return;
		}

		// End Check.
		if( m_bTryToEnd )
		{
			if( CheckForNode( m_node, 1 ) ) {
				m_bDone = true;
				Debug.Log( "DONE" );
			}
		}
		else
		{
			if( m_list.Count <= 4 ) {
				m_bTryToEnd = true;
				Debug.Log( "TRY TO END" );
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
