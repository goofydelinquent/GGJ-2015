using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{	
	[SerializeField] private TextMesh m_text;
	[SerializeField] private SpriteRenderer m_spriteBg;
	
	private bool m_bActive = false;

	public void Awake()
	{
		//TweenFade( 0.0f );

		//iTween.Init( gameObject );
	}
	/*
	public void Fade( bool p_bIn )
	{
		if( p_bIn == m_bActive ) { return; }
		m_bActive = p_bIn;

		if( m_bActive ) {
			iTween.ValueTo( gameObject, iTween.Hash(
				"from", 0f,
				"to", 1f,
				"time", 1.0f,
				"onupdatetarget", gameObject,
				"onupdate", "TweenFade",
				"easetype", iTween.EaseType.easeOutCubic
				));
		}
		else {
			iTween.ValueTo( gameObject, iTween.Hash(
				"from", 1f,
				"to", 0f,
				"time", 1.0f,
				"onupdatetarget", gameObject,
				"onupdate", "TweenFade",
				"easetype", iTween.EaseType.easeOutCubic
				));
		}
	}*/

	public void TweenFade( float p_value )
	{
		Color color = new Color( 1, 1, 1, p_value );
		renderer.material.color = color;
		m_spriteBg.color = color;
	}
}
