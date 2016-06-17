using UnityEngine;
using System.Collections;

public class activeTerrain : MonoBehaviour {

    private GameObject[] terrains;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        terrains = GameObject.FindGameObjectsWithTag("terrain");
        StartCoroutine(checkIfInRange());	
	}

    IEnumerator checkIfInRange()
    {
        while(true)
        {
            foreach(GameObject terrainPart in terrains)
            {
                //print(terrains.Length);
                if(terrainPart.transform.isDistanceSmallerThan(player.transform.position, 120f))
                {
                    terrainPart.SetActive(true);
                }
                else
                {
                    terrainPart.SetActive(false);
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
