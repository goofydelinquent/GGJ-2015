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

		iTween.Init( gameObject );
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	public void ShowMemory( string p_image )
	{
		transform.position = new Vector3( PanelManager.Instance.PanelSize.x * PanelManager.Instance.CurrentPanelIndex, 0, 0 );
		gameObject.SetActive( true );
		iTween.FadeTo( gameObject, 1.0f, 0.0f );

		Invoke( "DelayedFadeOut", 3.65f );
	}

	private void DelayedFadeOut()
	{
		iTween.FadeTo( gameObject, iTween.Hash( "alpha", 0, "time", 0.35f, "easeType", iTween.EaseType.easeOutCubic ) );
		//iTween.FadeTo( gameObject, 0, 0.35f );
	}
}
