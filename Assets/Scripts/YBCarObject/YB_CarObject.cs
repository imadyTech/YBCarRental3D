using imady.NebuEvent;
using imady.NebuUI;
using UnityEngine;
using Newtonsoft.Json;
using YBCarRental3D;

public class YB_CarObject : NebuEventUnityObjectBase,
    INebuObserver<NebuDataMessage<string>>,
    INebuObserver<YBCarSelectedMessage>,
    INebuObserver<YBCarDeSelectedMessage>
{
    public Light LeftLight;
    public Light RightLight;
    public Vector3 startPosition;
    public Quaternion startRotation;
    public Vector3 demoPosition = new Vector3(1000f, 1000f, 1000f);
    public string unityModelName = "default";


    public bool isRotating = false;
    public Vector3 rotation = new Vector3(0,0.5f, 0);


    protected override void Awake()
    {
        startRotation = this.transform.rotation;
        startPosition = this.transform.position;
    }
    public void Update()
    {
        if (isRotating)
            this.transform.Rotate(rotation);
    }


    public void OnNext(NebuDataMessage<string> message)
    {
        try
        {
            var lights = this.gameObject.GetComponentsInChildren<Light>();
            foreach (var light in lights)
                light.intensity = 0.5f;

        }
        catch { }
    }


    public void OnNext(YBCarSelectedMessage message)
    {
        if (this.unityModelName.ToLower() == message.messageBody.UnityModelName.ToLower())
        {
            this.transform.position = demoPosition;
            isRotating = true;
        }
    }
    public void OnNext(YBCarDeSelectedMessage message)
    {
        this.isRotating = false;
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
}