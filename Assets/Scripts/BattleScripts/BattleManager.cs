using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    [Serializable]
    public struct participantAmounts
    {
        public GameObject participant;
        public byte amount;
    }
    public List<participantAmounts> participants;
    List<GameObject> turnOrder = new List<GameObject>();
    Dictionary<int, List<GameObject>> teams = new Dictionary<int, List<GameObject>>();
    int currentCharacter=0;
    public ToggleGroup menubarToggleGroup;
    public GameObject moveListContent;
    public GameObject moveUIPrefab;
    public GameObject allyAnchor;
    public GameObject enemyAnchor;
    public List<GameObject> characterPlacements;
    private int currentMoveListDraw=-1;



    private void OnValidate()
    {
        
        
    }
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        instatiateAndAssignTeamDictionaryTurnOrder();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void instatiateAndAssignTeamDictionaryTurnOrder()
    {
        
        foreach(participantAmounts x in participants)
        {
            for(byte i=0; i < x.amount; i++) {
                GameObject instance = Instantiate(x.participant, new Vector3(0, 0, 0), Quaternion.identity,null);
                turnOrder.Add(instance);
                int team = instance.GetComponent<CharacterInstance>().team;
                if (!teams.ContainsKey(team))
                {
                    teams.Add(team, new List<GameObject>());
                }
                teams[team].Add(instance);

            }
            
        }
        turnOrder.Sort(compareCharacterSpeeds);
        GameObject allyPlacement = Instantiate(characterPlacements[teams[0].Count - 1], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), allyAnchor.transform);
        int enemies = 0;
        foreach (int key in teams.Keys)
        {
            if (key == 0)
                continue;
            enemies += teams[key].Count;
        }
        GameObject enemyPlacement = Instantiate(characterPlacements[enemies - 1], new Vector3(0, 0, 0), new Quaternion(0,0,0,0), enemyAnchor.transform);
        List<GameObject> teamList=new List<GameObject>();
        foreach (int key in teams.Keys)
        {
            teamList.AddRange(teams[key]);
            if (key == 0)
            {
                Transform[] allyPoints = allyPlacement.transform.GetComponentsInChildren<Transform>();
                
                for (int i=1; i<allyPoints.Length; i++)
                {
                    teamList[i - 1].transform.SetParent(allyPoints[i],false);
                }
                teamList = new List<GameObject>();
            }
            
        }
        Transform[] points = enemyPlacement.transform.GetComponentsInChildren<Transform>();
        
        for (int i = 1; i < points.Length; i++)
        {
            teamList[i - 1].transform.SetParent(points[i],false);
        }

    }
    
    private static int compareCharacterSpeeds(GameObject x, GameObject y)
    {
        CharacterInstance xCharacter =x.GetComponent<CharacterInstance>();
        CharacterInstance yCharacter =y.GetComponent<CharacterInstance>();
        if(xCharacter == null)
        {
            if (yCharacter == null)
            {
                // If x is null and y is null, they're
                // equal.
                return 0;
            }
            else
            {
                // If x is null and y is not null, y
                // is greater.
                return -1;
            }
        }
        else
        {
            if (yCharacter == null)
            // ...and y is null, x is greater.
            {
                return 1;
            }
            else
            {
                // ...and y is not null, compare the speeds
                Debug.Log(xCharacter + ", " + xCharacter._speed);
                Debug.Log(yCharacter + ", " + yCharacter._speed);  
                int retval = xCharacter._speed.Value.CompareTo(yCharacter._speed.Value);
                if (retval != 0)
                {
                    // If the speeds are not equal,
                    // the higher speed is greater.
                    //
                    return retval;
                }
                else
                {
                    // If the speeds are equal,
                    // sort them by team.
                    //
                    return xCharacter.team.CompareTo(yCharacter.team);
                }
            }
        }
    }

    public void drawMoves()
    {
        if (currentMoveListDraw != currentCharacter)
        {
            currentMoveListDraw = currentCharacter;
            foreach (Component y in moveListContent.GetComponentsInChildren<Component>())
            {
                //Destroy(y.gameObject);
                if (y is Button)
                {
                    Button button = (Button) y;
                    button.onClick.RemoveAllListeners();
                }
            }
            foreach (Move x in turnOrder[currentCharacter].GetComponent<CharacterInstance>().MoveSet)
            {
                GameObject moveUI = Instantiate(moveUIPrefab, new Vector3(0, 0, 0), Quaternion.identity, moveListContent.transform);
                TMP_Text[] text = moveUI.GetComponentsInChildren<TMP_Text>();
                text[0].text = x.Name;
                text[1].text = x.ActionCost.ToString();
                Button moveButton = moveUI.GetComponent<Button>();
                moveButton.onClick.AddListener(() => selectTarget(x));
                
            }
        }
    }

    private void selectTarget(Move move)
    {
        foreach (Toggle x in menubarToggleGroup.ActiveToggles())
        {
            x.isOn = false;
        }
    }
}
