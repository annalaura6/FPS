using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// http://doc-api.photonengine.com/en/PUN/current/group__public_api.html

public class Network : Photon.MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			Player.DebugPlayerList();
		}
	}

	// Conection to the server

	public void ConnectToServer()
	{
		PhotonNetwork.ConnectUsingSettings("FPS_1.0");
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 200, 20), PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		SceneManager.LoadScene(1);
		//PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

	//void OnEnable()
	//{
	//	//OnSceneLoaded function to start listening for a scene change as soon as this script is enabled.
	//	SceneManager.sceneLoaded += OnSceneLoaded;
	//}

	//void OnDisable()
	//{
	//	//OnSceneLoaded function to stop listening for a scene change as soon as this script is disabled.
	//	SceneManager.sceneLoaded -= OnSceneLoaded;
	//}

	//private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	//{
	//	Debug.Log(System.String.Format("Scene {0} has been loaded.", scene.name, mode.ToString()));
	//}

	private void OnLevelWasLoaded(int level)
	{
		if (level != 0)
		{
			PhotonNetwork.JoinRandomRoom();
		}
	}

	void OnPhotonPlayerConnected(PhotonPlayer pPlayer)
	{
		if (PhotonNetwork.isMasterClient)
		{
			// get PlayerIn, add pPlayer to the list and show it to every player
			photonView.RPC("PlayerIn", PhotonTargets.AllBuffered, pPlayer);
		}

	}

	void OnPhotonPlayerDisconnected(PhotonPlayer pPlayer)
	{
		if (PhotonNetwork.isMasterClient)
		{
			// remove the player
			photonView.RPC("PlayerOut", PhotonTargets.AllBuffered, pPlayer);
		}
	}

	[PunRPC]
	void PlayerIn(PhotonPlayer pPlayer)
	{
		Player player = new Player();
		player.nickname = pPlayer.NickName;
		player.pPlayer = pPlayer;
		Player.players.Add(player);
	}

	[PunRPC]
	void PlayerOut(PhotonPlayer pPlayer)
	{
		var tmpPlayer = Player.FindPlayer(pPlayer);
		if (tmpPlayer != null)
		{
			Player.players.Remove(tmpPlayer);
		}
	}

	void OnCreatedRoom()
	{
		//Remote Procedure Call (RPC) is a protocol that one program can use to request a service 
		//from a program located in another computer on a network without having to understand the network's details.

		photonView.RPC("PlayerIn", PhotonTargets.AllBuffered, PhotonNetwork.player);
	}
}


	