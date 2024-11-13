using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public static Ticker Instance;
    public delegate void TickEvent();

    public event TickEvent OnTick;

    public float tickInterval;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(ExecuteTick());
        
    }
    public IEnumerator ExecuteTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);
            OnTick?.Invoke();
        }
    }

}
