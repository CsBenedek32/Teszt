using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    public Timer_changer tc;
    
    public class OnTickEventArgs: EventArgs
    {
        public int tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;
    public float TickTimerMax = 1f;

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;

    }

    private void Update()
    {
        if(tc.timePassesSpeed > 0) {
            TickTimerMax = 1f / tc.timePassesSpeed;
        
        tickTimer += Time.deltaTime;
        if(tickTimer >= TickTimerMax) {
            tickTimer -= TickTimerMax;
            tick++;
            if (OnTick != null) { OnTick(this, new OnTickEventArgs { tick = tick }); }

        }
        }
    }
}
