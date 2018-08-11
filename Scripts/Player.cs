using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	public string nickname = "";
	public PhotonPlayer pPlayer;

	public static List<Player> players = new List<Player>();

	public static void DebugPlayerList()
	{
		Debug.Log("Number of players: " + players.Count + ". Current players: ");

		foreach (var player in players)
		{
			Debug.Log(player.nickname);
		}
	}

	public static Player FindPlayer(PhotonPlayer pPlayer)
	{
		for (int i = 0;  i < players.Count; i++)
		{
			if (players[i].pPlayer == pPlayer)
			{
				return players[i];
			}
		}

		return null;
	}
}

