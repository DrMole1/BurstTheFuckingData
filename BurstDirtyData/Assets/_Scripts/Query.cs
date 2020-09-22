using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Query
{
    public string QueryText;
    public List<Condition> conditions;
    public float Duration;
    public int Limit;

    public Query(string query, Condition[] conditions, float duration = 0.0f, int limit = 0)
    {
        this.QueryText = query;
        this.conditions = new List<Condition>();
        this.Duration = duration;
        this.Limit = limit;

        if(conditions != null && conditions.Length > 0)
        {
            foreach(Condition c in conditions)
            {
                this.conditions.Add(c);
            }
        }
    }
}
