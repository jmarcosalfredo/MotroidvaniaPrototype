using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    private FileDataHandler dataHandler;
    private GameData gameData;
    private List<ISaveable> allSaveables;

    [SerializeField] private string fileName = "gameData.json";
    [SerializeField] private bool encryptData = true;

    private IEnumerator Start()
    {
        Debug.Log(Application.persistentDataPath);
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        allSaveables = FindISaveables();

        yield return new WaitForSeconds(0.05f);
        LoadGame();
    }

    private void LoadGame()
    {
        gameData = dataHandler.LoadData();

        if (gameData == null)
        {
            Debug.Log("No data was found. Creating a new save!.");
            gameData = new GameData();
            return;
        }

        foreach (var saveable in allSaveables)
        {
            saveable.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var saveable in allSaveables)
        {
            saveable.SaveData(ref gameData);
        }

        dataHandler.SaveData(gameData);
    }

    [ContextMenu("*******Delete Save Data*******")]
    public void DeleteSave()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveable> FindISaveables()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<ISaveable>().ToList();
    }
}
