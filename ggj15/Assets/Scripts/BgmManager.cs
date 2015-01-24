using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BgmManager : MonoBehaviour {

	public enum MusicType {
		Intro = 0,
		Body = 1,
		Ending = 2
	}

	private int m_bufferCount = 2;

	public List<AudioClip> m_clips = new List<AudioClip>();
	
	private List<AudioSource> m_trackBuffers = new List<AudioSource>();


	private int m_currentClipIndex = -1;
	private int m_currentBufferIndex = -1;
	
	private int m_nextBufferIndex = -1;
	private int m_nextClipIndex = -1;
	private float m_timeToNext = -1f;

	private float m_fadeTime = 2f;
	private bool m_bIsFading = false;
	
	void Awake () {
		
		for( int i = 0; i < m_bufferCount; i++ ) {
			
			GameObject current = new GameObject( "Buffer " + i );
			current.transform.parent = this.transform;
			AudioSource src = current.AddComponent<AudioSource>();
			m_trackBuffers.Add( src );
			
		}
		
		if ( m_clips.Count > 0 ) {
			
			m_currentClipIndex = 0;
			
		}
		
		if ( m_trackBuffers.Count > 0 ) {
			
			m_currentBufferIndex = 0;
			
		}
		
		if ( m_currentBufferIndex >= 0 &&  m_currentClipIndex >= 0 ) {
			
			m_trackBuffers[ m_currentBufferIndex ].clip = m_clips[ m_currentClipIndex ];
			
		} else {
			
			// :(
			
		}
		
	}
	
	// Use this for initialization
	void Start () {
		
		if ( m_currentBufferIndex >= 0 ) {
			
			m_trackBuffers[ m_currentBufferIndex ].Play();
			SetNextTrack( MusicType.Intro );
			
		}

		SetNextTrack(MusicType.Body, false );
		/* DBG_NEXTRACK - Used for testing
		SetNextTrack( 0 );
		//*/
		
		//transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
		//transform.position = transform.parent.transform.position;
		
	}
	
	public float SetNextTrack( MusicType p_next, bool p_willLoop = true, bool p_forceToFade = false ) {

		int iType = (int)p_next;
		if ( iType < 0 || iType >= m_clips.Count ) {
			
			//out of bounds
			return -1f;
			
		}
		
		float timeLeft = 0f;
		m_nextClipIndex = iType;
		
		//Check if something is playing, then stop loop
		if ( m_currentBufferIndex >= 0 ) {

			AudioSource current = m_trackBuffers[ m_currentBufferIndex ];
			current.loop = false;

			if ( ! p_forceToFade ) {
				timeLeft = current.audio.clip.length - current.time; 
			} else {
				timeLeft = m_fadeTime;
				m_bIsFading = true;
			}
		}
		
		m_nextBufferIndex = ( m_currentBufferIndex + 1 ) % m_bufferCount;
		
		AudioSource next = m_trackBuffers[ m_nextBufferIndex ];
		next.clip = m_clips[ m_nextClipIndex ];
		next.loop = p_willLoop;
		next.PlayDelayed( timeLeft );
		next.volume = 1.0f;
		m_timeToNext = timeLeft;
		
		return timeLeft;
		
	}

	public int GetCurrentTrackIndex () {
		
		return m_currentClipIndex;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( m_timeToNext > 0f ) {

			m_timeToNext -= Time.deltaTime;

			if ( m_bIsFading ) {
				AudioSource current = m_trackBuffers[ m_currentBufferIndex ];
				current.volume = m_timeToNext / m_fadeTime;
			}

			if ( m_timeToNext < 0f ) {
				
				m_currentBufferIndex = m_nextBufferIndex;
				m_currentClipIndex = m_nextClipIndex;
				
				m_timeToNext = -1f;
				m_nextBufferIndex = -1;
				m_nextClipIndex = -1;

			}
		}
	}
	
}