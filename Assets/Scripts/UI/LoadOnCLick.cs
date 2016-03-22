using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnCLick : MonoBehaviour {

	public void LoadScene (int level) 
	{
        if (level == -1) {
            Application.Quit();
        }
		SceneManager.LoadScene(level);
	}
}
