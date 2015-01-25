using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{
	private static FadeController m_instance;
	public static FadeController Instance { get { return m_instance; } }

	[SerializeField] private Texture2D m_texture;

	private void Awake()
	{
		m_instance = this;

		iTween.CameraFadeAdd( m_texture );
	}
	
	private void OnDestroy()
	{
		m_instance = null;
	}

	/*
	private void OnGUI()
	{
		if( GUI.Button( new Rect( 100, 100, 100, 100 ), "FADE" ) )
		{
			FadeOut();
		}
	}*/


	public void FadeOut(float p_time = 0.35f)
	{
		iTween.CameraFadeFrom( iTween.Hash(
			"amount", 1,
			"time", p_time,
			"easeType", iTween.EaseType.easeOutCubic ) );

	}
}
