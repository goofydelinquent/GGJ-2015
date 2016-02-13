using UnityEngine;
using System.Collections;

public class MemoryJitter : MonoBehaviour {

	public Vector2 m_positionRange = Vector2.zero;

	public float m_interval;
	private float m_currentInterval;
	public Vector2 m_intervalRange = new Vector2( 0.3f, 1f );

	private Vector3 m_originalPosition = Vector3.zero;

	void OnEnable () {
		m_originalPosition = this.transform.localPosition;
	}

	void OnDisable () {
		this.transform.localPosition = m_originalPosition;
		Color c = this.GetComponent<Renderer>().material.color;
		c.a = 1f;
		this.GetComponent<Renderer>().material.color = c;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Color c = this.GetComponent<Renderer>().material.color;

		if ( this.GetComponent<Renderer>().material.color.a < 1f ) {
			if ( Random.Range( 0, 100 ) < 40 ) {
				c.a = 1f;
				this.GetComponent<Renderer>().material.color = c;
			}
		}

		m_currentInterval += Time.deltaTime;
		if ( m_currentInterval < m_interval ) { return; }

		m_currentInterval = 0f;
		m_interval = Random.Range( m_intervalRange.x, m_intervalRange.y );

		this.transform.localPosition = m_originalPosition 
			+ new Vector3(  Random.Range( -m_positionRange.x, m_positionRange.x ), 
			              Random.Range( -m_positionRange.y, m_positionRange.y ),
			              0 );

		//if ( Random.Range( 0, 100 ) < 50 ) {
			c.a = 0.7f;
			this.GetComponent<Renderer>().material.color = c;
		//}


	}
}
