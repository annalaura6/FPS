using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	Network network;
	public InputField nickname;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		network = GetComponent<Network>();
	}

	public void PlayButton()
	{
		if(nickname.text.Length >= 4)
		{
			PhotonNetwork.playerName = nickname.text; 
			network.ConnectToServer();
		}
			
	}
}
