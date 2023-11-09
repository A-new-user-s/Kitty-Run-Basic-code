using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform player;
    
    public Camera cams;
        
    void Awake()
    {
        Camera cam = cams;

        Rect rt = cam.rect;

        float scale_height = ((float)Screen.width / Screen.height) / ((float)1 / 2);
        float scale_width = 1f / scale_height;
        if (scale_height < 1)
        {
            //rt.height = scale_height;
            //rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            //rt.width = scale_width;
            //rt.x = (1f - scale_width) / 2f;
        }

        //cam.rect = rt;
    }
  
    void LateUpdate()
    {

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x + originalPos.x, y+originalPos.y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
