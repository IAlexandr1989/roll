using UnityEngine;
using System.Collections;

namespace roll
{
	public class Rotator : MonoBehaviour {

		// Update is called once per frame
		void Update () {
			transform.Rotate (new Vector3 (45, 15, 45) * Time.deltaTime);
		}

	}
}