using UnityEngine;
using System.Collections;

public class Quote : MonoBehaviour
{	
	private TextMesh m_text;

	public void Awake()
	{
		m_text = GetComponent<TextMesh>();

		m_text.text = "";

		iTween.Init( gameObject );
	}

	public void RandomizeText()
	{
		m_text.text = RandomTextPool.GetRandomText();

		//iTween.FadeTo( gameObject, 0, 0 );
		//iTween.FadeTo( gameObject, iTween.Hash( "alpha", 1, "time", 0.35f, "easeType", iTween.EaseType.easeInOutQuad ) );
	}


}
