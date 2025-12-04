using UnityEngine;

public class EmotionDisplay : MonoBehaviour
{
    public EmotionReceiver receiver; 
    
    private Material targetMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            targetMaterial = renderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (receiver == null) return;

        // 1. 获取最新数据
        EmotionData data = receiver.GetEmotion();

        if (data != null && data.hasFace)
        {
            // 2. 根据 Valence (愉悦度) 改变颜色
            // Valence > 0 (开心) -> 红色
            // Valence < 0 (难过) -> 蓝色
            if (targetMaterial != null)
            {
                if (data.valence > 0.2f)
                    targetMaterial.color = Color.Lerp(targetMaterial.color, Color.red, Time.deltaTime * 5);
                else if (data.valence < -0.2f)
                    targetMaterial.color = Color.Lerp(targetMaterial.color, Color.blue, Time.deltaTime * 5);
                else
                    targetMaterial.color = Color.Lerp(targetMaterial.color, Color.white, Time.deltaTime * 5);
            }
            
            // Debug.Log($"Unity 收到: {data.label} ({data.valence})");
        }
    }
}
