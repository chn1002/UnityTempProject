using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilitySystem  {
    List<int> pItem;

    public ProbabilitySystem(List<int> item)
    {
        pItem = item;
    }

    public int RandomIndexWithProbability()
    {
        int sum = 0;
        foreach (int item in pItem)
        {
            sum += item;
        }

        float rand = Random.Range(0, sum);

        int current = 0;
        for (int i = 0; i < pItem.Count; i++)
        {
            current += pItem[i];
            if (rand < current)
            {
                return i;
            }
        }

        return 0;
    }

}
