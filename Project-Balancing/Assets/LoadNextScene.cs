using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealthManager>() != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
