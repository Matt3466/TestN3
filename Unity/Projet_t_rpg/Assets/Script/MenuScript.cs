using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript
{
    [MenuItem("Tools/Assign Tile Material")] // crée un menu dans la barre d'outil d'Unity
    public static void AssigneTileMaterial() // permet d'assigner un materiaux à tout les gameObject ayant le tage "Tile"
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");// permet de charger le bon materiaux, en l'occurence "Tile"
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }
    [MenuItem("Tools/Assign Tile Script")] 
    public static void AssignTile () //Permet d'assigner à tout les GameObject portant le Tag "Tile" le script "Tile" par contre il ne faut pas l'activer 2 fois si non, chaqu'un des GameObject aura le Script assigné deux fois
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }
}
