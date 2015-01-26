using UnityEngine;
using System.Collections;

public class Quote : MonoBehaviour
{	
	private TextMesh m_text;

	private Color m_color;

	public void Awake()
	{
		m_text = GetComponent<TextMesh>();

		m_text.text = "";

		m_color = renderer.material.color;

		TweenFade( 0.0f );

		iTween.Init( gameObject );
	}

	public void RandomizeText()
	{
		m_text.text = RandomTextPool.GetRandomText();

		iTween.ValueTo( gameObject, iTween.Hash(
			"from", 0f,
			"to", 1f,
			"time", 1.0f,
			"onupdatetarget", gameObject,
			"onupdate", "TweenFade",
			"easetype", iTween.EaseType.easeOutCubic
			));
	}

	public void TweenFade( float p_value )
	{
		renderer.material.color = new Color( m_color.r, m_color.g, m_color.b, p_value );
	}
}
