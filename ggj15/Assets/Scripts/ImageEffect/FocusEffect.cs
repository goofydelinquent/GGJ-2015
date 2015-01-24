using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
public class FocusEffect : ImageEffectBase {
	public RenderTexture rt;
	public Texture  m_textureFocus;
	private float    m_focusFactor;

	public Texture m_baseTexture;

	private int m_screenWidth;
	private int m_screenHeight;

	public Transform m_player;
	public float m_threshold = 50f;

	void Start () {

		m_screenWidth = Screen.width;
		m_screenHeight = Screen.height;

		rt = new RenderTexture( m_screenWidth, m_screenHeight, 32 );
		Graphics.Blit( m_baseTexture, rt );
	}
	
	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {

		if ( m_screenWidth != Screen.width || m_screenHeight != Screen.height ) {
			m_screenWidth = Screen.width;
			m_screenHeight = Screen.height;

			rt = new RenderTexture( m_screenWidth, m_screenHeight, 32 );
		}

		Graphics.Blit( m_baseTexture, rt );

		material.SetTexture("_FocusTex", m_textureFocus);


		RenderTexture.active = rt;
		GL.PushMatrix();
		GL.LoadPixelMatrix( 0, m_screenWidth, m_screenHeight, 0 );

		float scaleFactor = 768f / m_screenHeight;
		float minDistance = float.PositiveInfinity;

		foreach ( FocusBeacon b in FocusBeacon.S_BEACONS ) {

			float distance = Mathf.Abs( (m_player.transform.position - b.transform.position).magnitude );
			minDistance = Mathf.Min( minDistance, distance );

			Vector3 position = Camera.main.WorldToScreenPoint( b.transform.position );
			float size = ( b.m_radius * scaleFactor ) / 2f;

			Graphics.DrawTexture( new Rect( position.x - (size / 2f), m_screenHeight - position.y - (size / 2f),
			                               size, size), m_textureFocus );

		}

		GL.PopMatrix();

		if ( minDistance > m_threshold ) {
			Graphics.Blit( source, destination );
			return;
		}

		material.SetFloat("_FocusFactor", ( minDistance / m_threshold ) );


		RenderTexture.active = null;
		//Graphics.Blit( rt, destination );

		material.SetTexture("_FocusTex", rt );
		Graphics.Blit( source, destination, material );
	}
}