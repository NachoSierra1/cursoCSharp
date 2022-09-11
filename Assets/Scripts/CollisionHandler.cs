using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int delayTime = 1;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip succesClip;

    private AudioSource audioSource; 

    private bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }
        
        switch (other.gameObject.tag)
            {
                case "Respawn":
                    Debug.Log("Get to the other pad!");
                    break;
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartSuccesSequence();
                    break;
                case "Fuel":
                    Debug.Log("You picked up fuel");
                    break;
                default:
                    StartCrashSequence();
                    break;

            }
        
    }

     void StartSuccesSequence()
     {
         isTransitioning = true;
        // todo add sfx upon succes
        audioSource.PlayOneShot(succesClip);
        // todo add particle effect upon succes
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",1f);
    }
     
     void StartCrashSequence()
     {
         // todo add sfx upon crash
         audioSource.PlayOneShot(crashClip);
         // todo add particle effect upon crash
         GetComponent<Movement>().enabled = false;
         Invoke("ReloadScene",1f);
        
     }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
       
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}