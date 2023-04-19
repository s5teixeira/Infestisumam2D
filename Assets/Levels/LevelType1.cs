using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;




public class LevelType1 : MonoBehaviour
{

    [SerializeField] public GameObject LevelMesh;
    [SerializeField] public Camera camera;
    [Header("WARNING! I DONT KNOW HOW TO EXTRACT WIDTH AND HEIGHT FROM SUPERTILED2UNITY, PLEASE ENTER THE VALUES MANUALLY!")]

    [SerializeField] float mapWidth;
    [SerializeField] float mapHeight;

    [SerializeField] GameObject northernDoor;
    [SerializeField] GameObject eastDoor;
    [SerializeField] GameObject southDoor;
    [SerializeField] GameObject westDoor;


    // Start is called before the first frame update
    void Start()
    {
        camera.transform.position = new Vector3(0, 0, -4);
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
