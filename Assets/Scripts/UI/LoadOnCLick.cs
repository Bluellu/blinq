using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnCLick : MonoBehaviour {

	public void LoadScene (int level) 
	{
		SceneManager.LoadScene(level);
	}
}
