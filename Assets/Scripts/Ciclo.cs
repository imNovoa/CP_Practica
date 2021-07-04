using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


    public class Ciclo : MonoBehaviour
    {
        public Ciclo() { }

        public GameObject middayPlane;
        public GameObject midnigtPlane;
        public GameObject noonPlane;
        public GameObject dawnPlane;

        private float horaInicio = 6.0f;
        private float duracionDia = 24.0f;
        //private float horaActual;
        private float timeRate;
        private int horaInt;
        public Text texto;
        private double decimales;

        // Start is called before the first frame update
        void Start()
        {
            timeRate = 5.0f / duracionDia;
            Globales.horaActual = horaInicio;
        }

        // Update is called once per frame
        void Update()
        {
            Globales.horaActual += timeRate * Time.deltaTime;
            decimales = Globales.horaActual - Math.Truncate(Globales.horaActual);
            decimales = decimales * 60;

            //MidnightPlane
            if (Globales.horaActual <= 01.30f)
            {
                midnigtPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
            }
            else if (Globales.horaActual <= 07.00f)
            {
                midnigtPlane.transform.position = new Vector3(-4.180901f, -18.1f, -74.245f);
            }
            //DawnPlane
            else if (Globales.horaActual <= 08.30f)
            {
                dawnPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
            }
            else if (Globales.horaActual <= 12.00f)
            {
                dawnPlane.transform.position = new Vector3(-4.180901f, -18.1f, -74.245f);
            }
            //MiddayPlane
            else if (Globales.horaActual <= 12.30f)
            {
                middayPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
            }
            else if (Globales.horaActual <= 20.00f)
            {
                middayPlane.transform.position = new Vector3(-4.180901f, -18.1f, -74.245f);
            }
            //NoonPlane
            else if (Globales.horaActual <= 21.30f)
            {
                noonPlane.transform.position = new Vector3(-4.180901f, 18.1f, -74.245f);
            }
            else
            {
                noonPlane.transform.position = new Vector3(-4.180901f, -18.1f, -74.245f);
            }

            if (Globales.horaActual >= 24.0f)
            {
                Globales.horaActual = 0.0f;
            }
            horaInt = (int)Globales.horaActual;
            texto.text = horaInt.ToString() + ":" + decimales.ToString("00");
        }

        public float getHoraActual()
        {
            return Globales.horaActual;
        }
    }