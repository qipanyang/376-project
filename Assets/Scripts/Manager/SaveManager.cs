using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using Manager;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;

    private string _path;
    private SaveData data;


    public SaveManager()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _path = Application.persistentDataPath + "/save";

        Button savebtn = saveButton.GetComponent<Button>();
        Button loadbtn = loadButton.GetComponent<Button>();
        savebtn.onClick.AddListener(Save);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(_path + ".txt", FileMode.Create);
        data = new SaveData();
        serializer.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saving.." + _path);

    }

    void Load()
    {
        if (File.Exists(_path + "save.save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(_path + ".txt", FileMode.Open);
            data = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loading.." + _path);
        }
        else Debug.Log("No Loading Files" );
    }

    [SerializeField]
    public class SaveData
    {
        public float time;
        public GoldData goldData;
        public int EnemyCastleHealth;
        public int PlayerCastleHealth;
        public MinionListSaveData minionListSaveData;

        public SaveData()
        {
            time = Time.time;
            goldData = GameManager.Ctx.GoldManager.OnSave() as GoldData;
            EnemyCastleHealth = GameManager.Ctx.EnemyCastle.Health;
            PlayerCastleHealth = GameManager.Ctx.PlayerCastle.Health;
            minionListSaveData = GameManager.Ctx.MinionsManager.OnSave() as MinionListSaveData;
        }


    }
}
