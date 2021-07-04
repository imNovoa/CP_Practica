using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Globales
{
    public static float horaActual;

    //Los distritos están todos abiertos al iniciar la partida
    public static bool[] distritos = Enumerable.Repeat(true, 6).ToArray();
        
    public static Vector3[] generarPuertas()
    {
        Vector3[] puertas = new Vector3[60];
        puertas[0] = new Vector3(38.88f, 18.39f, -38.13f);
        puertas[1] = new Vector3(-8.75f, 18.39f, -37.4f);
        puertas[2] = new Vector3(19.53f, 18.39f, 29.42f);
        puertas[3] = new Vector3(36.35f, 18.39f, 38.95f);
        puertas[4] = new Vector3(-32.9f, 18.39f, 13.21f);
        puertas[5] = new Vector3(-44.2f, 18.39f, 36.4f);
        puertas[6] = new Vector3(43.49f, 18.39f, -105.33f);
        puertas[7] = new Vector3(20.01f, 18.39f, -125.6f);
        puertas[8] = new Vector3(-56.2f, 18.39f, -92.5f);
        puertas[9] = new Vector3(4.51f, 18.39f, -93.4f);

        puertas[10 + 0] = new Vector3(-81.31f, 18.39f, -36.85f);
        puertas[10 + 1] = new Vector3(-81.31f, 18.39f, -11.41f);
        puertas[10 + 2] = new Vector3(-75.89f, 18.39f, -105.75f);
        puertas[10 + 3] = new Vector3(-103.8f, 18.39f, -122.1f);
        puertas[10 + 4] = new Vector3(-153.5f, 18.39f, -120.3f);
        puertas[10 + 5] = new Vector3(-156.5f, 18.39f, -71.7f);
        puertas[10 + 6] = new Vector3(-159.73f, 18.39f, -29.36f);
        puertas[10 + 7] = new Vector3(-129.1f, 18.39f, 6.1f);
        puertas[10 + 8] = new Vector3(-84.62f, 18.39f, 36.72f);
        puertas[10 + 9] = new Vector3(-108.7f, 18.39f, 37.2f);

        puertas[20 + 0] = new Vector3(-92.5f, 18.39f, 135.2f);
        puertas[20 + 1] = new Vector3(-58.5f, 18.39f, 128.75f);
        puertas[20 + 2] = new Vector3(-11.16f, 18.39f, 146.44f);
        puertas[20 + 3] = new Vector3(27.2f, 18.39f, 74.7f);
        puertas[20 + 4] = new Vector3(-13.73f, 18.39f, 90.81f);
        puertas[20 + 5] = new Vector3(-73.59f, 18.39f, 92.59f);
        puertas[20 + 6] = new Vector3(-174.95f, 18.39f, 107.79f);
        puertas[20 + 7] = new Vector3(124f, 18.39f, 89.4f);
        puertas[20 + 8] = new Vector3(209.8f, 18.39f, 74.9f);
        puertas[20 + 9] = new Vector3(164.73f, 18.39f, 83.79f);

        puertas[30 + 0] = new Vector3(224.28f, 18.39f, -177.11f);
        puertas[30 + 1] = new Vector3(207f, 18.39f, -146.96f);
        puertas[30 + 2] = new Vector3(173.1f, 18.39f, -221.8f);
        puertas[30 + 3] = new Vector3(128.3f, 18.39f, -216.2f);
        puertas[30 + 4] = new Vector3(94.45f, 18.39f, -157.92f);
        puertas[30 + 5] = new Vector3(61.9f, 18.39f, -212.3f);
        puertas[30 + 6] = new Vector3(28.8f, 18.39f, -155.3f);
        puertas[30 + 7] = new Vector3(-49.4f, 18.39f, -223.6f);
        puertas[30 + 8] = new Vector3(-125.85f, 18.39f, -174.77f);
        puertas[30 + 9] = new Vector3(-140.81f, 18.39f, -218.24f);

        puertas[40 + 0] = new Vector3(-66.83f, 18.39f, 323.36f);
        puertas[40 + 1] = new Vector3(-30.7f, 18.39f, 339.6f);
        puertas[40 + 2] = new Vector3(-49.7f, 18.39f, 216f);
        puertas[40 + 3] = new Vector3(-143.05f, 18.39f, 219.81f);
        puertas[40 + 4] = new Vector3(-221.9f, 18.39f, 231.5f);
        puertas[40 + 5] = new Vector3(-298.1f, 18.39f, 167.4f);
        puertas[40 + 6] = new Vector3(-283.65f, 18.39f, 265.55f);
        puertas[40 + 7] = new Vector3(65.1f, 18.39f, 217.9f);
        puertas[40 + 8] = new Vector3(124.63f, 18.39f, 211.45f);
        puertas[40 + 9] = new Vector3(211.8f, 18.39f, 249.37f);

        puertas[50 + 0] = new Vector3(-345.6f, 18.39f, 25.6f);
        puertas[50 + 1] = new Vector3(-233.1f, 18.39f, 85f);
        puertas[50 + 2] = new Vector3(-210.9f, 18.39f, -26.3f);
        puertas[50 + 3] = new Vector3(-204.6f, 18.39f, 48.2f);
        puertas[50 + 4] = new Vector3(-250.1f, 18.39f, -287.6f);
        puertas[50 + 5] = new Vector3(-156.36f, 18.39f, -303.53f);
        puertas[50 + 6] = new Vector3(-104.23f, 18.39f, -273.71f);
        puertas[50 + 7] = new Vector3(-81.1f, 18.39f, -341.6f);
        puertas[50 + 8] = new Vector3(-76.18f, 18.39f, -301.16f);
        puertas[50 + 9] = new Vector3(-56.7f, 18.39f, -256.38f);
        return puertas;
    }
}
