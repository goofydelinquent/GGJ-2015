using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{

	private static PanelManager m_instance;
	public static PanelManager Instance { get { return m_instance; } }

	private Queue<GameObject> m_queue;

	private int m_panelCounter = 0;

	private Vector2 m_offset = Vector3.right * 10.24f;

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
		panelObject.transform.position = m_offset * m_panelCounter;

		m_queue.Enqueue( panelObject );

		if( m_panelCounter > 1 )
		{
			Destroy( m_queue.Dequeue() );
		}

		m_panelCounter++;

		Debug.Log( "Queue count: " + m_queue.Count + " Panel Counter: " + m_panelCounter );
	}
}
