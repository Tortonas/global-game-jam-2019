using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMesh : MonoBehaviour
{
    public Mesh[] meshes;
    public GameObject prefab;
    GameObject mesh;
    TreeScript script;
    int randomIndex = 0;
    public bool CheckDown = false;
    // Start is called before the first frame update
    void Start()
    {
        randomIndex = Random.Range(0, meshes.Length);
        GameObject obj = prefab;
        mesh = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        mesh.GetComponent<MeshFilter>().mesh = meshes[randomIndex];
        if (CheckDown)
        {
            script = gameObject.GetComponent<TreeScript>();
            script.tempMesh = meshes[randomIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
