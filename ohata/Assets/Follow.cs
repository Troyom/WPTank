using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    //Ojetivo
    Transform goal;

    //Velocidade
    public float speed = 5.0f;
    
    //Precisão
    public float accuracy = 1.0f;
   
    //Rotação
    public float rotSpeed = 2.0f;

    // Indentificar o wpmanager
    public GameObject wpManager;
    
    //Index
    GameObject[] wps;
    
    //Nos
    GameObject currentNode;
    
    //Origem
    int currentWP = 0;
    
    //Graph
    Graph g;

    void Start()
    {
        //Chama componentes
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    //Leva parao heliporto
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    //Leva para a Ruina
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        //Caminhada
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //Muda para o ponto mais proximo
        currentNode = g.getPathPoint(currentWP);

        //Muda para o próximo 
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }

        // faz a movimentação para outro sentido, mirando em outro ponto
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
