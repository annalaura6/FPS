using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour {

	Transform spawnPoints;
	GameObject myPlayerGObj;
	GameObject newPlayerGObj;

	public void SpawnPlayer()
	{
		// Local
		Vector3 spawnPosition = Vector3.zero;

		if (spawnPoints != null && spawnPoints.childCount > 0)
		{
			spawnPosition = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)).transform.position;
		}
		// Set the player’s position to the chosen spawn point
		transform.position = spawnPosition;

		//spawnPoints = GameObject.Find("Map " + SceneManager.GetActiveScene().name + "/SpawnPoints/" + Player.myPlayer.team.ToString()).transform;
		//spawnPosition = spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)).transform.position;

		myPlayerGObj = PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity, 0);
		myPlayerGObj.GetComponent<FirstPersonController>().enabled = true;
		myPlayerGObj.transform.Find("CameraHolder").gameObject.SetActive(true);
		GetComponent<PhotonView>().RPC("SpawnPlayerRPC", PhotonTargets.AllBuffered, Player.myPlayer.nickname, myPlayerGObj.GetComponent<PhotonView>().viewID);
	}

	[PunRPC]
	void SpawnPlayerRPC(string nickname, PhotonMessageInfo pmi, int photonVID)
	{
		// For everyone
		newPlayerGObj = PhotonView.Find(photonVID).gameObject;
		newPlayerGObj.name = "Player_" + nickname;
		Player player = Player.FindPlayer(pmi.sender);
		player.go = newPlayerGObj;
	}
}
