using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Skills : MonoBehaviour
{
    //Classes & script references
    public Movement p_movement;



    //Field variables
    public float max_Health = 100f;
    public float user_Energy;
    public float user_Health;


    void Start()
    {
        
    }

    void Update()
    {
        g_player_Energy();
    }


    // ------------------------------------------------------------  (getters/setters) SKILLS [START] ---------------------------------------------------------------------------
    public float g_player_Energy()
    {
        user_Energy = p_movement.getEnergy();

        return user_Energy;

    }

    public float g_user_Health()
    {
        return user_Health;
    }

    public void set_user_Health(float x)
    {
        user_Health = x;
    }

    public void set_user_Energy(float x)
    {
        user_Energy = x;
    }

    // ------------------------------------------------------------  (getters/setters) SKILLS [END] ---------------------------------------------------------------------------



}
