using UnityEngine;
using System.Collections;

public class MyCameraController : CameraController
{
	private const int FRAME_RATE_TARGET		= 60;
	private Vector3 m_offset = Vector3.zero;
	private bool m_bIsFollowingPlayer = true;

	private static MyCameraController m_instance = null;
	public static MyCameraController Instance { get { return m_instance; } }

	private bool m_bFocusToEndPanel = false;

	private float m_dampValue = 0.08f;

	protected override void Awake()
	{
		base.Awake();

		m_instance = this;

		Application.targetFrameRate = FRAME_RATE_TARGET;
	}

	private void Start()
	{
		m_offset.x = 1;
		m_offset.y = PanelManager.Instance.PanelSize.y * 0.5f;
		m_offset.z = -10.0f;

		if ( ! m_bIsFollowingPlayer ) { return; }

		Vector3 targetPosition = GetTargetPosition();
		targetPosition.y = m_offset.y;
		targetPosition.z = m_offset.z;
		transform.position = targetPosition;
	}

	private void OnDestroy()
	{
		m_instance = null;
	}

	public void SetFollowPlayer( bool p_bWillFollowPlayer ) {
		m_bIsFollowingPlayer = p_bWillFollowPlayer;
	}

	private void LateUpdate()
	{
		if ( ! m_bIsFollowingPlayer ) { return; }

		Vector3 velocity = Vector3.zero;
		Vector3 targetPosition = GetTargetPosition();
		targetPosition.y = m_offset.y;
		targetPosition.z = m_offset.z;
		transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref velocity, m_dampValue );
	}

	public Vector3 GetTargetPosition()
	{
		if( m_bFocusToEndPanel ) {
			return PanelManager.Instance.EndPanelObject.transform.position + Vector3.right * PanelManager.Instance.PanelSize.x * 0.5f;
		}
		else {
			return PlayerController.Instance.transform.position + Vector3.right * m_offset.x;
		}
	}

	public void	FocusToEndPanel()
	{
		m_bFocusToEndPanel = true;

		m_dampValue = 0.15f;
	}
}
