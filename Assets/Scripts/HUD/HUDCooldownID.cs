using UnityEngine;
using System.Collections;

public enum CooldownID { none = 0, Spell, Absorb, Boot };

public class HUDCooldownID : MonoBehaviour {

    public CooldownID theID;
}
