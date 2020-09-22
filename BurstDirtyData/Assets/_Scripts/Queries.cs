using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queries : MonoBehaviour
{
    private const string bGreen = "<mark=#5AFF008F>";
    private const string bPink = "<mark=#FF96FF8F>";
    private const string bPurple = "<mark=#FF49FF8F>";
    private const string bYellow = "<mark=#FFFF288F>";
    private const string eRed = "<color=#C73F3F>";
    private const string eBlue = "<color=#3F89C7>";
    private const string eGreen = "<color=#3FC740>";
    private const string fe = "</color>";
    private const string fb = "</mark>";

    public Dictionary<string, List<Query>> DicoQueries;
    private void Awake()
    {
        DicoQueries = new Dictionary<string, List<Query>>();
        AddQueriesLevel1("Niveau1");
        AddQueriesEasy("Easy");
        AddQueriesMedium("Medium");
        AddQueriesHard("Hard");
        AddQueriesExtreme("Extreme");

        DicoQueries.Add("Niveau2", DicoQueries["Easy"]);
        DicoQueries.Add("Niveau3", DicoQueries["Easy"]);
        DicoQueries.Add("Niveau4", DicoQueries["Medium"]);
        DicoQueries.Add("Niveau5", DicoQueries["Medium"]);
        DicoQueries.Add("Niveau6", DicoQueries["Hard"]);
        DicoQueries.Add("Niveau7", DicoQueries["Hard"]);

        //Ajout des queries du niveau 8
        List<Query> niveau8 = new List<Query>();
        foreach(Query q in DicoQueries["Easy"])
        {
            niveau8.Add(q);
        }
        foreach (Query q in DicoQueries["Medium"])
        {
            niveau8.Add(q);
        }
        foreach (Query q in DicoQueries["Hard"])
        {
            niveau8.Add(q);
        }
        DicoQueries.Add("Niveau8", niveau8);
        DicoQueries.Add("NiveauMaudit", DicoQueries["Extreme"]);
        DicoQueries.Add("NiveauMadembonus", DicoQueries["Easy"]);


        RandomizeList(DicoQueries["Niveau2"]);
        RandomizeList(DicoQueries["Niveau3"]);
        RandomizeList(DicoQueries["Niveau4"]);
        RandomizeList(DicoQueries["Niveau5"]);
        RandomizeList(DicoQueries["Niveau6"]);
        RandomizeList(DicoQueries["Niveau7"]);
        RandomizeList(DicoQueries["Niveau8"]);
        RandomizeList(DicoQueries["NiveauMaudit"]);
        RandomizeList(DicoQueries["NiveauMadembonus"]);
    }

    public void RandomizeList(List<Query> alpha)
    {
        for (int i = 0; i < alpha.Count; i++)
        {
            Query temp = alpha[i];
            int randomIndex = Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
    }
    private void AddQueriesLevel1(string name){
        List<Query> queries = new List<Query>();
        List<Condition> conditions = new List<Condition>();
        //int[] fillamount;

        //SELECT ID FROM GREEN WHERE COLOR="red";
        conditions.Add(new Condition("", "green", "red"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE COLOR=\""+eRed+ "red"+fe+"\";", conditions.ToArray(),20));

        //SELECT ID FROM YELLOW WHERE COLOR="green";
        conditions.Clear();
        conditions.Add(new Condition("", "yellow", "green"));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE COLOR=\""+eGreen+"green"+fe+"\";", conditions.ToArray(),20));


        //SELECT ID FROM PINK WHERE FILLAMOUNT >= 50;
        conditions.Clear();
        conditions.Add(new Condition("", "pink", "", 50, ">="));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" WHERE FILLAMOUNT >= 50;", conditions.ToArray(),20));

        //ajout de al liste dans le dictionnaire
        DicoQueries.Add(name, queries);
    }

    private void AddQueriesEasy(string name)
    {
        List<Query> queries = new List<Query>();
        List<Condition> conditions = new List<Condition>();
        int[] fillamount;

        //SELECT ID FROM YELLOW WHERE SHAPE="sphere";
        conditions.Add(new Condition("sphere", "yellow"));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE SHAPE = \"sphere\"; ", conditions.ToArray(), 15));


        //SELECT ID FROM GREEN WHERE SHAPE = "capsule" AND COLOR = "blue";
        conditions.Clear();
        conditions.Add(new Condition("capsule", "green", "blue"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE SHAPE = \"capsule\" AND COLOR = \""+eBlue+"blue"+fe+"\";", conditions.ToArray(), 15));

        //SELECT ID FROM PURPLE WHERE COLOR IN ("red","green");
        conditions.Clear();
        conditions.Add(new Condition("", "purple","red"));
        conditions.Add(new Condition("", "purple","green"));
        queries.Add(new Query("SELECT ID FROM "+bPurple+"PURPLE"+fb+" WHERE COLOR IN(\""+eRed+"red"+fe+"\",\""+eGreen+"green"+fe+"\");", conditions.ToArray(), 15));

        //SELECT ID FROM PINK WHERE FILLAMOUNT BETWEEN 0 AND 50;
        conditions.Clear();
        fillamount = new int[] { 0, 50 };
        conditions.Add(new Condition(fillamount,"=","","pink"));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" WHERE FILLAMOUNT BETWEEN 0 AND 50;", conditions.ToArray(), 15));

        //SELECT ID FROM PINK WHERE FILLAMOUNT = 50 OR COLOR = "red";
        conditions.Clear();
        conditions.Add(new Condition("", "pink", "red"));
        conditions.Add(new Condition("", "pink", "", 50, "="));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" WHERE FILLAMOUNT = 50 OR COLOR = \""+eRed+"red"+fe+"\";",conditions.ToArray(),15));


        //SELECT ID FROM GREEN LIMIT 5;
        conditions.Clear();
        conditions.Add(new Condition("", "green"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" LIMIT 5;", conditions.ToArray(), 15, 5));

        //SELECT ID FROM YELLOW WHERE SHAPE = "sphere" AND FILLAMOUNT <= 50;
        conditions.Clear();
        conditions.Add(new Condition("sphere", "yellow", "",50,"<="));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE SHAPE = \"sphere\" AND FILLAMOUNT <= 50;", conditions.ToArray(), 15));


        //SELECT ID FROM PURPLE WHERE COLOR IN("blue","green") AND SHAPE = "capsule";
        conditions.Clear();
        conditions.Add(new Condition("capsule", "purple", "blue"));
        conditions.Add(new Condition("capsule", "purple", "green"));
        queries.Add(new Query("SELECT ID FROM "+bPurple+"PURPLE"+fb+" WHERE COLOR IN(\""+eBlue+"blue"+fe+"\",\""+eGreen+"green"+fe+"\") AND SHAPE = \"capsule\";", conditions.ToArray(), 15));


        //ajout de al liste dans le dictionnaire
        DicoQueries.Add(name, queries);
    }

    private void AddQueriesMedium(string name)
    {
        List<Query> queries = new List<Query>();
        List<Condition> conditions = new List<Condition>();
        //int[] fillamount;

        //SELECT ID FROM YELLOW UNION SELECT ID FROM GREEN;
        conditions.Clear();
        conditions.Add(new Condition("", "purple"));
        conditions.Add(new Condition("", "yellow"));
        queries.Add(new Query("SELECT ID FROM " + bYellow + "YELLOW" + fb + " UNION SELECT ID FROM " + bGreen + "GREEN" + fb + ";", conditions.ToArray(), 15));


        //SELECT ID FROM YELLOW WHERE SHAPE = "sphere" ORDER BY FILLAMOUNT DESC LIMIT 5;
        conditions.Clear();
        conditions.Add(new Condition("sphere", "yellow","",100,"="));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE SHAPE = \"sphere\" ORDER BY FILLAMOUNT DESC LIMIT 5;", conditions.ToArray(), 15,5));

        //SELECT ID FROM PURPLE WHERE COLOR LIKE "_e%" LIMIT 5;
        conditions.Clear();
        conditions.Add(new Condition("", "purple", "red"));
        queries.Add(new Query("SELECT ID FROM "+bPurple+"PURPLE"+fb+" WHERE COLOR LIKE \"_e %\" LIMIT 5;", conditions.ToArray(), 15,5));

        //SELECT ID FROM PURPLE UNION SELECT ID FROM YELLOW WHERE FILLAMOUNT > 75;
        conditions.Clear();
        conditions.Add(new Condition("", "purple"));
        conditions.Add(new Condition("", "yellow","",75,">"));
        queries.Add(new Query("SELECT ID FROM "+bPurple+"PURPLE"+fb+" UNION SELECT ID FROM YELLOW WHERE FILLAMOUNT > 75;", conditions.ToArray(), 15));

        //SELECT ID FROM GREEN WHERE SUBSTR(COLOR, 4, 1) = "e";
        conditions.Clear();
        conditions.Add(new Condition("", "green", "blue"));
        conditions.Add(new Condition("", "green", "green"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE SUBSTR(COLOR, 4, 1) = \"e\";", conditions.ToArray(), 15));

        //SELECT ID FROM PURPLE WHERE SHAPE = "capsule" ORDER BY FILLAMOUNT ASC LIMIT 5;
        conditions.Clear();
        conditions.Add(new Condition("capsule", "purple", "", 25, "="));
        queries.Add(new Query("SELECT ID FROM "+bPurple+"PURPLE"+fb+" WHERE SHAPE = \"capsule\" ORDER BY FILLAMOUNT ASC LIMIT 5;", conditions.ToArray(), 15,5));

        //SELECT ID FROM GREEN WHERE SHAPE = "capsule" AND COLOR = "blue" OR FILLAMOUNT = 25;
        conditions.Clear();
        conditions.Add(new Condition("capsule", "green", "blue"));
        conditions.Add(new Condition("", "green", "", 25, "="));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE SHAPE = \"capsule\" AND COLOR = \""+eBlue+"blue"+fe+"\" OR FILLAMOUNT = 25;", conditions.ToArray(), 15));

        //SELECT ID FROM GREEN WHERE COLOR NOT IN("red", "green");
        conditions.Clear();
        conditions.Add(new Condition("", "green", "blue"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE COLOR NOT IN(\""+eRed+"red"+fe+"\", \""+eGreen+"green"+fe+"\");", conditions.ToArray(), 15));

        //ajout de al liste dans le dictionnaire
        DicoQueries.Add(name, queries);
    }

    private void AddQueriesHard(string name)
    {
        List<Query> queries = new List<Query>();
        List<Condition> conditions = new List<Condition>();
        int[] fillamount;

        //SELECT PINK.ID FROM PINK INNER JOIN YELLOW ON YELLOW.ID = PINK.YELLOW_ID WHERE PINK.COLOR NOT LIKE "_e%";
        conditions.Clear();
        conditions.Add(new Condition("", "pink", "blue"));
        conditions.Add(new Condition("", "pink", "green"));
        queries.Add(new Query("SELECT "+bPink+"PINK"+fb+".ID FROM "+bPink+"PINK"+fb+" INNER JOIN "+bYellow+"YELLOW"+fb+" ON "+bYellow+"YELLOW"+fb+".ID = "+bPink+"PINK"+fb+".YELLOW_ID WHERE "+bPink+"PINK"+fb+".COLOR NOT LIKE \"_e%\";", conditions.ToArray(), 15));


        //SELECT YELLOW. IDFROM YELLOW WHERE YELLOW.PURPLE_ID IN(SELECT PURPLE.ID FROM PURPLE);
        conditions.Clear();
        conditions.Add(new Condition("", "yellow"));
        queries.Add(new Query("SELECT "+bYellow+"YELLOW"+fb+". ID FROM "+bYellow+"YELLOW"+fb+" WHERE "+bYellow+"YELLOW"+fb+".PURPLE_ID IN(SELECT "+bPurple+"PURPLE"+fb+".ID FROM "+bPurple+"PURPLE"+fb+");", conditions.ToArray(), 15));


        //SELECT ID FROM GREEN WHERE SHAPE NOT IN(SELECT DISTINCT(SHAPE) as SHAPE FROM GREEN WHERE SHAPE != "sphere");
        conditions.Clear();
        conditions.Add(new Condition("sphere", "green"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE SHAPE NOT IN(SELECT DISTINCT(SHAPE) as SHAPE FROM "+bGreen+"GREEN"+fb+" WHERE SHAPE != \"sphere\");", conditions.ToArray(), 15));


        //SELECT ID, FILLAMOUNT FROM PINK WHERE COLOR = "blue" HAVING FILLAMOUNT BETWEEN 75 AND 100;
        conditions.Clear();
        fillamount = new int[2] { 75, 100 };
        conditions.Add(new Condition(fillamount,"=","","pink","blue"));
        queries.Add(new Query("SELECT ID, FILLAMOUNT FROM "+bPink+"PINK"+fb+" WHERE COLOR = \""+eBlue+"blue"+fe+"\" HAVING FILLAMOUNT BETWEEN 75 AND 100;", conditions.ToArray(), 15));

        //SELECT ID FROM YELLOW WHERE FILLAMOUNT BETWEEN (SELECT COUNT(DISTINCT(SHAPE)) FROM YELLOW) AND 50;
        conditions.Clear();
        fillamount = new int[2] { 2, 50 };
        conditions.Add(new Condition(fillamount, "=", "", "yellow"));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE FILLAMOUNT BETWEEN (SELECT COUNT(DISTINCT(SHAPE)) FROM "+bYellow+"YELLOW"+fb+") AND 50;", conditions.ToArray(), 15));

        //SELECT ID FROM PINK WHERE LENGTH(SUBSTR(COLOR, 3)) BETWEEN 1 AND 2;
        conditions.Clear();
        conditions.Add(new Condition("", "pink","blue"));
        conditions.Add(new Condition("", "pink","red"));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" WHERE LENGTH(SUBSTR(COLOR, 3)) BETWEEN 1 AND 2;", conditions.ToArray(), 15));

        //SELECT ID FROM PINK WHERE SHAPE NOT LIKE "%r%" AND COLOR LIKE "%r%";
        conditions.Clear();
        conditions.Add(new Condition("capsule", "pink","red"));
        conditions.Add(new Condition("capsule", "pink","green"));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" WHERE SHAPE NOT LIKE \"%r%\" AND COLOR LIKE \"% r %\";", conditions.ToArray(), 15));

        //SELECT ID FROM GREEN WHERE UPPER(COLOR) LIKE "%E%" LIMIT 5;
        conditions.Clear();
        conditions.Add(new Condition("", "green", "blue"));
        conditions.Add(new Condition("", "green", "red"));
        queries.Add(new Query("SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE UPPER(COLOR) LIKE \"%E%\" LIMIT 5;", conditions.ToArray(), 15,5));

        //ajout de al liste dans le dictionnaire
        DicoQueries.Add(name, queries);
    }

    private void AddQueriesExtreme(string name)
    {
        List<Query> queries = new List<Query>();
        List<Condition> conditions = new List<Condition>();
        int[] fillamount;

        //SELECT ID FROM PINK INNER JOIN YELLOW ON YELLOW.ID = PINK.YELLOW_ID INNER JOIN PURPLE ON PURPLE.ID = YELLOW.PURPLE_ID INNER JOIN GREEN ON GREEN.ID = PURPLE.GREEN_ID WHERE SHAPE NOT IN(SELECT DISTINCT(SHAPE) as SHAPE FROM GREEN WHERE SHAPE != "sphere");
        conditions.Add(new Condition("sphere", "pink"));
        queries.Add(new Query("SELECT ID FROM "+bPink+"PINK"+fb+" INNER JOIN "+bYellow+"YELLOW"+fb+" ON "+bYellow+"YELLOW"+fb+".ID = "+bPink+"PINK"+fb+".YELLOW_ID INNER JOIN "+bPurple+"PURPLE"+fb+" ON "+bPurple+"PURPLE"+fb+".ID = "+bYellow+"YELLOW"+fb+".PURPLE_ID INNER JOIN "+bGreen+"GREEN"+fb+" ON "+bGreen+"GREEN"+fb+".ID = "+bPurple+"PURPLE"+fb+".GREEN_ID WHERE SHAPE NOT IN(SELECT DISTINCT(SHAPE) as SHAPE FROM "+bGreen+"GREEN"+fb+" WHERE SHAPE != \"sphere\");", conditions.ToArray(), 15));

        //SELECT* FROM(SELECT ID, FILLAMOUNT FROM GREEN WHERE COLOR= "blue" AND SHAPE LIKE "%e%" UNION SELECT ID, FILLAMOUNT FROM YELLOW)AS TEMPTABLE HAVING FILLAMOUNT BETWEEN 75 AND 100;
        conditions.Clear();
        fillamount = new int[] { 75, 100 };
        conditions.Add(new Condition(fillamount,"=","sphere", "green", "blue"));
        conditions.Add(new Condition(fillamount, "=", "", "yellow"));
        queries.Add(new Query("SELECT * FROM(SELECT ID, FILLAMOUNT FROM "+bGreen+"GREEN"+fb+" WHERE COLOR = \""+eBlue+"blue"+fe+"\" AND SHAPE LIKE \"%e%\" UNION SELECT ID, FILLAMOUNT FROM "+bYellow+"YELLOW"+fb+")AS TEMPTABLE HAVING FILLAMOUNT BETWEEN 75 AND 100;", conditions.ToArray(), 15));


        //SELECT ID, FILLAMOUNT FROM YELLOW WHERE COLOR = "blue" AND SHAPE LIKE "%e%" UNION SELECT ID, FILLAMOUNT FROM PINK HAVING FILLAMOUNT BETWEEN 75 AND 100;
        conditions.Clear();
        fillamount = new int[] { 75, 100 };
        conditions.Add(new Condition("sphere","yellow","blue"));
        conditions.Add(new Condition(fillamount, "=", "", "pink"));
        queries.Add(new Query("SELECT ID, FILLAMOUNT FROM "+bYellow+"YELLOW"+fb+" WHERE COLOR = \""+eBlue+"blue"+fe+"\" AND SHAPE LIKE \"%e%\" UNION SELECT ID, FILLAMOUNT FROM "+bPink+"PINK"+fb+" HAVING FILLAMOUNT BETWEEN 75 AND 100;", conditions.ToArray(), 15));

        //SELECT ID FROM YELLOW WHERE FILLAMOUNT BETWEEN(SELECT COUNT(DISTINCT(SHAPE)) FROM YELLOW) AND 50 UNION SELECT ID FROM GREEN WHERE UPPER(COLOR) LIKE "%E%";
        conditions.Clear();
        fillamount = new int[] { 2, 50};
        conditions.Add(new Condition(fillamount, "=","", "yellow", "blue"));
        conditions.Add(new Condition("", "green", "green"));
        conditions.Add(new Condition("", "green", "red"));
        queries.Add(new Query("SELECT ID FROM "+bYellow+"YELLOW"+fb+" WHERE FILLAMOUNT BETWEEN(SELECT COUNT(DISTINCT(SHAPE)) FROM "+bYellow+"YELLOW"+fb+") AND 50 UNION SELECT ID FROM "+bGreen+"GREEN"+fb+" WHERE UPPER(COLOR) LIKE \"%E%\";", conditions.ToArray(), 15));

        //SELECT* FROM(SELECT ID, FILLAMOUNT FROM YELLOW WHERE SHAPE="sphere" AND COLOR LIKE "_e%" UNION SELECT ID, FILLAMOUNT FROM PINK)AS TEMPTABLE HAVING FILLAMOUNT BETWEEN 25 AND 50;
        conditions.Clear();
        fillamount = new int[] { 25, 50 };
        conditions.Add(new Condition(fillamount, "=", "sphere", "yellow", "red"));
        conditions.Add(new Condition(fillamount, "=", "", "pink"));
        queries.Add(new Query("SELECT* FROM(SELECT ID, FILLAMOUNT FROM "+bYellow+"YELLOW"+fb+" WHERE SHAPE= \"sphere\" AND COLOR LIKE \"_e%\" UNION SELECT ID, FILLAMOUNT FROM "+bPink+"PINK"+fb+")AS TEMPTABLE HAVING FILLAMOUNT BETWEEN 25 AND 50;", conditions.ToArray(), 15));

        //SELECT ID, FILLAMOUNT FROM YELLOW AS GREEN WHERE GREEN.COLOR = "blue" AND GREEN.SHAPE LIKE "%e%" UNION SELECT ID, FILLAMOUNT FROM PINK AS PURPLE HAVING PURPLE.FILLAMOUNT BETWEEN 75 AND 100;
        conditions.Clear();
        fillamount = new int[] { 75, 100 };
        conditions.Add(new Condition("sphere", "yellow", "blue"));
        conditions.Add(new Condition(fillamount, "=", "", "pink"));
        queries.Add(new Query("SELECT ID, FILLAMOUNT FROM "+bYellow+"YELLOW"+fb+" AS "+bGreen+"GREEN"+fb+" WHERE "+bGreen+"GREEN"+fb+".COLOR = \""+eBlue+"blue"+fe+"\" AND "+bGreen+"GREEN"+fb+".SHAPE LIKE \"%e%\" UNION SELECT ID, FILLAMOUNT FROM "+bPink+"PINK"+fb+" AS "+bPurple+"PURPLE"+fb+" HAVING "+bPurple+"PURPLE"+fb+".FILLAMOUNT BETWEEN 75 AND 100;", conditions.ToArray(), 15));

        //ajout de al liste dans le dictionnaire
        DicoQueries.Add(name, queries);
    }
}
