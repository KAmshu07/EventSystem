/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);

    private void Start()
    {
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f), 0f, Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f));

        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        // Instantiate the camera separately without making it a child of the player
        GameObject cameraObject = Instantiate(cameraPrefab);
        cameraObject.transform.position = playerObject.transform.position;
        cameraObject.transform.rotation = Quaternion.identity;
    }
}
*/