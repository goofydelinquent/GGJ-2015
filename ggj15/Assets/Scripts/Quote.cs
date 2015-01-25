using UnityEngine;
using System.Collections;

public class Quote : MonoBehaviour
{	
	void Start()
	{
		GetComponent<TextMesh>().text = RandomTextPool.GetRandomText();;
		RandomTextPool.GetRandomText();
	}
}
