using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{

    public static int numAldeanos = 50;
    public GameObject Aldeano;
    private GameObject[] Aldeanos = new GameObject[numAldeanos];
    private Vector3[] posSpawn = new Vector3[numAldeanos];

    private static int districts = 6;
    private int[] population = new int[districts];
    private static int totalSpawnpoints = 4 + 3 + 4 + 6 + 6 + 8;
    private Vector3[] spawnpoints = new Vector3[totalSpawnpoints];
    private int[] spawnpoints_per_district = new int[districts] ;

    private void Awake()
    {
        posSpawn = this.getPosiciones();
        generarAldeanos();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] getAldeanos()
    {
        return this.Aldeanos;
    }

    private Vector3[] getPosiciones()
    {
        for(int i = 0; i < numAldeanos; i++)
        {
            //Asignar posicion aquí, ya sea random o de la manera que lo gestionemos (por ejemplo creando pivotes fijos para que no se metan en la malla del NavMesh
            //y también tener spawnear por aldeanos por igual en todos los distritos
            //this.posSpawn[i] = new Vector3(37.57f, 10, -12.82f);
            this.posSpawn[i] = new Vector3(100, 0, 100);
        }
        return this.posSpawn;
    }

    private void generarAldeanos()
    {
        Aldeanos[0] = this.Aldeano;
        for (int i = 1; i < numAldeanos; i++)
        {
            Aldeanos[i] = Instantiate(Aldeano);
            //Aldeanos[i].transform.position = new Vector3(100, Aldeanos[i].transform.position.y, 200);
            Aldeanos[i].transform.position = new Vector3(40, 0, 0);
            //Aquí sería Aldeanos[i].transform.position = this.posSpawn[i]
        }
    }

    //Método específico para la config. actual del distrito. Asigna todas las coordenadas de spawn de los aldeanos.
    private void GestionarSpawnpoints()
    {

        //Rellenamos el array de spawnpoints_per_district
        spawnpoints_per_district[0] =  4;
        spawnpoints_per_district[1] =  3;
        spawnpoints_per_district[2] =  4;
        spawnpoints_per_district[3] =  6;
        spawnpoints_per_district[4] =  6;
        spawnpoints_per_district[5] =  8;

        //Rellenamos cada spawnpoint:

        ///------------Dist 1
        spawnpoints[0] = new Vector3(40, 10, -12);
        spawnpoints[1] = new Vector3(-2, 10, -72);
        spawnpoints[2] = new Vector3(-2, 10, 47);
        spawnpoints[3] = new Vector3(-42, 10, -12);
        ///------------Dist 2
        spawnpoints[4] = new Vector3(-122, 10, -93);
        spawnpoints[5] = new Vector3(-142, 10, -12);
        spawnpoints[6] = new Vector3(-122, 10, 47);
        ///------------Dist 3
        spawnpoints[7] = new Vector3(-102, 10, 87);
        spawnpoints[8] = new Vector3(17, 10, 107);
        spawnpoints[9] = new Vector3(117, 10, 107);
        spawnpoints[10] = new Vector3(200, 10, 107);
        ///------------Dist 4
        spawnpoints[11] = new Vector3(176, 10, -192);
        spawnpoints[12] = new Vector3(56, 10, -212);
        spawnpoints[13] = new Vector3(16, 10, -152);
        spawnpoints[14] = new Vector3(-83, 10, -192);
        spawnpoints[15] = new Vector3(-143, 10, -192);
        spawnpoints[16] = new Vector3(-163, 10, -152);
        ///------------Dist 5
        spawnpoints[17] = new Vector3(197, 10, 247);
        spawnpoints[18] = new Vector3(137, 10, 247);
        spawnpoints[19] = new Vector3(17, 10, 247);
        spawnpoints[20] = new Vector3(-102, 10, 227);
        spawnpoints[21] = new Vector3(-282, 10, 247);
        spawnpoints[22] = new Vector3(-262, 10, 147);
        ///------------Dist 6
        spawnpoints[23] = new Vector3(-102, 10, -332);
        spawnpoints[24] = new Vector3(-62, 10, -292);
        spawnpoints[25] = new Vector3(-362, 10, -192);
        spawnpoints[26] = new Vector3(-202, 10, -252);
        spawnpoints[27] = new Vector3(-222, 10, 7);
        spawnpoints[28] = new Vector3(-242, 10, 67);
        spawnpoints[29] = new Vector3(-222, 10, -92);
        spawnpoints[30] = new Vector3(-342, 10, -32);
    }


    //Obtiene un numero de distritos, un array de poblacion por distrito, unas coordenadas de spawnpoints, un numero de spawnpoints por distrito y un array de villagers (indica que spawnpoint usan)
    //Devuelve tanto un array de posiciones, como un array de enteros correspondientes al ID del spawnpoint de cada aldeano
    private int[] generarArrayCoord(int district, int[] population, Vector3[] spawnpoints, int[] spawnpoints_per_district, int [] spawnpoint_of_villager)   //En principio hay 7 distritos, X poblacion por distrito (10?), X spawnpoints por distrito (2-5?) y tantos spawnpoints_of_villager como villagers haya
    {
                                    //---------------------Parte que devuelve vec3--------------//
        int pop = 0;
        for(int i= 0; i < district; i++)
        {
            pop += population[district];
        }
        Vector3[] final_spawnpoints = new Vector3[pop];
                                    //---------------------Parte que devuelve vec3--------------//

        int aux = 0;
        int villagerAct = 0;
        for (int i = 0; i < district; i++)
        {
            for(int j = 0; j < population[i]; j++)
            {
                float r = Random.Range(aux, spawnpoints_per_district[i]);
                Mathf.Round(r);

                                    //---------------------Parte que devuelve vec3--------------//
                final_spawnpoints[villagerAct] = spawnpoints[(int)r];
                                    //---------------------Parte que devuelve vec3--------------//

                                    //---------------------Parte que devuelve ID--------------//
                spawnpoint_of_villager[villagerAct] = (int) r;
                                    //---------------------Parte que devuelve ID--------------//

            }
            aux += spawnpoints_per_district[i];
        }

        return spawnpoint_of_villager;
    }


}
