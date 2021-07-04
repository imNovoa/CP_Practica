using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonText : MonoBehaviour
{
    public Text texto;
    public int distrito;
    public bool abierto = true;

    public void setText()
    {
        string inicioTexto = "Distrito " + distrito + ": ";

        if (abierto == true)
        {
            texto.text = inicioTexto + "Cerrado";
            abierto = false;
        }
        else
        {
            texto.text = inicioTexto + "Abierto";
            abierto = true;
        }

    }
}
