using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ejemplo
{
    public class Spawn : MonoBehaviour
    {
        public Spawn()
        {

        }

        public static int numAldeanos = 50;
        public static int numGuardiasA = 12;
        public static int grupoGuardias = 6;
        public GameObject Aldeano;
        public GameObject Guardia;
        private GameObject[] Aldeanos = new GameObject[numAldeanos];
        private GameObject[] Guardias = new GameObject[numGuardiasA];
        private Vector3[] posSpawnAldeanos = new Vector3[numAldeanos];
        private Vector3[] posSpawnGuardiasA = new Vector3[numGuardiasA];

        private static Vector3[] formacionGuardias = new Vector3[grupoGuardias];
        private static Vector3[] puestosGuardias = new Vector3[12];


        private static int districts = 6;
        private int[] villagers_per_district = new int[] { 8, 8, 8, 9, 8, 9};
        private int[] guardias_per_district = new int[] { 1, 1, 1, 1, 1, 1 };
        private static int totalSpawnpoints = 4 + 3 + 4 + 6 + 6 + 8;
        private static Vector3[] spawnpoints = new Vector3[totalSpawnpoints];
        private int[] spawnpoints_per_district = new int[districts] ;

        public static Vector3[] GetSpawnpoints () { return spawnpoints; }
        public static Vector3[] GetFormaciones() { return puestosGuardias; }

        private void Awake()
        {
            GestionarSpawnpoints();
            GestionarPuestosDeGuardia();
            posSpawnAldeanos = getPosiciones();
            posSpawnGuardiasA = getPosicionesGuardias();
            generarAldeanos();
            generarGuardias();
        }

        // Start is called before the first frame update
        void Start()
        {
            Globales.distritos[0] = false;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public GameObject[] getAldeanos()
        {
            return this.Aldeanos;
        }

        public Vector3[] getPosiciones()
        {
            /*for(int i = 0; i < numAldeanos; i++)
            {
                //Asignar posicion aquí, ya sea random o de la manera que lo gestionemos (por ejemplo creando pivotes fijos para que no se metan en la malla del NavMesh
                //y también tener spawnear por aldeanos por igual en todos los distritos
                //this.posSpawn[i] = new Vector3(37.57f, 10, -12.82f);
                this.posSpawn[i] = new Vector3(100, 0, 100);
            }
            return this.posSpawn;*/


            Vector3[] final_spawnpoints = new Vector3[numAldeanos];


            int spawnpoint_ini = 0;
            int villagerAct = 0;

            for (int i = 0; i < districts; i++)
            {
                for (int j = 0; j < villagers_per_district[i]; j++)
                {
                    float r = Random.Range(spawnpoint_ini, spawnpoints_per_district[i]);
                    Mathf.Round(r);

                    final_spawnpoints[villagerAct] = spawnpoints[(int)r];

                    villagerAct++;
                }

                spawnpoint_ini += spawnpoints_per_district[i];
            }


                return final_spawnpoints;
        }

        public Vector3[] getPosicionesGuardias()
        {
        
            Vector3[] final_spawnpoints = new Vector3[numGuardiasA];


            int spawnpoint_ini = 0;

            for (int i = 0; i < numGuardiasA; i++)
            {
                
                    float r = Random.Range(spawnpoint_ini, totalSpawnpoints);
                    Mathf.Round(r);

                    final_spawnpoints[i] = spawnpoints[(int)r];

            }


            return final_spawnpoints;
        }

        private void generarAldeanos()
        {
            /*
            Aldeanos[0] = this.Aldeano;
            for (int i = 1; i < numAldeanos; i++)
            {
                Aldeanos[i] = Instantiate(Aldeano);
                //Aldeanos[i].transform.position = new Vector3(100, Aldeanos[i].transform.position.y, 200);
                Aldeanos[i].transform.position = new Vector3(40, 0, 0);
                //Aquí sería Aldeanos[i].transform.position = this.posSpawn[i]
            }*/

        
            Aldeanos[0] = this.Aldeano;
            Aldeanos[0].transform.position = posSpawnAldeanos[0];


            Quaternion spawnRotation = Quaternion.identity;

            for (int i = 1; i < numAldeanos; i++)
            {
                Aldeanos[i] = Instantiate(Aldeano, posSpawnAldeanos[i], spawnRotation);
                //Aldeanos[i].transform.position = posSpawn[i];
            }

        }

        private void generarGuardias()
        {


            Guardias[0] = this.Guardia;
            Guardias[0].transform.position = posSpawnGuardiasA[0];


            Quaternion spawnRotation = Quaternion.identity;

            for (int i = 1; i < numGuardiasA; i++)
            {
                Guardias[i] = Instantiate(Guardia, posSpawnGuardiasA[i], spawnRotation);
                //Aldeanos[i].transform.position = posSpawn[i];
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
            spawnpoints[0] = new Vector3(40, 18.39f, -12);
            spawnpoints[1] = new Vector3(1, 18.39f, -88);
            spawnpoints[2] = new Vector3(-2, 18.39f, 47);
            spawnpoints[3] = new Vector3(-42, 18.39f, -12);
            ///------------Dist 2
            spawnpoints[4] = new Vector3(-122, 18.39f, -93);
            spawnpoints[5] = new Vector3(-142, 18.39f, -12);
            spawnpoints[6] = new Vector3(-122, 18.39f, 47);
            ///------------Dist 3
            spawnpoints[7] = new Vector3(-102, 18.39f, 87);
            spawnpoints[8] = new Vector3(17, 18.39f, 107);
            spawnpoints[9] = new Vector3(117, 18.39f, 107);
            spawnpoints[10] = new Vector3(200, 18.39f, 107);
            ///------------Dist 4
            spawnpoints[11] = new Vector3(176, 18.39f, -192);
            spawnpoints[12] = new Vector3(56, 18.39f, -212);
            spawnpoints[13] = new Vector3(16, 18.39f, -152);
            spawnpoints[14] = new Vector3(-83, 18.39f, -192);
            spawnpoints[15] = new Vector3(-143, 18.39f, -192);
            spawnpoints[16] = new Vector3(-163, 18.39f, -152);
            ///------------Dist 5
            spawnpoints[17] = new Vector3(197, 18.39f, 247);
            spawnpoints[18] = new Vector3(137, 18.39f, 247);
            spawnpoints[19] = new Vector3(17, 18.39f, 247);
            spawnpoints[20] = new Vector3(-102, 18.39f, 227);
            spawnpoints[21] = new Vector3(-282, 18.39f, 247);
            spawnpoints[22] = new Vector3(-262, 18.39f, 147);
            ///------------Dist 6
            spawnpoints[23] = new Vector3(-102, 18.39f, -332);
            spawnpoints[24] = new Vector3(-62, 18.39f, -292);
            spawnpoints[25] = new Vector3(-362, 18.39f, -192);
            spawnpoints[26] = new Vector3(-202, 18.39f, -252);
            spawnpoints[27] = new Vector3(-222, 18.39f, 7);
            spawnpoints[28] = new Vector3(-242, 18.39f, 67);
            spawnpoints[29] = new Vector3(-222, 18.39f, -92);
            spawnpoints[30] = new Vector3(-342, 18.39f, -32);
        }

        private void GestionarPuestosDeGuardia()
        {

            //Rellenamos el array de puestos de guardia
            puestosGuardias[0] = new Vector3(-162.4778f, 18.39f, -332.3943f);
            puestosGuardias[1] = new Vector3(57.57729f, 18.39f, 7.179901f);
            puestosGuardias[2] = new Vector3(57.57729f, 18.39f, -12.8201f);
            puestosGuardias[3] = new Vector3(-62.41888f, 18.39f, -12.8201f);
            puestosGuardias[4] = new Vector3(-182.4795f, 18.39f, 147.1627f);
            puestosGuardias[5] = new Vector3(37.52045f, 18.39f, 167.1627f);
            puestosGuardias[6] = new Vector3(217.5205f, 18.39f, 167.1632f);
            puestosGuardias[7] = new Vector3(-122.4793f, 18.39f, 67.16287f);
            puestosGuardias[8] = new Vector3(-143.1945f, 18.39f, -132.6317f);
            puestosGuardias[9] = new Vector3(57.52056f, 18.39f, 247.0628f);
            puestosGuardias[10] = new Vector3(-182.4783f, 18.39f, -152.8943f);
            puestosGuardias[11] = new Vector3(-282.479f, 18.39f, 87.10536f);

            //Rellenamos el array de puestos de formacion
            formacionGuardias[0] = new Vector3(-302.4785f, 18.39f, -112.8947f);
            formacionGuardias[1] = new Vector3(-302.4783f, 18.39f, -152.8947f);
            formacionGuardias[2] = new Vector3(-262.4783f, 18.39f, -152.8947f);
            formacionGuardias[3] = new Vector3(-282.4783f, 18.39f, -172.8946f);
            formacionGuardias[4] = new Vector3(-262.4785f, 18.39f, -112.8946f);
            formacionGuardias[5] = new Vector3(-282.4784f, 18.39f, -132.8946f);
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

}

