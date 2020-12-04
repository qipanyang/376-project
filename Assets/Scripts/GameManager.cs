using System;
using System.Collections;
using System.Collections.Generic;
using Castle;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance => _instance;
	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}
    public static GameManager Ctx;

    public TextManager TextManager;

    public GameObject playerCastleObject;
    public GameObject enemyCastleObject;

    public MinionsManager MinionsManager;
    public GoldManager GoldManager;
    
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
	    GoldManager = GetComponent<GoldManager>();
	    TextManager = new TextManager();

	    // setup camera follow system
	    _cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
	    _cameraFollow.Setup(() => _cameraFollowPosition);
	    
	    InitializeCastleStatus();
    }

    private void InitializeCastleStatus()
    {
        EnemyCastle.Health = 1000;
        PlayerCastle.Health = 1000;
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

        if (EnemyCastle.Health <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if (PlayerCastle.Health <=0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void LoadData(SaveManager.SaveData saveData)
    {
        GoldManager.OnLoad(saveData.goldData);
        MinionsManager.OnLoad(saveData.minionListSaveData);
        Ctx.EnemyCastle.Health = saveData.EnemyCastleHealth;
        Ctx.PlayerCastle.Health = saveData.PlayerCastleHealth;

    }

}