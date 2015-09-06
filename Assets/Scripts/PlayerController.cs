using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace roll
{
	public class PlayerController : MonoBehaviour {
		public float speed;
		private Rigidbody rb;
		private int count;
		public int targets;
		public Text targetText;
		public Text winText;
		public Text levelText;
		public int level ;

		IEnumerator restarting() {
			Debug.Log("Before Waiting 2 seconds");
			yield return new WaitForSeconds(2);
			Application.LoadLevel(Application.loadedLevel);
			Debug.Log("After Waiting 2 Seconds");
		}
		void Start () {
			if (level == 1) {
				//PlayerPrefs.SetInt("Player level", level);
			}
			int lvl = PlayerPrefs.GetInt("Player level");
			if (lvl != 0) {
				level = lvl;
			}
			rb = GetComponent<Rigidbody>();
			targets = level;
			finishCheck ();
			levelText.text = "Level: " + level.ToString();
		}
		void FixedUpdate () {
			float moveHorizontal = Input.acceleration.x; // Input.GetAxis ("Horizontal");
			float moveVertical = Input.acceleration.y; // Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}
		
		void OnTriggerEnter (Collider other) {
			if (other.gameObject.CompareTag("Pick Up")) {
				other.gameObject.SetActive(false);
				count = count + 1;
				finishCheck();
			}
			if (other.gameObject.CompareTag ("Boundary")) {
				if (count != targets) {
					winText.text = "Restarting..";
					StartCoroutine(restarting());
				}
			}
		}

		void finishCheck () {
			targetText.text = "Targets: " + (targets - count).ToString();
			if (count == targets) { 
				winText.text = "You Win. Next round!";
				level = level + 1;
				PlayerPrefs.SetInt ("Player level", level);
				StartCoroutine(restarting());
			}
		}

		public void resetLevel () {
			PlayerPrefs.SetInt ("Player level", 1);
			winText.text = "Level reset..";
			StartCoroutine(restarting());
		}
	}
}