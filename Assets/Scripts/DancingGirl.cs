using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingGirl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetInteger("number", Random.Range(0, 4));
    }
}
