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
}
