using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//fused kanji sunt acei kanji care se pot asambla din alti kanji
[CreateAssetMenu(fileName = "new Fused Kanji", menuName = "Fused Kanji")]
public class Fused_Kanji : Kanji {

    public int[] component_radical_ids;
    public int[] component_kanji_ids;

    //Iau toate id-urile din slot-uri si le compar cu id-urile din component_kanji_id,daca se potrivesc poate avea loc fuziunea
    public bool Fuse(int[] ids_for_fusion) {

        //int matching_kanji_ids = 0;

        var component_list = new List<int>(component_radical_ids);
        var fusion_id_list = new List<int>(ids_for_fusion);
        List<int> Intersection_list = new List<int>();

        //intersectez elementele array-ului ids_for_fusion cu component_kanji_ids
        //intersect va contine elementele comune ale celor 2 array-uri
        //var intersect = component_kanji_ids.Intersect(ids_for_fusion);

        //for (int j = 0; j < component_list.Count; j++)
        //{
        //int j = 0;
        //    for (int k = 0; k < fusion_id_list.Count; k++)
        //    {
        //        if (component_list[j] == fusion_id_list[k] && j< component_list.Count)
        //        {
        //            Intersection_list.Add(fusion_id_list[k]);
        //            j++; //k++;
        //            //component_list.Remove(comp);
        //        }
        //    }
        //}

        Intersection_list = Return_intersection(ids_for_fusion);

        //fusion_id_list.RemoveAll(x => component_list.Exists(y => y != x));

        int i = 0;
        foreach (int intersect in Intersection_list)
        {
            Debug.Log("Intersection_list[" + i + "]=" + intersect);
            i++;
        }

        i = 0;
        foreach (int comp in component_radical_ids)
        {
            Debug.Log("comp[" + i + "]=" + comp);
            i++;
        }

        i = 0;
        foreach (int fus in fusion_id_list)
        {
            Debug.Log("fus[" + i + "]=" + fus);
            i++;
        }

        Debug.Log("Comp length=" + component_radical_ids.Length);
        Debug.Log("Fusion length=" + fusion_id_list.ToArray().Length);

        //if (component_kanji_ids.Length <= fusion_id_list.ToArray().Length) return true;
        if (component_radical_ids.Length <= Intersection_list.Count()) return true;
        else return false;
    }

    public int[] Get_component_kanji_ids()
    {
        return component_kanji_ids;
    }

    //returnez numarul de kanji componente
    public int Get_number_of_component_radicals()
    {
        return component_radical_ids.Length;
    }

    public List<int> Return_intersection(int[] fusion_ids) {
        var component_list = new List<int>(component_radical_ids);
        var fusion_id_list = new List<int>(fusion_ids);

        var intersect = component_list.Intersect(fusion_id_list).ToList();
        var groups1 = component_list.Where(e => intersect.Contains(e)).GroupBy(e => e);
        var groups2 = fusion_id_list.Where(e => intersect.Contains(e)).GroupBy(e => e);

        var allGroups = groups1.Concat(groups2);

        return allGroups.GroupBy(e => e.Key)
            .SelectMany(group => group
                .First(g => g.Count() == group.Min(g1 => g1.Count())))
            .ToList();
                                                            }
                                            }
