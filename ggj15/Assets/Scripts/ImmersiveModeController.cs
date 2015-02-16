using UnityEngine;
using System.Collections;

public class ImmersiveModeController : MonoBehaviour
{
	#if UNITY_ANDROID && !UNITY_EDITOR
	const int SYSTEM_UI_FLAG_IMMERSIVE = 2048;
	const int SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 4096;
	const int SYSTEM_UI_FLAG_HIDE_NAVIGATION = 2;
	const int SYSTEM_UI_FLAG_FULLSCREEN = 4;
	const int SYSTEM_UI_FLAG_LAYOUT_STABLE = 256;
	const int SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION = 512;
	const int SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN = 1024;

	private int m_argument;

	private ImmersiveModeController m_instance;

	public void Start()
	{
		if( m_instance != null )
		{
			if( m_instance != this )
			{
				Destroy( gameObject );
				return;
			}
		}

		m_instance = this;

		TurnImmersiveModeOn();

		DontDestroyOnLoad( gameObject );
	}

	void OnApplicationFocus(bool focusStatus) {
		TurnImmersiveModeOn();
	}
	
	void ImmersiveModeCall() {
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			RunOnUiThread();
		}));
	}

	void RunOnUiThread() {
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject window = activity.Call<AndroidJavaObject>("getWindow");
		AndroidJavaObject decorView = window.Call<AndroidJavaObject>("getDecorView");
		decorView.Call("setSystemUiVisibility", m_argument );
		decorView.Dispose();
	}


	public void TurnImmersiveModeOn()
	{
		m_argument = SYSTEM_UI_FLAG_FULLSCREEN | SYSTEM_UI_FLAG_HIDE_NAVIGATION | SYSTEM_UI_FLAG_IMMERSIVE_STICKY;
		ImmersiveModeCall();
	}
	
	void TurnImmersiveModeOff()
	{
		m_argument = SYSTEM_UI_FLAG_LAYOUT_STABLE | SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION | SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN;
		ImmersiveModeCall();
	}

	#endif
}