using UnityEngine;
using UnityEngine.Rendering;
[ExecuteAlways]
public class GlobeManager : MonoBehaviour
{
    private void Awake()
    {
        if (Application.isPlaying)
            Shader.EnableKeyword("_GLOBE_ENABLED");
        else
            Shader.DisableKeyword("_GLOBE_ENABLED");
    }
}
