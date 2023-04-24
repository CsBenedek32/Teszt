using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer_changer : MonoBehaviour
{
    public Button stopButton, speed1Button, speed2Button, speed3Button;
    List<Button> buttons;
    public Color borderColor;

    private float timePassedSinceBeginning = 0;
    private float dayPassed = 0;
    public Text textBox;
    private DateTime initialDate = new DateTime(3045, 1, 1);
    private DateTime actDate;
    public float timePassesSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<Button> { stopButton, speed1Button, speed2Button, speed3Button };

        textBox.text = initialDate.ToString("yyyy/MM/dd");
        actDate = initialDate;
        ModifyOutline(speed1Button);


        stopButton.onClick.AddListener(() => {
            ResetButtonColor();
            if (timePassesSpeed == 0)
            {
                ModifyOutline(speed1Button);
                timePassesSpeed = 1;
            }
            else
            {
                ModifyOutline(stopButton);
                timePassesSpeed = 0;
            }
        });

        speed1Button.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(speed1Button);
            timePassesSpeed = 1;
        });

        speed2Button.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(speed2Button);
            timePassesSpeed = 2;
        });

        speed3Button.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(speed3Button);
            timePassesSpeed = 3;
        });
    }

    // Update is called once per frame
    void Update()
    {
        timePasses(timePassesSpeed);
    }

    void timePasses(float speed)
    {
        timePassedSinceBeginning += speed * Time.deltaTime;
        refreshDate(timePassedSinceBeginning);
    }

    void refreshDate(float time)
    {
        time = (int)time;
        dayPassed = time;
        actDate = initialDate.AddDays(dayPassed);
        textBox.text = actDate.ToString("yyyy/MM/dd");
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = borderColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
