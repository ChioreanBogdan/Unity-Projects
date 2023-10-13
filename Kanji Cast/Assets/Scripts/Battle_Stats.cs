using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Stats : Stats {

    public int unique_id;

    //devine true daca un personaja folosit un skill
    public bool used_skill;

    public bool stunned;

    //rezistenta la foc a cuiva (poate avea si valoare negativa)
    public int fire_resistance;

    //cate incarcaturi cu praf inflamabil am
    public int いち_flammable_charges;

    //returneaza id-ul unic al personajului selectat
    public int Get_Unique_id()
    {
        return unique_id;
    }

    public bool Get_used_skill()
    {
        return used_skill;
    }

    public int Get_current_health()
    {
        return Current_Health;
    }

    public int Get_いち_flammable_charges()
    {
        return いち_flammable_charges;
    }

    public void Set_used_skill(bool new_value)
    {
        used_skill = new_value;
    }

    public void Set_Unique_id(int id)
    {
        unique_id = id;
    }

    public void Set_Current_Health(int new_current_health)
    {
        this.Current_Health = new_current_health;
    }

    public void Set_いち_flammable_charges(int number_of_charges)
    {
        this.いち_flammable_charges = number_of_charges;
    }
                                    }
