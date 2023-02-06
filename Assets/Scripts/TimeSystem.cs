using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof (TextMeshPro))]
public class TimeSystem : MonoBehaviour
{
    public TextMeshProUGUI clock;
    public System.DateTime dateTime = new System.DateTime(2100, 1, 1);

    private float time = 0;

    private static readonly int NUM_OF_SECONDS_TO_IN_GAME_MINUTE = 1;

    public void Start()
    {
        StartCoroutine(InGameMinutesGenerator());
    }

    public void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator InGameMinutesGenerator()
    {
        while (true)
        {
            dateTime = dateTime.AddMinutes(1);
            clock.text = dateTime.ToString();
            yield return new WaitForSeconds(NUM_OF_SECONDS_TO_IN_GAME_MINUTE);
        }
    }
}
