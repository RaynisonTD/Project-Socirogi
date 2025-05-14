using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UI;
using System.Linq;


public class LevelManager : MonoBehaviour
{
    // Static instance for global access (Singleton pattern)
    public static LevelManager instance;

    // Parent GameObject that holds all scene transition scripts
    public GameObject transitionContainer;

    // Array to store all SceneTransition components found in the container
    private SceneTransition[] transitions;

    // List of scene names to load from randomly
    public List<string> sceneNames;

    // Called when the script instance is being loaded
    public void Awake()
    {
        // Ensure only one instance exists (Singleton logic)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    // Called on the first frame the script is active
    private void Start()
    {
        // Find all SceneTransition components in the transition container
        transitions = transitionContainer.GetComponentsInChildren<SceneTransition>();
    }

    // Coroutine to handle loading scenes with transitions
    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        // Find the transition with the given name
        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

        // If not found, log a warning and stop the coroutine
        if (transition == null)
        {
            Debug.LogWarning($"No transition found with name '{transitionName}'");
            yield break;
        }

        // Begin loading the scene asynchronously but don't activate it yet
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Play the transition-in animation
        yield return transition.animateTransitionIn();

        // Allow the scene to be activated
        asyncLoad.allowSceneActivation = true;

        // Play the transition-out animation
        yield return transition.animateTransitionOut();
    }

    // Loads a random scene from the list using the given transition
    public void TransitionToRandomScene(string transitionName)
    {
        // If no scenes are set, log an error
        if (sceneNames == null || sceneNames.Count == 0)
        {
            Debug.LogError("No scenes assigned to LevelManager");
            return;
        }

        // Choose a random scene from the list
        int index = UnityEngine.Random.Range(0, sceneNames.Count);
        string sceneToLoad = sceneNames[index];

        // Start loading the scene with a transition
        StartCoroutine(LoadSceneAsync(sceneToLoad, transitionName));
    }

    // Loads a specific scene by name using the given transition
    public void TransitionToScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }
}


