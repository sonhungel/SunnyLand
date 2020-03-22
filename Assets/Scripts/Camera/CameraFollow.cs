using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;   // biến dùng để lưu trữ tham chiếu  đến đối tượng player
    

    private Vector3 offset;     // biến dùng để lưu trữ khoảng cách bù giữa player và camera

    public float minX, maxX, minY, maxY;
    
    // Start is called before the first frame update
    void Start()
    {
        
        offset = transform.position - player.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = player.transform.position.x+3f;
        temp.y = player.transform.position.y;

        if (temp.x < minX)
        {
            temp.x = minX;
        }

        if (temp.x > maxX)
        {
            temp.x = maxX;
        }
        /*if(temp.x<200)
        {
            if (temp.y < minY)
            {
                temp.y = minY;
            }
            if (temp.y > maxY)
            {
                temp.y = maxY;
            }
        }
        else*/
        
            if (temp.y < minY)
            {
                temp.y = minY;
            }
            if (temp.y > 17)
            {
                temp.y = 17;
            }
        
        
        transform.position = temp;
       // transform.position = player.transform.position + offset;
    }
}
