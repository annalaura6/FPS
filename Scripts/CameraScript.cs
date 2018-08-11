using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {

	public static bool initiation = false;

	void Start()
	{
		if (!initiation)
		{
			SceneManager.LoadScene(0);
		}
	}
}
