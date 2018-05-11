using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    public bool is_AI = true;
    public float total_hp = 1000.0f;
    public float soldier_melee_creation_time = 5.0f;
    private float soldier_melee_creation_timer = 0.0f;
    public GameObject melee_soldier;
    public GameObject ranged_soldier;
    public GameObject cavalry_soldier;

    [Header("Current HP")]
    public float hp = 1000.0f;

    [Header("Melee Stats")]
    public float hp_melee = 100.0f;
    public float attack_melee = 25.0f;
    public float attack_distance_melee = 1.25f;
    public float attack_speed_melee = 1.0f;
    public float speed_melee = 1.2f;
    public float waiting_distance_melee = 1.0f;

    [Header("Distance Stats")]
    public float hp_ranged = 80.0f;
    public float attack_ranged = 25.0f;
    public float attack_distance_ranged = 2.5f;
    public float attack_speed_ranged = 1.2f;
    public float speed_ranged = 1.0f;
    public float waiting_distance_ranged = 1.0f;

    [Header("Cavalry Stats")]
    public float hp_cavalry = 200.0f;
    public float attack_cavalry = 40.0f;
    public float attack_distance_cavalry = 1.5f;
    public float attack_speed_cavalry = 0.8f;
    public float speed_cavalry = 1.8f;
    public float waiting_distance_cavalry = 1.0f;

    void Start()
    {
        hp = total_hp;
        soldier_melee_creation_timer = 0.0f;
    }

    void Update()
    {
        if (is_AI)
        {
            soldier_melee_creation_timer += Time.deltaTime;
            if (soldier_melee_creation_timer >= soldier_melee_creation_time)
            {
                soldier_melee_creation_timer = 0.0f;
                GameObject copy = Instantiate(melee_soldier, null);
				copy.SetActive(true);
                copy.GetComponent<SoldiersManager>().enabled = true;
                copy.transform.position = gameObject.transform.position;
                SetStates(copy, PlayerBaseController.SOLDIER_TYPE.S_MELEE);
            }
        }
    }

    public void ApplyDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            if (is_AI)
                Defeat();
            else
                Victory();
        }
    }

    public void SetStates(GameObject soldier, PlayerBaseController.SOLDIER_TYPE soldier_type)
    {
        switch (soldier_type)
        {
            case PlayerBaseController.SOLDIER_TYPE.S_NONE:
                break;
            case PlayerBaseController.SOLDIER_TYPE.S_MELEE:
                soldier.GetComponent<SoldiersManager>().type = soldier_type;
                soldier.GetComponent<SoldiersManager>().hp = hp_melee;
                soldier.GetComponent<SoldiersManager>().attack = attack_melee;
                soldier.GetComponent<SoldiersManager>().attack_distance = attack_distance_melee;
                soldier.GetComponent<SoldiersManager>().attack_speed = attack_speed_melee;
                soldier.GetComponent<SoldiersManager>().speed = speed_melee;
                soldier.GetComponent<SoldiersManager>().waiting_distance = waiting_distance_melee;
                break;

            case PlayerBaseController.SOLDIER_TYPE.S_RANGED:
                soldier.GetComponent<SoldiersManager>().type = soldier_type;
                soldier.GetComponent<SoldiersManager>().hp = hp_ranged;
                soldier.GetComponent<SoldiersManager>().attack = attack_ranged;
                soldier.GetComponent<SoldiersManager>().attack_distance = attack_distance_ranged;
                soldier.GetComponent<SoldiersManager>().attack_speed = attack_speed_ranged;
                soldier.GetComponent<SoldiersManager>().speed = speed_ranged;
                soldier.GetComponent<SoldiersManager>().waiting_distance = waiting_distance_ranged;
                break;

            case PlayerBaseController.SOLDIER_TYPE.S_CAVALRY:
                soldier.GetComponent<SoldiersManager>().type = soldier_type;
                soldier.GetComponent<SoldiersManager>().hp = hp_cavalry;
                soldier.GetComponent<SoldiersManager>().attack = attack_cavalry;
                soldier.GetComponent<SoldiersManager>().attack_distance = attack_distance_cavalry;
                soldier.GetComponent<SoldiersManager>().attack_speed = attack_speed_cavalry;
                soldier.GetComponent<SoldiersManager>().speed = speed_cavalry;
                soldier.GetComponent<SoldiersManager>().waiting_distance = waiting_distance_cavalry;
                break;

            default:
                break;
        }
    }

    public void Defeat()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void Victory()
    {
        SceneManager.LoadScene("WinScene");
    }
}