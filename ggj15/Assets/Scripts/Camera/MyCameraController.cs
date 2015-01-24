using UnityEngine;
using System.Collections;

public class MyCameraController : CameraController
{
	private const int FRAME_RATE_TARGET		= 60;
	private Vector3 m_offset = Vector3.zero;

	private static MyCameraController m_instance = null;
	public static MyCameraController Instance { get { return m_instance; } }

	protected override void Awake()
	{
		base.Awake();

		m_instance = this;

		Application.targetFrameRate = FRAME_RATE_TARGET;
	}

	private void Start()
	{
		m_offset.x = 2;
		m_offset.y = PanelManager.Instance.PanelSize.y * 0.5f;
		m_offset.z = -10.0f;

		Vector3 targetPosition = PlayerController.Instance.transform.position;
		targetPosition.x += m_offset.x;
		targetPosition.y = m_offset.y;
		targetPosition.z = m_offset.z;
		transform.position = targetPosition;
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	private void LateUpdate()
	{
		Vector3 velocity = Vector3.zero;
		Vector3 targetPosition = PlayerController.Instance.transform.position;
		targetPosition.x += m_offset.x;
		targetPosition.y = m_offset.y;
		targetPosition.z = m_offset.z;
		transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref velocity, 0.08f );
	}
}
