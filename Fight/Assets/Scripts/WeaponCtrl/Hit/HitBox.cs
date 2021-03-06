﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器的伤害盒子
/// </summary>
public class HitBox : MonoBehaviour {

    [HideInInspector]
    public MeleeWeapon weapon;

    [HideInInspector]
    public Collider trigger;


    /// <summary>
    /// 检测触发器
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    bool CheckTrigger(Collider other)
    {
        return (weapon != null && (weapon.owner == null || other.gameObject != weapon.owner.gameObject));
    }

    private void Start()
    {
        trigger = GetComponent<Collider>();
        if (!trigger)
        {
            trigger = gameObject.AddComponent<BoxCollider>();
        }
        if (trigger)
        {
            trigger.isTrigger = true;
            trigger.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTrigger(other))
        {
            if (weapon!=null)
            {
                
                //攻击目标
                weapon.OnHit(this, other);
                Debug.Log(other.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        trigger = gameObject.GetComponent<Collider>();
        if (!trigger) trigger = gameObject.AddComponent<BoxCollider>();
        Color color = Color.red;
        color.a = 0.6f;
        Gizmos.color = color;
        if (!Application.isPlaying && trigger && !trigger.enabled) trigger.enabled = true;
        if (trigger && trigger.enabled)
        {
            if (trigger as BoxCollider)
            {
                BoxCollider box = trigger as BoxCollider;

                var sizeX = transform.lossyScale.x * box.size.x;
                var sizeY = transform.lossyScale.y * box.size.y;
                var sizeZ = transform.lossyScale.z * box.size.z;
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(box.bounds.center, transform.rotation, new Vector3(sizeX, sizeY, sizeZ));
                Gizmos.matrix = rotationMatrix;
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
            }
        }
    }
}
