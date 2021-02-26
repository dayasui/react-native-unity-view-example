using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class Rotate : MonoBehaviour
{

    private bool canRotate = true;

    // Use this for initialization
    void Awake()
    {
        UnityMessageManager.Instance.OnRNMessage += onMessage;
    }

    void onDestroy()
    {
        UnityMessageManager.Instance.OnRNMessage -= onMessage;
    }

    void onMessage(MessageHandler message)
    {
        var data = message.getData<string>();
        Debug.Log("onMessage:" + data);
        canRotate = !canRotate;
        UnityMessageManager.Instance.SendMessageToRN("Now it is " + (canRotate ? "": "not ") + "rotating");
        message.send(new { CallbackTest = "I am Unity callback" });
    }

    void OnMouseDown()
    {
        Debug.Log("click");
        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
        {
            name = "click",
            callBack = (data) =>
            {
                Debug.Log("onClickCallBack:" + data);
            }
        });
    }

    public void GetInitValue() {
        JObject jObj = new JObject();
        
        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
        {
            name = "getInitValue",
            callBack = (data) =>
            {
                Debug.Log("onClickCallBack:" + data);
            }
        });
    }

    public void GetSession() {
        UnityMessageManager.Instance.SendMessageToRN(new UnityMessage()
        {
            name = "getSession",
            callBack = (data) =>
            {
                Debug.Log("onClickCallBack:" + data);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!canRotate)
        {
            return;
        }
        var delta = 30 * Time.deltaTime;
        transform.localRotation *= Quaternion.Euler(delta, delta, delta);
    }
}