using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{

	private static PanelManager m_instance;
	public static PanelManager Instance { get { return m_instance; } }

	private Queue<GameObject> m_queue;

	private int m_totalPanelCounter = 0;
	private int m_currentPanelIndex = 0;

	private Vector2 m_panelSize = new Vector3( 10.24f, 7.68f );
	public Vector2 PanelSize { get { return m_panelSize; } }

	private void Awake()
	{
		m_instance = this;

		m_queue = new Queue<GameObject>();

		RequestPanel();
		RequestPanel();
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	private void OnGUI()
	{
		if( GUI.Button( new Rect( 20, 20, 200, 200 ), "ADD" ) )
		{
			RequestPanel();
		}
	}

	private void RequestPanel()
	{
		// Instantiate specific panel here.
		GameObject panelObject = Instantiate( Resources.Load( "Prefabs/Panels/panel" ) ) as GameObject;
		panelObject.transform.parent = transform;
		panelObject.transform.position = new Vector3( m_panelSize.x * m_totalPanelCounter, 0, 0 );

		GameObject stripObject = Instantiate( Resources.Load( "Prefabs/filmstrip" ) ) as GameObject;
		stripObject.transform.parent = panelObject.transform;
		stripObject.transform.position = new Vector3( m_panelSize.x * ( m_totalPanelCounter + 1 ), m_panelSize.y * 0.5f, 0 );

		m_queue.Enqueue( panelObject );

		if( m_totalPanelCounter > 2 )
		{
			Destroy( m_queue.Dequeue() );
		}

		m_totalPanelCounter++;

		Debug.Log( "Queue count: " + m_queue.Count + " Panel Counter: " + m_totalPanelCounter );
	}

	public void IncrementCurrentIndex()
	{
		m_currentPanelIndex++;

		Debug.Log( "Current panel index: " + m_currentPanelIndex );
	}
}
