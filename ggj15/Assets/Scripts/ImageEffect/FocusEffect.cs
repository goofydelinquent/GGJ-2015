﻿using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
public class FocusEffect : ImageEffectBase {
	private RenderTexture rt;
	public List<Texture> m_textureFocuses = new List<Texture>();
	public float m_interval = 0.09f;

	//public 	Texture  	m_textureFocus;
	private float    	m_focusFactor;
	public 	float     	m_greyscaleRamp = 0f;

	public 	Texture 	m_baseTexture;

	private int 		m_screenWidth;
	private int			m_screenHeight;

	public 	Transform 	m_player;
	public 	float 		m_threshold = 50f;
	private float		m_plateau = 0.5f;
	
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

		material.SetTexture("_FocusTex", m_textureFocuses[ 0 ]);
		material.SetFloat("_GreyscaleRamp", m_greyscaleRamp );

		RenderTexture.active = rt;
		GL.PushMatrix();
		GL.LoadPixelMatrix( 0, m_screenWidth, m_screenHeight, 0 );

		float scaleFactor = 1024f / m_screenWidth;
		float minDistance = m_threshold;

		FocusBeacon closest = null;

		foreach ( FocusBeacon b in FocusBeacon.S_BEACONS ) {

			float distance = Mathf.Abs( m_player.transform.position.x - b.transform.position.x );
			if ( distance <= minDistance ) {
				closest = b;
				minDistance = distance;
			}
		}

		if ( minDistance > m_threshold ) {
			Graphics.Blit( source, destination );
			return;
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

			int index =  Mathf.FloorToInt( ( Time.timeSinceLevelLoad / m_interval ) % m_textureFocuses.Count );

			Graphics.DrawTexture( new Rect( position.x - (size / 2f ), yPosition  - (size / 2 ),
			                               size, size), m_textureFocuses[ index ] );

		}

		GL.PopMatrix();




		material.SetFloat("_FocusFactor", Mathf.Sin( ( minDistance / m_threshold ) * Mathf.PI * 0.5f )  );


		RenderTexture.active = null;
		//Graphics.Blit( rt, destination );

		material.SetTexture("_FocusTex", rt );
		Graphics.Blit( source, destination, material );
	}
}