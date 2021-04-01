using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// transporta dados para diferentes tipos de classe
[System.Serializable]

public struct Link
{
    //Ida e volta = BI, Uni = ida
    public enum direction {UNI, BI}
    //ligação de ponto a ponto
    public GameObject node1;
    public GameObject node2;
    //Direção
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    // deixa publico um lugar para colocar oq voce define como waypoint
    // public Transform[] waypoints;

    //Array de waypoints
    public GameObject[] waypoints;
    // Array de informações
    public Link[] links;
    // Arquivos da pasta Graph
    public Graph graph = new Graph();

    void Start()
    {
        // se a quantidade de waypoint é maior que 0 ...
        if(waypoints.Length > 0)
        {
            //É um loop do Array de waypoints
            foreach(GameObject wp in waypoints)
            {
                // reabastece seu waypoint
                graph.AddNode(wp);
            }
            //liga os waypoint
            foreach(Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                // se ele for  BI ele faz o caminho inverso
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    void Update()
    {
        //Traçar a linha
        graph.debugDraw(); 
    }
}
