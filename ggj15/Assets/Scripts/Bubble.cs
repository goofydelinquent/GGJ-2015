using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{	
	[SerializeField] private TextMesh m_text;
	[SerializeField] private SpriteRenderer m_spriteBg;
	[SerializeField] private SpriteRenderer m_spriteCircle;

	private bool m_bActive = false;

	public void Awake()
	{
		TweenFade( 0.0f );

		m_spriteBg.transform.Rotate( Vector3.back, Random.value * 360.0f );
		m_spriteCircle.transform.Rotate( Vector3.back, Random.value * 360.0f );

		#if UNITY_ANDROID
			m_text.gameObject.SetActive( false );
		#else
		m_spriteCircle.gameObject.SetActive( false );
		#endif

		iTween.Init( gameObject );
	}

	private void Update()
	{
		m_spriteBg.transform.Rotate( Vector3.back, 2 * Time.deltaTime );
		m_spriteCircle.transform.Rotate( Vector3.back, 10 * Time.deltaTime );
	}

	public void Fade( bool p_bIn )
	{
		if( p_bIn == m_bActive ) { return; }
		m_bActive = p_bIn;

		if( m_bActive ) {
			iTween.ValueTo( gameObject, iTween.Hash(
				"from", 0f,
				"to", 1f,
				"time", 0.35f,
				"onupdatetarget", gameObject,
				"onupdate", "TweenFade",
				"easetype", iTween.EaseType.easeOutCubic
				));
		}
		else {
			iTween.ValueTo( gameObject, iTween.Hash(
				"from", 1f,
				"to", 0f,
				"time", 0.35f,
				"onupdatetarget", gameObject,
				"onupdate", "TweenFade",
				"easetype", iTween.EaseType.easeOutCubic
				));
		}
	}

	public void SetText( string p_text )
	{
		#if UNITY_ANDROID
		m_text.text = "";
		#else
		m_text.text = p_text;
		#endif
	}

	public void TweenFade( float p_value )
	{
		Color color = new Color( 1, 1, 1, p_value );
		m_text.GetComponent<Renderer>().material.color = color;
		m_spriteBg.color = color;
		m_spriteCircle.color = color;
	}
}
