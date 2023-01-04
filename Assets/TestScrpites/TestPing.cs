using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PingConnect());
    }

    IEnumerator PingConnect()
    {
        int pingCount = 0;
        Ping ping = new Ping("43.156.20.190");
        while (!ping.isDone)
        { 
            yield return new WaitForSeconds(0.1f);
            pingCount++;
        }
        Debug.LogError($"Ping ip:{ping.ip}, ping time:{ping.time},ping count:{pingCount}");
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
