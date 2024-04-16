using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    // Loads the next scene with a given delay
    [SerializeField] float sceneLoadDelay = 2f;

    // To generate a random ID
    [SerializeField] private string id;

    [SerializeField] private Button continueGameButton;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid(){
        id = System.Guid.NewGuid().ToString();
    }

    ScoreKeeper scoreKeeper;

    private bool checkpointReached = false;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start(){
        if(!DataPersistenceManager.instance.HasGameData()){
            continueGameButton.interactable = false;
        }
    }
    public void NewGame()
    {
        scoreKeeper.ResetScore();
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        // save the gane right before we load the new scene, so that the new games data persists
        DataPersistenceManager.instance.SaveGame();
        StartCoroutine(WaitAndLoad("Level 1", sceneLoadDelay));
    }
    
    public void ContinueGame(){
        DataPersistenceManager.instance.LoadGame();
        StartCoroutine(WaitAndLoad(sceneLoadDelay));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver()
    {
        DataPersistenceManager.instance.SaveGame();
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator WaitAndLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadData(GameData data){
        data.checkpointReached.TryGetValue(id, out checkpointReached);
    }
    public void SaveData(ref GameData data){
        if( data.checkpointReached.ContainsKey(id)){
            data.checkpointReached.Remove(id);
        }
        data.checkpointReached.Add(id, checkpointReached);
    }
}
