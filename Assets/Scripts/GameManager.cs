using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// setup camera follow system
	private CameraFollow _cameraFollow;
	private Vector3 _cameraFollowPosition;
	
	public archer archerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
	    // setup camera follow system
	    _cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
	    _cameraFollow.Setup(() => _cameraFollowPosition);
    }

    // Update is called once per frame
    void Update()
    {
	    // setup camera follow system
        float movement = 10f;
        float edgeSize = 50f;
        if (Input.GetKey(KeyCode.LeftArrow))
	        _cameraFollowPosition.x -= movement * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
	        _cameraFollowPosition.x += movement * Time.deltaTime;
        if (Input.mousePosition.x > Screen.width - edgeSize)
	        _cameraFollowPosition.x += movement * Time.deltaTime;
        if (Input.mousePosition.x < edgeSize)
	        _cameraFollowPosition.x -= movement * Time.deltaTime;
    }
    public void generateArcher()
    {
	    Vector3 playerCastlePosition = GameObject.FindWithTag("PlayerCastle").transform.position; 
	    Instantiate(archerPrefab, new Vector3(playerCastlePosition.x, playerCastlePosition.y-0.5f, 0), Quaternion.identity);
    }
}
