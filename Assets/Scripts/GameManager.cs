using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public archer archerPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generateArcher() {
	Vector3 playerCastlePosition = GameObject.FindWithTag("PlayerCastle").transform.position;
	Instantiate(archerPrefab, new Vector3(playerCastlePosition.x, playerCastlePosition.y-0.5f, 0), Quaternion.identity);
    }
}
