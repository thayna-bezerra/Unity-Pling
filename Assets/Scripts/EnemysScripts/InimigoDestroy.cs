using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDestroy : MonoBehaviour
{
    void Start()
    {
        Invoke("Destruidor", 2.5f);
    }

    void Destruidor()
    {
        Destroy(this.gameObject);
    }

}
