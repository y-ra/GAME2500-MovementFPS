using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamagePopUp : MonoBehaviour
{
    public static EnemyDamagePopUp current;
    public GameObject popUpPrefab;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            CreatePopUp(Vector3.one, Random.Range(0, 1000).ToString(), Color.yellow);
        }
    }

    public void CreatePopUp(Vector3 position, string text, Color color)
    {
        var popup = Instantiate(popUpPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        //Destroy Timer
        Destroy(popup, 1f);
    }
}
