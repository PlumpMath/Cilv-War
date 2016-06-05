using UnityEngine;
using Complete;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

	public static bool m_IsPause = false;
	public GameObject[] m_ArrStar;
	public GameObject m_ExplosionPrefab;

	public int countStar;
	public GameObject m_GameOverCanvas;
	public Text m_ScoreText;
	public Text m_GameScoreText;
	public GameObject m_PowerFull;

	private PlayerMovement _playerMovement;
	private GameObject _clonePowerFull;
	private bool _isPowerShow = false;
	private bool _isStillShow = false;

	private int _score = 0;
	private int _scoreInZone = 0;
	private bool _isOccupy = false;

	private float _timeToShow = 0;
	Vector3 position = new Vector3(40, 1, 72.9f);

	private AudioSource _explosionAudio; // The audio source to play when the tank explodes.
	private ParticleSystem _explosionParticles; // The particle system the will play when the tank is destroyed.


	private void Awake() {
		// Instantiate the explosion prefab and get a reference to the particle system on it.
		_explosionParticles = Instantiate(m_ExplosionPrefab).GetComponent < ParticleSystem > ();

		// Get a reference to the audio source on the instantiated prefab.
		_explosionAudio = _explosionParticles.GetComponent < AudioSource > ();

		// Disable the prefab so it can be activated when it's required.
		_explosionParticles.gameObject.SetActive(false);

		for (int i = 0; i < m_ArrStar.Length; i++) {
			m_ArrStar [i].SetActive (false);
		}
	}

	// Use this for initialization
	void Start() {
		//this sets timescale to 1 at start.
		Time.timeScale = 1;
		//this disables GameOverCanves and GameCanvas, enables startCanvas.
		m_GameOverCanvas.SetActive(false);

		_playerMovement = FindObjectOfType < PlayerMovement > ();
	}

	// Update is called once per frame
	void Update() {
		if (!m_IsPause) {
			_playerMovement.enabled = true;
			_timeToShow += Time.deltaTime;

			if (_isPowerShow) {
				if (GameObject.Find("Player") != null) {
					position.x = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.x;
					position.z = Random.Range(0, 10) + GameObject.Find("Player").gameObject.transform.position.z;
					_clonePowerFull = (GameObject)Instantiate(m_PowerFull, position, Quaternion.identity);

					_isPowerShow = false;
				}
			} else {
				if (_timeToShow > 30) {
					Destroy(_clonePowerFull);
					_isStillShow = false;
					_timeToShow = 0;
				}
			}

			if(getOccupy()){
				GameObject []objs = GameObject.FindGameObjectsWithTag (_playerMovement.m_EPlayerZone);
				foreach (GameObject obj in objs)
					Destroy (obj);
			}
		} else {
			_playerMovement.enabled = false;
		}
	}

	public void AddScore() {
		//Add score by 1 and showing that score to GameScoreText.
		_score += 1;
		if (_playerMovement.m_EPlayerZone != "") {
			if (Vector3.Distance(GameObject.FindGameObjectWithTag(_playerMovement.m_EPlayerZone).transform.position,
					_playerMovement.transform.position) < 44) {
				_scoreInZone++;
			} else
				_scoreInZone = 0;
		}
		m_GameScoreText.text = _score.ToString();

		int _time = (int)Time.time;
		if (Advertisement.IsReady() && _score % 7 ==0 && _time > 900)
			Advertisement.Show();
	}

	public void RequestPower(float statePower) {
		if (statePower < 50 && !_isStillShow) {
			_isPowerShow = true;
			_isStillShow = true;
		}
	}

	public int getScoreInZone() {
		return _scoreInZone;
	}
	public void setScoreInZone(int value) {
		_scoreInZone = value;
	}
	public int getCountStar() {
		return countStar;
	}
	bool getOccupy(){
		return _isOccupy;
	}
	public void setOccupy(bool value){
		_isOccupy = value;
	}
	int min = 20, max = 30;
	public void addEnemy(Transform ene) {
		float x = ene.transform.position.x + Random.Range(min, max) ;
		float z = ene.transform.position.z + Random.Range(min, max) ;
		if (x > 140)
			x  =  ene.transform.position.x - Random.Range(min, max) ;
		if (z > 140)
			z = ene.transform.position.z - Random.Range (min, max);
		Vector3 position = new Vector3(x, 0, z);
		ene.transform.position = position;
		ene.gameObject.SetActive(true);
	}

	public void GameOver() {
		if (Advertisement.IsReady() && Random.Range(1, 3) == 2)
			Advertisement.Show();
		//Plays GameOverSound
		AudioSource audio = GetComponent < AudioSource > ();
		audio.Play();
		//Pause the time
		Time.timeScale = 0;
		//activate the gameover canvas
		m_GameOverCanvas.SetActive(true);
		//show the score value on ScoreText
		m_ScoreText.text = "Your Score: " + _score.ToString();

		//show highscore value on HighScoreText
		for (int i = 0; i < countStar; i++) {
			m_ArrStar [i].SetActive (true);
		}
	}

	public void OnDeath(Transform m_Transform) {
		TankHealth.m_Dead = true;

		_explosionParticles.transform.position = m_Transform.position;
		_explosionParticles.gameObject.SetActive(true);

		_explosionParticles.Play();

		_explosionAudio.Play();
		_explosionAudio.Stop();
		m_Transform.gameObject.SetActive(false);
	}
}
