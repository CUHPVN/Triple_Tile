using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance {  get; private set; }
    public string time="Full";
    public System.DateTime lateTime;
    public int elapsedTimes;
    public int health=0;
    public int maxHealth=5;
    public int timePHealth= 300;
    void Awake()
    {
        Instance= this;
        if (health < maxHealth)
        {
            lateTime = System.DateTime.Now;
        }
    }

    void Update()
    {
        UpdateTime();
    }
    public void DecHealth()
    {
        GameManager.Instance.SetWin(true);
        if (health == maxHealth)
        {
            lateTime = System.DateTime.Now;
        }
        if (health > 0)
        health--;
    }
    public void UpdateTime()
    {
        
        if(lateTime != default)
        {
            if (health < maxHealth) elapsedTimes = (int)(System.DateTime.Now - lateTime).TotalSeconds;
            else elapsedTimes = 0;
            {
                if (elapsedTimes > timePHealth)
                {
                    health++;
                    health = Mathf.Min(health, maxHealth);
                    if (health == maxHealth) lateTime = default;
                    else lateTime = System.DateTime.Now;
                }
                if (health < maxHealth)
                {
                    time =  ((timePHealth - elapsedTimes) / 60).ToString()+ ":" + ((timePHealth - elapsedTimes)%60).ToString();
                }
                else
                {
                    time = "Full";
                }
            }
        }
    }
    public void Calculate(System.DateTime saveTime)
    {
        if (health == maxHealth) saveTime = System.DateTime.Now;
        double elapsedTime = (System.DateTime.Now - saveTime).TotalSeconds;
        elapsedTime += elapsedTimes;
        health += (int)(elapsedTime/timePHealth);
        health = Mathf.Min(health, maxHealth);
        lateTime = System.DateTime.Now.AddSeconds(-(elapsedTime%timePHealth));
    }
}
