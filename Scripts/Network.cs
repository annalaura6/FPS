using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// http://doc-api.photonengine.com/en/PUN/current/group__public_api.html

public class Network : Photon.MonoBehaviour
{
	MyGameManager myGameManager;

	void Start()
	{
		myGameManager = GetComponent<MyGameManager>();
	}

	void Update()
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
	}

	void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

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
			int team = 0;

			if(Player.players[Player.players.Count - 1].team == Team.TeamCT)
			{
				team = 1;
			}

			else
			{
				team = 0;
			}

			// get PlayerIn, add pPlayer to the list and show it to every player
			photonView.RPC("PlayerIn", PhotonTargets.AllBuffered, pPlayer, team);
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
	void PlayerIn(PhotonPlayer pPlayer, int team)
	{
		Player player = new Player();
		Player.players.Add(player);
		player.nickname = pPlayer.NickName;
		player.pPlayer = pPlayer;
		
		player.team = (Team) team;

		if(pPlayer == PhotonNetwork.player)
		{
			// Spawn a player
			Player.myPlayer = player;
			myGameManager.SpawnPlayer();
		}
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

		int team = Random.Range(0, 2);
		//Team randomTeam = (Team) Random.Range(1,3);
		photonView.RPC("PlayerIn", PhotonTargets.AllBuffered, PhotonNetwork.player, team);
	}
}