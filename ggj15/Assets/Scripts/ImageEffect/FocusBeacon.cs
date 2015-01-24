using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FocusBeacon : MonoBehaviour {
	
	public float m_radius = 1f;
	public static List<FocusBeacon> S_BEACONS = new List<FocusBeacon>();

	// Use this for initialization
	void Start () {

		FocusBeacon b = this.GetComponent<FocusBeacon>();
		if ( b == null ) return;
		S_BEACONS.Add( b );

	}

	void OnDestroy () {

		FocusBeacon b = this.GetComponent<FocusBeacon>();
		if ( b == null ) return;
		S_BEACONS.Remove( b );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
