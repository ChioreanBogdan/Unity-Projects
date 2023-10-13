using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effects {

   public static int Deal_physical_damage(int damage,int life)
    {
        life = life-damage;
        return life;
    }

    //Efecte pentru kanji-ul 一
    public static int Add_flammable_charges(int numbers_of_charges,int charges_to_add)
    {
        numbers_of_charges = numbers_of_charges + charges_to_add;
        return numbers_of_charges;
    }
                            }
