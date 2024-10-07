using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyShader : MonoBehaviour
{
    public static int PlayerTransform = Shader.PropertyToID("_PlayerTransform");
    public Transform Character;
    private Material Material;
    void Start()
    {
        Material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Material.SetVector(PlayerTransform, Character.position);
    }
}
