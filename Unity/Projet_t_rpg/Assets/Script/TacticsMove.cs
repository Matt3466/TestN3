using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public bool turn = false;


    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    public Tile actualTargetTile;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile"); // permet d'entrer toutes les tuiles de la map

        halfHeight = GetComponent<Collider>().bounds.extents.y;// calcule la hauteur du personnage

        TurnManager.AddUnit(this);// fait entre le personnage dans son tour

    }
    public void GetCurrentTile() // fonction qui permet de définir que la tuile sur laquelle est le personnage est la tuile "currente"
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true; 
    }
    public Tile GetTargetTile(GameObject target)    //
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }
    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
       // tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target);
        }
    }
    public void FindSelectableTiles()
    {

        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave  as null

        while (process.Count >0)
        {

            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {


                foreach (Tile tile in t.adjacencyList)
                {

                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }
    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }

    }

    public void Move()
    {
        if (path.Count >0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            
            //Calculer la position des unitée en haut de la tuile ciblée
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target)>= 0.05f)
            {
                bool jump = transform.position.y != target.y;
                    if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizotalVelocity();
                }
                // Locomotion (metre animation ici)
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                // Tile center reached
                transform.position = target;
                path.Pop();  
            }

        }
        else
        {
            RemoveSelectableTiles();
            moving = false;


            TurnManager.EndTurn(); // ici le tour ce termine pour cette unité
        }
    }
    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }
        selectableTiles.Clear();
    }
    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }
    void SetHorizotalVelocity()
    {
        velocity = heading * moveSpeed;
    }
    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }
    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;

        target.y = transform.position.y;
        CalculateHeading(target);
        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;
            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }
    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;
        if (transform.position.y <= target.y)
        {
            fallingDown = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;
            velocity = new Vector3();
        }
    }
    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;
        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }
    void MoveToEdge() // pour marcher jusqu'au bord de la tuile avant de sauter
    {
        if (Vector3 .Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizotalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;
            velocity /= 4.0f;
            velocity.y = 1.5f;
        }
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];
        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;

            }
        }
        list.Remove(lowest);
        return lowest;
    }

    protected Tile FindEndTile(Tile t) // pour s'arreter sur la tuile adjacente à la cible ou pour aller le plus près possible s'il n'est pas possible de l'atteindre en un tour
    {
        Stack<Tile> tempPath = new Stack<Tile>();
        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }
        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }
        return endTile;
    }

    protected void FindPath(Tile target) // algorithme de "l'étoile" permet de calculer automatiquement le chemin que va prendr le pnj
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent = ??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }
            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //ne rien faire, déja traité
                }
                else if (openList.Contains(tile)) // choisir le meilleur chemin entre g et h
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                        
                    }
                }
                else
                {
                    tile.parent = t;
                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;
                    openList.Add(tile);

                }
            }


        }
        // to do - what do you do if there is no path to the target tile?
        // il peut y avoir des bug si cet algorithme ne trouve pas de cible ou si la cible est inateignable

        Debug.Log("Path not found");
    }
    public void BeginTurn()
    {
        turn = true;
    }
    public void EndTurn()
    {
        turn = false;
    }
}
