using UnityEngine;
using System.Collections;

public class MemoryController : MonoBehaviour
{

	private static MemoryController m_instance;
	public static MemoryController Instance { get { return m_instance; } }

	private SpriteRenderer m_spriteRenderer;

	private void Awake()
	{
		m_instance = this;

		m_spriteRenderer = GetComponent<SpriteRenderer>();

		gameObject.SetActive( false );
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	public void ShowMemory( string p_image )
	{
		transform.position = new Vector3( PanelManager.Instance.PanelSize.x * PanelManager.Instance.CurrentPanelIndex, 0, 0 );
		gameObject.SetActive( true );
	}

	private void CB()
	{

	}
}
