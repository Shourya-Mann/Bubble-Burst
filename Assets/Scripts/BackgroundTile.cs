using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{

    public GameObject[] bubbles;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        int toUse = Random.Range(0, bubbles.Length);
        GameObject bubble = Instantiate(bubbles[toUse], transform.position, Quaternion.identity);
        bubble.transform.parent = this.transform; // bubble is the child of the backtile
        bubble.name = this.gameObject.name; // sanity check
    }
}
