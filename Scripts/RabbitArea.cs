using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class RabbitArea : Area
{
    public RabbitAgent rabbitAgent;
    public TextMeshPro cumulativeRewardText;
    public GameObject egg;
    public Egg eggPrefab;
    public GameObject ovira1;
    public GameObject ovira2;
    public GameObject ovira3;
    public TextMeshPro stevecJajcText;

    [HideInInspector]
    public float obsticales = 0f;

    [HideInInspector]
    public float egg_Number = 0f;

    [HideInInspector]
    public List<GameObject> eggList;
    
    public override void ResetArea()
    {
        RemoveAllEggs();

        if (obsticales == 1.0f)
            ovira1.SetActive(true);
        if (obsticales == 2.0f)
            ovira2.SetActive(true);
        if (obsticales == 3.0f)
            ovira3.SetActive(true);

        PlaceRabbit();
        SpawnEgg(egg_Number);
    }

    public void RemoveSpecificEgg(GameObject eggObject)
    {
        eggList.Remove(eggObject);
        Destroy(eggObject);
        if (eggList.Count == 0)
        {
            rabbitAgent.SetReward(2f);
            rabbitAgent.Done();
        }
        else
        {
            rabbitAgent.SetReward(1f);
            Debug.Log("Elementi v listu = " + eggList.Count);
        }
    }

    private void RemoveAllEggs()
    {
        if (eggList != null)
        {
            for (int i = 0; i < eggList.Count; i++)
            {
                if (eggList[i] != null)
                {
                    Destroy(eggList[i]);
                }
            }
        }

        eggList = new List<GameObject>();
    }

    private void PlaceRabbit()
    {
        rabbitAgent.transform.position = new Vector3(Random.Range(-19f, 19f), 0f, Random.Range(-19f, 19f)) + Vector3.up * 0.2f; // Postavim omejitve kje se lahko postavi v okolju
        rabbitAgent.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);  // rotira se le okoli svoje osi y-(360)
    }


    public void SpawnEgg(float count)
    {
        for (float i = 0; i < count; i++)
        {
            GameObject eggObject = Instantiate<GameObject>(eggPrefab.gameObject);
            eggObject.transform.position = new Vector3(Random.Range(-19f, 19f), 0f, Random.Range(-19f, 19f)) + Vector3.up * 0.2f; // Postavim omejitve kje se lahko postavi v okolju
            eggObject.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); // rotira se le okoli svoje osi y-(360)
            eggObject.transform.parent = transform;
            eggList.Add(eggObject);
        }
    }

    private void Update() // shranjevanje rewarda v spremenljivko. Ta vrednost se izpisuje v okolju preko TextMeshPro
    {
        cumulativeRewardText.text = rabbitAgent.GetCumulativeReward().ToString("0.00");
        stevecJajcText.text = rabbitAgent.Stevec_jajc.ToString("0");
    }

}
