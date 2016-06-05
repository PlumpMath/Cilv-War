using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	private bool is_load_gameplay = false;
	private bool load_scene = false;
	public Text m_Text_loading;

	void Update () {
		if (is_load_gameplay && !load_scene) {
			load_scene = true;

			m_Text_loading.text = "Loading...";

			SceneManager.LoadSceneAsync (1);
		}
	}

	public void setLoadGameplay () {
		is_load_gameplay = true;
	}
}
