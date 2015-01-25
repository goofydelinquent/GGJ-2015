using UnityEngine;
using System.Collections;

public class SceneRestarter : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		if ( Input.GetKey( KeyCode.Backspace ) || Input.GetKey( KeyCode.Backslash ) ) {
			Application.LoadLevel( 0 );
		}
	
	}
}
