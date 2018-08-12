using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

	public static Player myPlayer;
	public string nickname = "";
	public PhotonPlayer pPlayer;
	public Team team;
	public GameObject go;

	public static List<Player> players = new List<Player>();

	public static void DebugPlayerList()
	{
		string debugString = "Number of players: " + players.Count + ". Current players: \n";
		int i = 0;

		foreach (var player in players)
		{
			debugString += "ID: " + i + ", nickname: " + player.nickname + ", team: " + player.team + "\n";
			i++;
		}

		Debug.Log(debugString);		
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

public enum Team { TeamCT, TeamT }

