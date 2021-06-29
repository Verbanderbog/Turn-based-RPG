using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInstance : MonoBehaviour
{   
    Character baseStats;
    public bool VaryStatsAndDontUpdate=true;//if true stats will vary by a set amount and EXP/stat changes will not update on the base sheet
    public string Name = "";
    public short Level = 1;
    public int HealthMax = 1;
    public int ActionPointsMax = 1;
    public short Strength = 0; //determines defense, crit damage and melee damage
    public short Speed = 0; //determines turn order, crit chance and dodge chance
    public short Aim = 0; //determines hit chance, crit chance and ranged damage
    public short Logic = 0; //determines ability damage, crit damage, a little melee damage, and a little ranged damage

    public List<Move> DefaultMoveList;

    public HashSet<Move> MoveSet;

    public bool AllowDifferentTeam=false;

    public byte team;

    //Exposed Stats
    public ResourceStat _health;
    public ResourceStat _actionPoints;
    public ExposedStat _strength; //determines defense, crit damage and melee damage
    public ExposedStat _speed; //determines turn order, crit chance and dodge chance
    public ExposedStat _aim; //determines hit chance, crit chance and ranged damage
    public ExposedStat _logic; //determines ability damage, crit damage, a little melee damage, and a little ranged damage
    //Derived Stats
    public DerivedStat _defense;
    public DerivedStat _dodge;
    public DerivedStat _critDamage;
    public DerivedStat _critChance;
    public DerivedStat _hitChance;
    public DerivedStat _melee;
    public DerivedStat _ranged;
    public DerivedStat _ability;

    private void OnValidate()
    {

        if (baseStats != null)
        {
            Name = baseStats.Name;
            Level = baseStats.Level;
            HealthMax = (int)baseStats._health.BaseValue;
            ActionPointsMax = (int)baseStats._actionPoints.BaseValue;
            Strength = (short)baseStats._strength.BaseValue;
            Speed = (short)baseStats._speed.BaseValue;
            Aim = (short)baseStats._aim.BaseValue;
            Logic = (short)baseStats._logic.BaseValue;
        }
        if (VaryStatsAndDontUpdate)
        {
            _health = new ResourceStat(HealthMax, StatType.Health);
            _actionPoints = new ResourceStat(ActionPointsMax, StatType.ActionPoints);
            _strength = new ExposedStat(Strength, StatType.Strength, _defense, _critDamage, _melee);
            _speed = new ExposedStat(Speed, StatType.Speed, _critChance, _dodge);
            _aim = new ExposedStat(Aim, StatType.Aim, _hitChance, _critChance, _ranged);
            _logic = new ExposedStat(Logic, StatType.Logic, _ability, _critDamage, _melee, _ranged);

            _defense = new DerivedStat(StatType.Defense, 0.9f, 0f, _strength);
            _dodge = new DerivedStat(StatType.Dodge, 5f, -5f, _speed);
            _critDamage = new DerivedStat(StatType.CritDamage, 4f, 1f, _strength, _logic);
            _critChance = new DerivedStat(StatType.CritChance, 4f, 0f, _speed, _aim);
            _hitChance = new DerivedStat(StatType.HitChance, 4f, 0f, _aim);
            _melee = new DerivedStat(StatType.Melee, 1000f, 1f, _strength, _logic);
            _ranged = new DerivedStat(StatType.Ranged, 1000f, 1f, _aim, _logic);
            _ability = new DerivedStat(StatType.Ability, 1000f, 1f, _logic);
        }
        else
        {
            _health = baseStats._health;
            _actionPoints = baseStats._actionPoints;
            _strength = baseStats._strength;
            _speed = baseStats._speed;
            _aim = baseStats._aim;
            _logic = baseStats._logic;

            _defense = baseStats._defense;
            _dodge = baseStats._dodge;
            _critDamage = baseStats._critDamage;
            _critChance = baseStats._critChance;
            _hitChance = baseStats._hitChance;
            _melee = baseStats._melee;
            _ranged = baseStats._ranged;
            _ability = baseStats._ability;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
}
