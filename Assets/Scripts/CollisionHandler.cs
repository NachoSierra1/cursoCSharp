using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int delayTime = 1;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip succesClip;
    
    [SerializeField] ParticleSystem succesParticles;
    [SerializeField] ParticleSystem crashParticles;

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
        audioSource.Stop();
        audioSource.PlayOneShot(succesClip);
        succesParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",1f);
    }
     
     void StartCrashSequence()
     {
         isTransitioning = true;
         audioSource.Stop();
         audioSource.PlayOneShot(crashClip);
         crashParticles.Play();
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