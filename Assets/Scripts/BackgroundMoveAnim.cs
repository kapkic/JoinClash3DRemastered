using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (new Vector3(1,0,1) * Time.deltaTime * 5);
    }
}
