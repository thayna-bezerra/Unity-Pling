using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruidor : MonoBehaviour
{
    //invocar o metodo "Destruidor" (destruir este game object) apos 0.2f 

    void Start() { Invoke("Destruir", 0.2f); }

    void Destruir() { Destroy(this.gameObject); }
}
