using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAreaScript : MonoBehaviour {

	public int direction = 0;
	public int maxNumberOfLine = 10;
	public GameObject rock;
	public GameObject[] loots;

	private RockScript[][] rocks;
	private int[] numberOfRocks;
	
	private float loc;
	private int numberOfLineDeleted;
	private int numberOfLineToDelete;

	private int[] nbOfIceByLayer;
	private int[] nbOfIceCartridge;
	private int[] nbOfFireByLayer;
	private int[] nbOfFireCartridge;
	private int[] nbOfBoundingByLayer;
	private int[] nbOfBoundingCartridge;
	private int[] nbOfLaserByLayer;
	private int[] nbOfLaserCartridge;
	
	private List<int>[] iceLocation;
	private List<int>[] fireLocation;
	private List<int>[] boundingLocation;
	private List<int>[] laserLocation;

	// Use this for initialization
	void Start () {
		// Init variables
		this.numberOfLineDeleted = 0;
		this.numberOfLineToDelete = maxNumberOfLine - 4;
		this.numberOfRocks = new int[4] {10, 10, 10, 10};
		
		// Init rock localisation
		if (direction == 1) {
			loc = -2.5f;
		} 
		else {
			loc = -2f;
		}

		// Create mine
		this.rocks = new RockScript[4][];
		for(int line = 0; line < 4; ++line) {
			// Create lines
			this.rocks[line] = new RockScript[10];

			for(int column = 0; column < 10; ++column) {
				// Create rocks
				this.rocks[line][column] = Instantiate(rock, transform.position + new Vector3(direction * line * 0.5f, (column * 0.5f) + loc, 0f), Quaternion.identity).GetComponent<RockScript>();
				this.rocks[line][column].setParams(direction);
			}
		}
		print("Mine created !");

		// Init loot distribution
		this.nbOfIceByLayer = new int[10] {1, 1, 1, 2, 2, 2, 3, 3, 3, 0};
		this.nbOfFireByLayer = new int[10] {1, 1, 1, 2, 2, 2, 3, 3, 3, 0};
		this.nbOfBoundingByLayer = new int[10] {1, 1, 1, 2, 2, 2, 3, 3, 3, 0};
		this.nbOfLaserByLayer = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1};

		this.nbOfIceCartridge = new int[10] {1, 1, 1, 2, 2, 2, 3, 3, 3, 0};
		this.nbOfFireCartridge = new int[10] {2, 2, 2, 4, 4, 4, 6, 6, 6, 0};
		this.nbOfBoundingCartridge = new int[10] {3, 3, 3, 5, 5, 5, 7, 7, 7, 0};
		this.nbOfLaserCartridge = new int[10] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1};

		// Init loot location variables
		this.iceLocation = new List<int>[10];
		this.fireLocation  = new List<int>[10];
		this.boundingLocation  = new List<int>[10];
		this.laserLocation = new List<int>[10];

		// Init loot location
		for(int layer = 0; layer < 10; ++layer) {
			List<int> ice = new List<int>();
			List<int> fire = new List<int>();
			List<int> bounding = new List<int>();
			List<int> laser = new List<int>();

			for(int i = 0; i < this.nbOfIceByLayer[layer]; ++i) {
				ice.Add(Random.Range(0, 9));
			}
			for(int i = 0; i < this.nbOfFireByLayer[layer]; ++i) {
				fire.Add(Random.Range(0, 9));
			}
			for(int i = 0; i < this.nbOfBoundingByLayer[layer]; ++i) {
				bounding.Add(Random.Range(0, 9));
			}
			for(int i = 0; i < this.nbOfLaserByLayer[layer]; ++i) {
				laser.Add(Random.Range(0, 9));
			}

			iceLocation[layer] = ice;
			fireLocation[layer] = fire;
			boundingLocation[layer] = bounding;
			laserLocation[layer] = laser;
		}

	}
	
	// Update is called once per frame
	void Update () {
		// Check rocks PV
		for(int line = 0; line < 4; ++line) {
			for(int column = 0; column < 10; ++column) {
				// If rock heal point < 0
				if(this.rocks[line][column].getPv() <= 0) {
					// Save the rock position
					Vector3 v = this.rocks[line][column].getLocation();

					// Move the rock
					this.rocks[line][column].moveDirectly(new Vector3(direction * 2f, 0f, 0f));

					// Reset rock heal point
					this.rocks[line][column].resetPv();

					// Decrease the number of rock on the line
					this.numberOfRocks[line] = this.numberOfRocks[line] - 1;

					// Create loot at the rock position
					int pos = column;
					int layer = line + 4 * this.rocks[line][column].getBufferNumberOfTurn();

					for(int i = 0; i < iceLocation[layer].Count; ++i) {
						if((iceLocation[layer])[i] == pos) {
							Item projectileIce = Instantiate(loots[0], v, Quaternion.identity).GetComponent<Item>();
							projectileIce.setUtilization(nbOfIceCartridge[layer]);
						}
					}
					for(int i = 0; i < fireLocation[layer].Count; ++i) {
						if((fireLocation[layer])[i] == pos) {
							Item projectileFire = Instantiate(loots[1], v, Quaternion.identity).GetComponent<Item>();
							projectileFire.setUtilization(nbOfFireCartridge[layer]);
						}
					}
					for(int i = 0; i < boundingLocation[layer].Count; ++i) {
						if((boundingLocation[layer])[i] == pos) {
							Item projectileBounding = Instantiate(loots[2], v, Quaternion.identity).GetComponent<Item>();
							projectileBounding.setUtilization(nbOfBoundingCartridge[layer]);
					}
					}
					for(int i = 0; i < laserLocation[layer].Count; ++i) {
						if((laserLocation[layer])[i] == pos) {
							Item projectileLaser = Instantiate(loots[3], v, Quaternion.identity).GetComponent<Item>();
							projectileLaser.setUtilization(nbOfLaserCartridge[layer]);
						}
					}
					
					// Increase the number of buffer turn
					this.rocks[line][column].increaseBufferNumberOfTurn();
				}
			}
		}

		// Check number of rock on the first line
		if ((this.numberOfRocks[numberOfLineDeleted % 4] == 0) && (numberOfLineDeleted < numberOfLineToDelete)) {
			// Restart count
			this.numberOfRocks[numberOfLineDeleted % 4] = 10;
			
			// Move the mine
			for(int line = 0; line < 4; ++line) {
				for(int column = 0; column < 10; ++column) {
					this.rocks[line][column].move(new Vector3 (-direction * 0.5f, 0.0f, 0.0f), -direction);
				}
			}

			// Increment number of line deleted
			numberOfLineDeleted++;
		}
	}
}
