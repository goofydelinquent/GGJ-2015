using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour {

	private AsyncOperation m_async;
	[SerializeField] private Image m_bar;

	private void Start()
	{
		StartCoroutine( LoadLevel( 1 ) );
	}
	
	private IEnumerator LoadLevel( int p_pevel )
	{
		m_async = Application.LoadLevelAsync( p_pevel );
		//m_async.allowSceneActivation = false;
		yield return m_async;
		m_async.allowSceneActivation = true;
	}
	
	void Update()
	{
		if( m_async == null ) { return; }

		if( m_bar != null ) {
			m_bar.rectTransform.sizeDelta = new Vector2( 300 * m_async.progress, 10 );
		}

		if( m_async.isDone ) {
			m_async.allowSceneActivation = true;
		}
	}
}
