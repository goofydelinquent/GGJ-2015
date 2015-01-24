// Aspect Ratio = width / height.
// Camera ortho size is half of height.
// Unity camera is height based. Height stays and width changes depending on screen ratio.

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float m_orthoSize;
	[SerializeField] private float m_aspectRatioWidth;
	[SerializeField] private float m_aspectRatioHeight;

	private float m_width;
	private float m_height;

	public float Width 		{ get { return m_width; } }
	public float Height 	{ get { return m_height; } }

	protected virtual void Reset()
	{
		m_orthoSize = 3.84f;

		// 4:3
		m_aspectRatioWidth = 4.0f;
		m_aspectRatioHeight = 3.0f;
	}

	protected virtual void Awake()
	{
		camera.orthographic = true;
		camera.orthographicSize = m_orthoSize;

		float targetAspectRatio = m_aspectRatioWidth / m_aspectRatioHeight;

		float idealHeight = camera.orthographicSize * 2.0f;
		float idealWidth = targetAspectRatio * idealHeight;

		m_height = camera.orthographicSize * 2.0f;
		m_width = camera.aspect * m_height;
		
		if( camera.aspect < targetAspectRatio )
		{ // Current is more rectangle than ideal. Increase ortho size to fit width.

			float scaleFactor = idealWidth / m_width;
			camera.orthographicSize *= scaleFactor;
		}
		else
		{ // Current is more square than ideal. Decrease ortho size to fit width.

			float scaleFactor = m_width / idealWidth;
			camera.orthographicSize /= scaleFactor;
		}

		m_height = camera.orthographicSize * 2.0f;
		m_width = camera.aspect * m_height;

		float defaultPixelsToUnit = 100.0f;
		print( "Ideal Aspect Ratio: " + targetAspectRatio );
		print( "Current Aspect Ratio: " + camera.aspect );
		print( "Current Ortho Size: " + camera.orthographicSize );
		print( "Screen Resolution : ( " + Screen.width + ", " + Screen.height + " )" );
		print( "Game Resolution : ( " + m_width + ", " + m_height + " )" );
		print( "Canvas Resolution : ( " + m_width * defaultPixelsToUnit + ", " + m_height * defaultPixelsToUnit + " )" );
	}
}
