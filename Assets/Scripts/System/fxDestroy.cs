using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxDestroy : MonoBehaviour
{
    void Start()
    {
        Invoke("Destruidor", 0.2f);
    }

    void Destruidor()
    {
        Destroy(this.gameObject);
    }
}
