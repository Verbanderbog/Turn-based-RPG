using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<GameObject> participants;
    List<GameObject> turnOrder;
    Dictionary<int, List<GameObject>> teams;
    int currentCharacter=0;
    public ToggleGroup menubarToggleGroup;
    public GameObject moveListContent;
    public GameObject movePrefab;
    public GameObject allyAnchor;
    public GameObject enemyAnchor;
    public List<GameObject> characterPlacements;
    private int currentMoveListDraw=-1;



    private void OnValidate()
    {
        assignTeamDictionary();
        assignTurnOrder();
    }
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        assignTeamDictionary();
        assignTurnOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void assignTeamDictionary()
    {
        foreach(GameObject x in participants)
        {
            CharacterInstance xCharacter = x.GetComponent<CharacterInstance>();
            if (false)
            {

            }
        }
    }
    private void assignTurnOrder()
    {

        turnOrder = new List<GameObject>(participants);
        turnOrder.Sort(compareCharacterSpeeds);

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
            foreach (Move x in turnOrder[currentCharacter].GetComponent<Character>().MoveSet)
            {
                GameObject moveUI = Instantiate(movePrefab, new Vector3(0, 0, 0), Quaternion.identity, moveListContent.transform);
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
