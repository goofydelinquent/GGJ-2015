using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ChimePlayer : MonoBehaviour {

	private static ChimePlayer m_instance = null;
	public static ChimePlayer Instance { get { return m_instance; } }

	public void PlaySound() {
		this.audio.Play();
	}

	void Awake () {
		m_instance = this;
	}

	void OnDestroy () {
		m_instance = null;
	}
}
