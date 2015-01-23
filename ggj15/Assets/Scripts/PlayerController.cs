using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private static PlayerController m_instance;
	public static PlayerController Instance { get { return m_instance; } }

	private void Awake()
	{
		m_instance = this;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			Debug.Log(">>>>>>");
			float x = transform.position.x + (2.0f * Time.deltaTime);
			transform.position = new Vector2( x, transform.position.y);
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
	}
}
