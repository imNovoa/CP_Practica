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
        }
        return this.posSpawn;
    }

    private void generarAldeanos()
    {
        Aldeanos[0] = this.Aldeano;
        for (int i = 1; i < numAldeanos; i++)
        {
            Aldeanos[i] = Instantiate(Aldeano);
            Aldeanos[i].transform.position = new Vector3(100, Aldeanos[i].transform.position.y, 200);
            //Aquí sería Aldeanos[i].transform.position = this.posSpawn[i]
        }
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
