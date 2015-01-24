using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
public class FocusEffect : ImageEffectBase {
	public RenderTexture rt;
	public Texture  m_textureFocus;
	public float    m_focusFactor;

	public Texture m_baseTexture;

	private int m_screenWidth;
	private int m_screenHeight;

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
		material.SetFloat("_FocusFactor", m_focusFactor);

		RenderTexture.active = rt;
		GL.PushMatrix();
		GL.LoadPixelMatrix( 0, m_screenWidth, m_screenHeight, 0 );

		float scaleFactor = 1f; //1024f / m_screenHeight;

		Graphics.DrawTexture( new Rect( 0, 0, scaleFactor * 250, scaleFactor * 250 ), m_textureFocus );

		GL.PopMatrix();
		RenderTexture.active = null;
		//Graphics.Blit( rt, destination );

		material.SetTexture("_FocusTex", rt );
		Graphics.Blit( source, destination, material );
	}
}