using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Levels.SceneManagement.SceneInformation;
using UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject transitionContainer;
    public List<SceneInfo> scenes;

    private SceneTransition[] transitions;
    private GameObject player;
    private SceneInfo currentSceneInfo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        transitions = transitionContainer.GetComponentsInChildren<SceneTransition>();
    }

    public void TransitionToRandomScene(string transitionName)
    {
        if (scenes == null || scenes.Count == 0)
        {
            Debug.LogError("No scenes assigned.");
            return;
        }

        int index = Random.Range(0, scenes.Count);
        currentSceneInfo = scenes[index];
        StartCoroutine(LoadSceneAsync(currentSceneInfo, transitionName));
    }

    private IEnumerator LoadSceneAsync(SceneInfo sceneInfo, string transitionName)
    {
        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

        if (transition == null)
        {
            Debug.LogWarning($"No transition found with name '{transitionName}'");
            yield break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneInfo.scene);
        asyncLoad.allowSceneActivation = false;

        yield return transition.animateTransitionIn();

        asyncLoad.allowSceneActivation = true;

        SceneManager.sceneLoaded += OnSceneLoaded;

        yield return transition.animateTransitionOut();
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in scene.");
            return;
        }

        string spawnID = currentSceneInfo?.defaultSpawnID ?? "Entrance";

        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (var sp in spawnPoints)
        {
            if (sp.spawnID == spawnID)
            {
                player.transform.SetPositionAndRotation(sp.transform.position, sp.transform.rotation);
                return;
            }
        }

        Debug.LogWarning($"No spawn point found with ID '{spawnID}' in scene '{scene.name}'");
    }
}
