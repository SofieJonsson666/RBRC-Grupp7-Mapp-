using UnityEngine;

public class BackgroundMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material material;

    private Color startingColor = Color.white;

    private float hueNumber = 0;

    public float hueSpeed = 1;

    private void Start()
    {
        startingColor = Color.HSVToRGB(0, 0.38f, 0.87f);
    }

    private void Update()
    {
        hueNumber += DataSaver.instance.mps * Time.deltaTime * hueSpeed;
        if (hueNumber > 360)
        {
            hueNumber = 0;
        }
        material.color = Color.HSVToRGB(hueNumber / 360, 0.38f, 0.87f);

        //if (DataSaver.instance.metersTraveled < 50)
        //{
        //    material.color = startingColor;
        //}
        //else
        //{
        //    material.color = Color.HSVToRGB(1, 1, 1);
        //}
    }
}