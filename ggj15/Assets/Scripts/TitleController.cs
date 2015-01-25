using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
	
	private bool m_bDidInput = false;

	public PlayerController m_playerController;
	public BgmManager m_bgmManager;
	public PanelManager m_panelManager;
	public MyCameraController m_camera;
	public GameObject m_background;
	public GameObject m_title;
	public GameObject m_cover;

	public ParticleSystem m_particle;

	private float m_curScrollTimeLeft = -1f;
	private float m_timeToScrollBg = 3f;

	private float m_timeToFadeIn = 4f;
	private float m_curFadeInTimeLeft = -1f;


	// Use this for initialization
	void Start () {

		FadeController.Instance.FadeOut (1.0f);

		m_playerController.enabled = false;

		//Vector3 panelPosition = m_panelManager.transform.position;
		//panelPosition.x = 12f;
		//m_panelManager.transform.position = panelPosition;
		m_panelManager.enabled = false;

		m_camera.SetFollowPlayer( false );

		Vector3 playerPosition = m_playerController.transform.position;
		playerPosition.x = -3f;
		m_playerController.transform.position = playerPosition;
	
	}
	
	// Update is called once per frame
	void Update () {

		if ( ! m_bDidInput ) {
			if( Input.anyKeyDown )
			{
				m_bDidInput = true;
				m_bgmManager.SetNextTrack( BgmManager.MusicType.Body, true );
				m_curScrollTimeLeft = m_timeToScrollBg;
				m_panelManager.enabled = true;
				m_particle.Stop();

				return;
			}
			return;
		}

		if ( m_curScrollTimeLeft > 0f ) {

			m_curScrollTimeLeft -= Time.deltaTime;
			if ( m_curScrollTimeLeft <= 0f ) {

				m_curFadeInTimeLeft = m_timeToFadeIn;
				m_playerController.enabled = true;
				m_playerController.ForceSetWalking( true );

			} else {
				m_background.transform.Translate( 0f, Time.deltaTime * 0.4f, 0f );
				Color c = m_title.renderer.material.color;
				c.a = m_curScrollTimeLeft / m_timeToScrollBg;
				m_title.renderer.material.color = c;
				m_background.renderer.material.color = c;

			}

		} else if ( m_curFadeInTimeLeft > 0f ) {

			m_curFadeInTimeLeft -= Time.deltaTime;
			if ( m_curFadeInTimeLeft <= 0f ) {
			} else {
				Color c = m_cover.renderer.material.color;
				c.a = m_curFadeInTimeLeft / m_timeToFadeIn;
				m_cover.renderer.material.color = c;

				if ( m_curFadeInTimeLeft < 2f ) {
					m_playerController.ForceSetWalking( false );
					m_camera.SetFollowPlayer( true );
				}

			}

		} else {

		}
	
	}
}
