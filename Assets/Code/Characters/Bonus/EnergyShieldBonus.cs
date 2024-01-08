using System;
using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Characters;
using UnityEngine;

public class EnergyShieldBonus : MonoBehaviour
{
    public OutboundDisabler Disabler;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerHealth>().ActivateOneDamageShield();
        Disabler.ReturnToSpawner();
    }
}
