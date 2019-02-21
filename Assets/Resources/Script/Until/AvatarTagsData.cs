using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AvatarTagsData
{
    public static List<string> tags = new List<string>()
    {
        "hit_Head",
        "hit_Body",
        "hit_Limbs"
    };

    public static Dictionary<string, float> demageRate = new Dictionary<string, float>
    {
        {
            "hit_Head",
            2f
        },
        {
            "hit_Body",
            1f
        },
        {
            "hit_Limbs",
            0.7f
        },
    };
}
