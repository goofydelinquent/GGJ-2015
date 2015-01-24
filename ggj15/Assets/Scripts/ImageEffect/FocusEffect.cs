using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
public class FocusEffect : ImageEffectBase {
	private RenderTexture rt;
	public 	Texture  	m_textureFocus;
	private float    	m_focusFactor;
	public 	float     	m_greyscaleRamp = 0f;

	public 	Texture 	m_baseTexture;

	private int 		m_screenWidth;
	private int			m_screenHeight;

	public 	Transform 	m_player;
	public 	float 		m_threshold = 50f;
	private float		m_plateau = 1.3f;
	
	void Start () {

		m_screenWidth = Screen.width;
		m_screenHeight = Screen.height;

		rt = new RenderTexture( m_screenWidth, m_screenHeight, 32 );
		Graphics.Blit( m_baseTexture, rt );
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination) {

		if ( m_screenWidth != Screen.width || m_screenHeight != Screen.height ) {
			m_screenWidth = Screen.width;
			m_screenHeight = Screen.height;

			rt = new RenderTexture( m_screenWidth, m_screenHeight, 32 );
		}

		Graphics.Blit( m_baseTexture, rt );

		material.SetTexture("_FocusTex", m_textureFocus);
		material.SetFloat("_GreyscaleRamp", m_greyscaleRamp );

		RenderTexture.active = rt;
		GL.PushMatrix();
		GL.LoadPixelMatrix( 0, m_screenWidth, m_screenHeight, 0 );

		float scaleFactor = 1024f / m_screenWidth;
		float minDistance = float.PositiveInfinity;

		FocusBeacon closest = null;

		foreach ( FocusBeacon b in FocusBeacon.S_BEACONS ) {

			float distance = Mathf.Abs( m_player.transform.position.x - b.transform.position.x );
			if ( distance < minDistance ) {
				closest = b;
				minDistance = distance;
			}
		}

		if ( closest != null ) {

			FocusBeacon b = closest;

			Vector3 position = Camera.main.WorldToScreenPoint( b.transform.position );
			float size = b.m_radius * scaleFactor;

			float yPosition = position.y;
			if ( ! ( Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer ) ) {
				yPosition = m_screenHeight - yPosition;
			} else {
				yPosition = position.y;
			}
			Graphics.DrawTexture( new Rect( position.x - (size / 2f ), yPosition  - (size / 2 ),
			                               size, size), m_textureFocus );

		}

		GL.PopMatrix();

		if ( minDistance > m_threshold ) {
			Graphics.Blit( source, destination );
			return;
		}

		material.SetFloat("_FocusFactor", Mathf.Min ( ( minDistance * m_plateau ) / m_threshold, 1.0f ) );


		RenderTexture.active = null;
		//Graphics.Blit( rt, destination );

		material.SetTexture("_FocusTex", rt );
		Graphics.Blit( source, destination, material );
	}
}