using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if( other.TryGetComponent(out PlayerStateMachine playerStateMachine))
            SceneManager.LoadScene(0);
    }
}
