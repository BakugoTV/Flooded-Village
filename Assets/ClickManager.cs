using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {
    public GameObject empty;
    public char[,] CharGrid;
    public Transform prefabParent;
    public GameObject sand;
    public GameObject water;
    public GameObject rock;
    // Use this for initialization
    void Start() {
        CharGrid = new char[5, 10]{
        {'r', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'r'},
        {'r', 'e', 's', 'e', 'e', 's', 's', 's', 'e', 'r'},
        {'r', 'e', 'e', 'e', 'e', 'e', 's', 'e', 'e', 'r'},
        {'r', 's', 's', 'e', 's', 's', 'e', 's', 's', 'r'},
        {'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r'}};
        generationPrefab();
        propagation();
        //generationPrefab();
    }

    // Update is called once per frame
    void Update() {
        //Deb0ug.DrawLine(Camera.main.transform.position, new Vector3(0, 0, 1000000), Color.red);
        //Debug.DrawRay(Camera.main.transform.position, Vector3.forward * 1000, Color.red, 10f);
        Vector3 vector = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(vector);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 10f);
        RaycastHit2D raycast = Physics2D.GetRayIntersection(ray);
        if (Input.GetMouseButtonDown(0))
        {

            if ((raycast.collider != null) && (raycast.collider.CompareTag("empty")))
            {

                Vector2 pos = raycast.collider.transform.position;
                Destroy(raycast.collider.gameObject);
                Instantiate(sand, pos, Quaternion.identity);
            }
            if ((raycast.collider != null) && (raycast.collider.CompareTag("sand")))
            {

                Vector2 pos = raycast.collider.transform.position;
                Destroy(raycast.collider.gameObject);
                Instantiate(empty, pos, Quaternion.identity);
            }
        }
    }
    private void propagation()
    {
        
        //[sand = s, water = w, empty = e];
        for (int x = 0; x < 4; x++)
        {
            for (int y = 1; y < 9; y++)
            {
                if ((CharGrid[x, y] == 'e') 
                    && (CharGrid[x + 1, y] == 'w' 
                    | CharGrid[x - 1, y] == 'w' 
                    | CharGrid[x, y + 1] == 'w' 
                    | CharGrid[x, y - 1] == 'w'))
                {
                    CharGrid[x, y] = 'w';
                    Debug.Log("propagation");
                    propagation();
                }
            }
        }
    }
    private void generationPrefab()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if(CharGrid[x, y] == 'e')
                {
                    Instantiate(empty, new Vector3(y, -x), Quaternion.identity, prefabParent);
                }
                if (CharGrid[x, y] == 's')
                {
                    Instantiate(sand, new Vector3(y, -x), Quaternion.identity, prefabParent);
                }
                if (CharGrid[x, y] == 'w')
                {
                    Instantiate(water, new Vector3(y, -x), Quaternion.identity, prefabParent);
                }
                if (CharGrid[x, y] == 'r')
                {
                    Instantiate(rock, new Vector3(y, -x), Quaternion.identity, prefabParent);
                }
            }
        }
    }
}
