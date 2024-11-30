using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    [Header("击破值")]public int i_maxSG = 100; 
    [Header("是否被击破")]public bool b_isbreak = false; 
}