using Kosmos.Pathfinding.AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPlayer : MonoBehaviour
{
    //Movement
    private List<Node> path;
    private int pathIndex;
    private bool isMoving = false;
    private Rigidbody rigidBody;
    public float speed = 50;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void MoveToTarget(Node dest)
    {
        Node currNode = null;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            currNode = hit.transform.GetComponent<Node>();
        }

        if (currNode != null)
        {
            path = AStarPathfinding.Singleton.GetPath(currNode, dest);

            for (var i = 0; i < path.Count; i++)
            {
                Tile t = path[i].GetComponent<Tile>();
                t.SetState(Tile.TileState.Path);
            }

            isMoving = true;
            pathIndex = 0;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            //Termino del desplazamiento
            if(pathIndex >= path.Count)
            {
                isMoving = false;
            }

            //Siguiente objetivo
            Node currTarget = path[pathIndex];

            //Punto a moverse
            Vector3 destination = currTarget.transform.position;
            destination.y = transform.position.y;

            //Movimiento
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.fixedDeltaTime);

            //Verificacion de que se alcanzo el destino
            float dist = Vector3.Distance(transform.position, destination);

            if (dist <= 0)
            {
                pathIndex++;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out hit))
            {
                Node destNode = hit.transform.GetComponent<Node>();

                if (destNode != null)
                {
                    Node[] nodes = AStarPathfinding.Singleton.GetTiles();

                    foreach (Node n in nodes)
                    {
                        Tile currTile = n.GetComponent<Tile>();
                        currTile.SetState(Tile.TileState.Idle);
                    }

                    Tile tile = destNode.GetComponent<Tile>();
                    tile.SetState(Tile.TileState.Destination);

                    MoveToTarget(destNode);
                }
                else
                {
                    Debug.Log("Node not found");
                }
            }
        }
    }
}
