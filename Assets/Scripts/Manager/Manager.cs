using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour {
	public Image image;
	public void Pause() {
		GameController.m_IsPause = !GameController.m_IsPause;
		if (GameController.m_IsPause)
			image.color = Color.red;
		else
			image.color = Color.white;
	}
	public void Restart() {
		GameController.m_IsPause = false;
		SceneManager.LoadSceneAsync(1);
	}
}
