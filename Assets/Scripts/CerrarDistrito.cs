using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarDistrito : MonoBehaviour
{
    public void AbrirCerrrar()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();


        if (rend.enabled)
        {
            rend.enabled = false;
        }else
            rend.enabled = true;
    }
}
