using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace roll
{
	public class WallGenerator : MonoBehaviour {
		public Transform[] tilePrefabs;
		public Transform PickUp;
		public int sections;
		// Use this for initialization
		void Start () {
			GameObject pl = GameObject.FindGameObjectWithTag ("Player");
			PlayerController plc = pl.GetComponent<PlayerController> ();
			sections = plc.targets;
			GenerateLevel();
		}

		public void GenerateLevel() {
			Transform currentTile = null;
			Transform previousTile = null;
			Transform puClone = null;
			for (int i = 0; i < sections; i++) {
				currentTile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)], transform.position, Quaternion.identity) as Transform; // tilePrefabs[Random.Range(0, tilePrefabs.Length)].rotation

				if(previousTile) {
					Transform offset = previousTile.Find("End");

					if(offset) {
						int rr = Random.Range(0, 2);
						if (currentTile.tag.ToString() == "_I") {
							rr = rr + 1;
						}
						currentTile.rotation = Quaternion.Euler(0, 90 * rr, 0);//Random.rotation;//offset.rotation;
						currentTile.position += offset.position - currentTile.Find("Start").position;
					}
				}
				
				previousTile = currentTile;
				Transform endOffset = previousTile.Find ("End");
				Vector3 puPosition = new Vector3(endOffset.position.x, (float)(1.3), endOffset.position.z);
				puClone = Instantiate(PickUp, puPosition, Quaternion.identity) as Transform;
			}
		}
	}
}
