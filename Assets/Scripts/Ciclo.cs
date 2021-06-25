using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Ciclo : MonoBehaviour
{
    private float horaInicio = 6.0f;
    private float duracionDia = 24.0f;
    private float horaActual;
    private float timeRate;
    private int horaInt;
    public Text texto;
    private double decimales;

    // Start is called before the first frame update
    void Start()
    {
        timeRate = 5.0f / duracionDia;
        horaActual = horaInicio;
    }

    // Update is called once per frame
    void Update()
    {
        horaActual += timeRate * Time.deltaTime;
        decimales = horaActual - Math.Truncate(horaActual);
        decimales = decimales * 60;
        if(horaActual >= 24.0f)
        {
            horaActual = 0.0f;
        }
        horaInt = (int)horaActual;
        texto.text = horaInt.ToString() + ":" + decimales.ToString("00");
    }

    public float getHoraActual()
    {
        return this.horaActual;
    }
}
