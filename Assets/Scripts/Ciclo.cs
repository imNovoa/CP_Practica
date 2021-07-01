using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Ciclo : MonoBehaviour
{
    public GameObject middayPlane;
    public GameObject midnigtPlane;
    public GameObject noonPlane;
    public GameObject dawnPlane;

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

        //MidnightPlane
        if (horaActual <= 01.30f)
        {
            Debug.Log("Entra en Medianoche");
            midnigtPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        } else if (horaActual <= 07.00f)
        {
            Debug.Log("sale de Medianoche");
            midnigtPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        //DawnPlane
        else if (horaActual <= 08.30f)
        {
            Debug.Log("Entra en Amanecer");
            dawnPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        else if (horaActual <= 12.00f)
        {
            Debug.Log("Sale de Amanecer");
            dawnPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        //MiddayPlane
        else if (horaActual <= 12.30f)
        {
            Debug.Log("Entra en Mediodia");
            middayPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        else if (horaActual <= 20.00f)
        {
            Debug.Log("Sale de Mediodia");
            middayPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        //NoonPlane
        else if (horaActual <= 21.30f)
        {
            Debug.Log("Entra en Anochecer");
            noonPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }
        else
        {
            Debug.Log("Sale de Anochecer");
            noonPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
        }

        if (horaActual >= 24.0f)
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
