using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum E_CHARACTER_TYPE
{
    INVALID = (-1),
    PLAYER,
    ENEMY_01,
    ENEMY_02,
    BOSS,
    NPC,
    MAX
}

enum E_CHARACTER_STAT
{
    INVALID = (-1),
    IDLE,
    RUN,
    ATTACK,
    SKILL_01,
    SKILL_02,
    JUMP,
    MAX
}

enum E_PLAYER_ATTACK_NO
{
    INVALID = (-1),
    DEFAULT,
    SKILL_01,
    SKILL_02,
    SKILL_03,
    MAX
}