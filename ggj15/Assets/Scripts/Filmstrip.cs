using UnityEngine;
using System.Collections;

public class Filmstrip : MonoBehaviour
{
	private bool m_bCheckForPlayer = true;

	void Update()
	{
		if( !m_bCheckForPlayer ) { return; }

		if( PlayerController.Instance.transform.position.x > transform.position.x )
		{
			m_bCheckForPlayer = false;

			PanelManager.Instance.IncrementCurrentIndex();
		}
	}

	void OnBecameVisible()
	{
		//m_bCheckForPlayer = true;
	}
}
