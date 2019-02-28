using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float dayLength;
    public float nightLength;
    public float changeLength;

    public float timeOfDay;

    public Color day;
    public Color night;
    public Light light;
    public float rotation;

    public Color ambientDay;
    public Color ambientNight;

    public float nightFog;

    public Light campfire;
    public float nightCampfireIntensity;

    void Start()
    {
    }

    void Update()
    {
        float time = Mathf.Repeat(Time.time, dayLength + nightLength);

        if(time < dayLength)
        {
            timeOfDay = 1;

            if (dayLength - changeLength < time)
            {
                timeOfDay = 1 - Mathf.Cos(Mathf.Abs(time - dayLength) / changeLength * 90 * Mathf.Deg2Rad);
            }
            else if (changeLength > time)
            {
                timeOfDay = 1 - Mathf.Cos(time / changeLength * 90 * Mathf.Deg2Rad);
            }
        }
        else
        {
            timeOfDay = 0;
        }

        RenderSettings.ambientSkyColor = timeOfDay * ambientDay + (1 - timeOfDay) * ambientNight;
        light.color = timeOfDay * day + (1 - timeOfDay) * night;
        RenderSettings.fogDensity = nightFog * (1 - timeOfDay);
        Vector3 rot = light.transform.localRotation.eulerAngles;
        rot.y = Mathf.Repeat(Time.time, dayLength + nightLength) / (dayLength + nightLength) * 360;
        light.transform.localRotation = Quaternion.Euler(rot);
        campfire.intensity = nightCampfireIntensity * (1 - timeOfDay);
    }
}
