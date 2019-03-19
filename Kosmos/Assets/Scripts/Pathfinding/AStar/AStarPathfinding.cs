using Kosmos.Pathfinding.AStar;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    private static AStarPathfinding singleton;

    public static AStarPathfinding Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = FindObjectOfType<AStarPathfinding>();
            }

            return singleton;
        }
    }

    private Node[] tiles;
    private Node target;
    public float neighbourDistance = 1;

    private void Start()
    {
        tiles = FindObjectsOfType<Node>();

        //Asigna automaticamente los nodos vecinos de cada Tile.
        foreach (Node n in tiles)
        {
            n.neighbours = new List<Node>();

            foreach (Node neighbour in tiles)
            {
                if(n == neighbour)
                {
                    continue;
                }

                float dist = Vector3.Distance(n.transform.position, neighbour.transform.position);

                if (dist <= neighbourDistance)
                {
                    n.neighbours.Add(neighbour);
                }
            }
        }
    }

    public Node[] GetTiles()
    {
        return tiles;
    }

    public List<Node> GetPath(Node start, Node end)
    {
        this.target = end;

        var openSet = new List<Node>();
        var closedSet = new List<Node>();
        var gScore = new Dictionary<Node, float>();
        var fScore = new Dictionary<Node, float>();
        var parent = new Dictionary<Node, Node>();

        //Distancia desde start al nodo actual
        gScore.Add(start, 0);

        //Distancia total (desde start a end)
        fScore.Add(start, HValue(start));

        openSet.Add(start);

        parent[start] = null;
        
        while (openSet.Count > 0)
        {
            //Encontrar el nodo con menor fScore en el openSet
            float min = Mathf.Infinity;
            Node current = null;

            for (int i = 0; i < openSet.Count; i++)
            {
                if (fScore[openSet[i]] < min)
                {
                    min = fScore[openSet[i]];
                    current = openSet[i];
                }
            }

            if (current == end)
            {
                return BuildPath(parent);
            }
            
            //Mover el nodo seleccionado al closedSet
            openSet.Remove(current);
            closedSet.Add(current);

            //Recorrer los vecinos
            for (int i = 0; i < current.neighbours.Count; i++)
            {
                var n = current.neighbours[i];
 
                //Si esta en el closedSet, se ignora
                if (closedSet.Contains(n))
                {
                    continue;
                }

                //Añadir al openSet, verificar que este para evitar duplicados
                if (!openSet.Contains(n))
                {
                    openSet.Add(n);

                    //Inicializar diccionario
                    gScore.Add(n, Mathf.Infinity);
                    fScore.Add(n, Mathf.Infinity);
                }
                
                //gScore[current] es la distance desde start a current
                float tentativeGScore = gScore[current] + GValue(current, n);

                //Revisar si esta solucion es mejor que la actual
                if (tentativeGScore >= gScore[n])
                {
                    continue;
                }

                if (parent.ContainsKey(n))
                {
                    parent[n] = current;
                }
                else
                {
                    parent.Add(n, current);
                }

                gScore[n] = tentativeGScore;
                fScore[n] = gScore[n] + HValue(n);
            }
        }

        return null;
    }

    //Valor entre dos nodos.
    public float HValue(Node n)
    {
        return Vector3.Distance(n.transform.position, target.transform.position);
    }

    //Valor entre un nodo y el start
    public float GValue(Node start, Node end)
    {
        return Vector3.Distance(start.transform.position, end.transform.position);
    }

    private List<Node> BuildPath(Dictionary<Node, Node> parent)
    {
        //Reconstrucción del camino
        List<Node> path = new List<Node>();
        var currNode = target;

        while (parent[currNode] != null)
        {
            path.Add(currNode);
            currNode = parent[currNode];
        }

        path.Reverse();

        return path;
    }
}
