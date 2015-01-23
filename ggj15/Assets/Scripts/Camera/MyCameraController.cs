using UnityEngine;
using System.Collections;

public class MyCameraController : CameraController
{
	private const int FRAME_RATE_TARGET		= 60;
	private Vector3 CAMERA_OFFSET	 		= new Vector3( 0, 0, -10.0f );

	private static MyCameraController m_instance = null;
	public static MyCameraController Instance { get { return m_instance; } }

	[SerializeField]private GameObject m_testObject;

	protected override void Awake()
	{
		base.Awake();

		m_instance = this;

		Application.targetFrameRate = FRAME_RATE_TARGET;
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	private void LateUpdate()
	{
		Vector3 velocity = Vector3.zero;
		Vector3 targetPosition = m_testObject.transform.position + CAMERA_OFFSET;
		transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref velocity, 0.08f );
	}
}
