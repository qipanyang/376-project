using System;
using System.Collections;
using System.Collections.Generic;
using Castle;
using Manager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ctx;

    public TextManager TextManager;

    public GameObject playerCastleObject;
    public GameObject enemyCastleObject;

    public MinionsManager MinionsManager;
    public EnemyCastle EnemyCastle;
    public PlayerCastle PlayerCastle;
    
	// setup camera follow system
	private CameraFollow _cameraFollow;
	private Vector3 _cameraFollowPosition;
	
    
    // Start is called before the first frame update
    void Start()
    {
	    
	    Ctx = this;
	    MinionsManager = GetComponent<MinionsManager>();
	    TextManager = new TextManager();

	    // setup camera follow system
	    _cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
	    _cameraFollow.Setup(() => _cameraFollowPosition);
	    
	    InitializeCastleStatus();
    }

    private void InitializeCastleStatus()
    {
        EnemyCastle.Health = 200;
        PlayerCastle.Health = 200;
        PlayerCastle.MoneyRate = 20;
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

}