using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;




public class LevelType1 : MonoBehaviour
{

    [SerializeField] public GameObject LevelMesh;
    [SerializeField] public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        float targetAspectRatio = (float)Screen.width / (float)Screen.height;
        //LevelMesh.transform.localScale = new Vector2();
        float widthScale = (float)Screen.width/50.0f;
        float heightScale = (float)Screen.height / 25.0f;


        LevelMesh.transform.localScale = new Vector2(widthScale, heightScale);

        camera.transform.position = new Vector2(0, 0);
        Vector3 cameraCenter = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 1));
        LevelMesh.transform.position = cameraCenter;

        var width = camera.orthographicSize * 2.0 * Screen.width / Screen.height;
        //var viewRect = camera.ScreenToWorldPoint(camera.rect);
        Debug.Log(camera.rect);





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
