using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fxDestroy : MonoBehaviour
{
    //invocar o metodo "Destruidor" (destruir este game object) apos 0.2f 

    void Start() { Invoke("Destruidor", 0.2f); } 

    void Destruidor() { Destroy(this.gameObject); }
}
