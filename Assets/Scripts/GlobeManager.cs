using UnityEngine;
using UnityEngine.Rendering;

public class GlobeManager : MonoBehaviour
{
    private void Start()
    {
        Shader.EnableKeyword("_GLOBE_ENABLED");
    }
}
