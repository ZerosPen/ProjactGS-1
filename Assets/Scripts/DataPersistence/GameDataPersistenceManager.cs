using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameDataPersistenceManager : MonoBehaviour
{
    public static GameDataPersistenceManager Instance { get; private set; }

    [Header("File Stroge Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private FileDataHandler _DataHandler;
    private List<IDataPersistence> _dataPersistenceListObjects;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        this._DataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this._dataPersistenceListObjects = FindallDataPresistance();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //See save data, then Load any save data from file data (Via FileHandler)
        this.gameData = _DataHandler.Load();
        
        //id there are no data file make new game filedata
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to default");
            NewGame();
        }

        //Push the loaded data to all script!
        foreach (IDataPersistence dataPersistece in _dataPersistenceListObjects)
        {
            dataPersistece.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scrtips to Update so can the can update to news data
        foreach (IDataPersistence dataPersistece in _dataPersistenceListObjects)
        {
            dataPersistece.SaveData(ref gameData);
        }

        // save the data via FileHandler
        _DataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindallDataPresistance()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
    
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

}
