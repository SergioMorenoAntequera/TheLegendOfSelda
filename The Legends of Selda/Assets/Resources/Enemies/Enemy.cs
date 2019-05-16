using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp;
    private string type;

    public Enemy(int hp, string type)
    {
        this.hp = hp;
        this.type = type;
    }
}
