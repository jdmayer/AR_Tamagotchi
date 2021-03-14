using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>
public class SkinChanger : MonoBehaviour
{
    public Material materialRed;
    public Material materialBlue;
    public Material materialGreen;
    public Material materialPink;
    public Material materialYellow;

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetRed()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materialRed;
    }
    public void SetBlue()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materialBlue;
    }
    public void SetGreen()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materialGreen;
    }
    public void SetPink()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materialPink;
    }
    public void SetYellow()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materialYellow;
    }

}
