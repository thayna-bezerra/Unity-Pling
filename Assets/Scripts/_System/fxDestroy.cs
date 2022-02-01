using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxDestroy : MonoBehaviour
{
    //invocar o metodo "Destruidor" (destruir este game object) apos 2.5f 

    void Start() { Invoke("Destruir", 2.5f); } 

    void Destruir() { Destroy(this.gameObject); }
}
