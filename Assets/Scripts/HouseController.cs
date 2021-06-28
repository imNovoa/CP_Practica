using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HouseController : MonoBehaviour
{
    protected GameObject door, spawn1, spawn2, spawn3, spawn4;

    private int houseCapacity = 4;
    private bool[] occupied = new bool[4];
    private Vector3[] roomPos = new Vector3[4];
    private Quaternion[] Roomrotations = new Quaternion[4]; //Rotación que tendrá el personaje dentro de cada posición 


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<= houseCapacity - 1; i++)
        {
            this.occupied[i] = false;
        }

        //-------Se asignan las posiciones--------------//
        this.roomPos[0] = new Vector3(0.0f, 0.0f, 0.0f);
        this.roomPos[1] = new Vector3(1.0f, 0.0f, 0.0f);
        this.roomPos[2] = new Vector3(1.0f, 1.0f, 0.0f);
        this.roomPos[3] = new Vector3(0.0f, 1.0f, 0.0f);

        //-------Se asignan las rotaciones--------------//
        this.Roomrotations[0] = new Quaternion (0.0f, 0.0f, 0.0f, 0.0f);
        this.Roomrotations[1] = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        this.Roomrotations[2] = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        this.Roomrotations[3] = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //------------------------Devuelve -1 si no hay posiciones libres. Si las hay, devuelve su ID---------//
    public int CheckForRooms()
    {
        int aux = -1;
        for (int i = 0; i <= houseCapacity - 1; i++)
        {
            if(this.occupied[i] == false)
            {
                aux = i;
            }
        }

        return aux;
    }

    //------------------------Asigna la posición, rotación y movimiento de una habitación libre a un Game Object villager---------//
    //------------------Devuelve un bool en función de si se ha realizado la operación--------------//
    public bool GiveRoom(GameObject villager)
    {
        bool aux = false;
        int room = CheckForRooms();
        if (room != -1)
        {
            villager.transform.SetPositionAndRotation(roomPos[room], Roomrotations[room]);
            //DEJAR QUIETO EN LA POSICIÓN, NO SÉ COMO ACCEDER AL GAMEOBJECT.MOVEMENT
            aux = true;
        }
        return aux;
    }
}
