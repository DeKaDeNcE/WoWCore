﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Framework.Constants;

namespace Game.Entities
{
    public class PowerPctOrderPred : IComparer<WorldObject>
    {
        public PowerPctOrderPred(PowerType power, bool ascending = true)
        {
            m_power = power;
            m_ascending = ascending;
        }

        public int Compare(WorldObject objA, WorldObject objB)
        {
            Unit a = objA.ToUnit();
            Unit b = objB.ToUnit();
            float rA = a != null ? a.GetPowerPct(m_power) : 0.0f;
            float rB = b != null ? b.GetPowerPct(m_power) : 0.0f;
            return Convert.ToInt32(m_ascending ? rA < rB : rA > rB);
        }

        PowerType m_power;
        bool m_ascending;
    }

    public class HealthPctOrderPred : IComparer<WorldObject>
    {
        public HealthPctOrderPred(bool ascending = true)
        {
            m_ascending = ascending;
        }

        public int Compare(WorldObject objA, WorldObject objB)
        {
            Unit a = objA.ToUnit();
            Unit b = objB.ToUnit();
            float rA = a.GetMaxHealth() != 0 ? a.GetHealth() / (float)a.GetMaxHealth() : 0.0f;
            float rB = b.GetMaxHealth() != 0 ? b.GetHealth() / (float)b.GetMaxHealth() : 0.0f;
            return Convert.ToInt32(m_ascending ? rA < rB : rA > rB);
        }

        bool m_ascending;
    }

    public class ObjectDistanceOrderPred : IComparer<WorldObject>
    {
        public ObjectDistanceOrderPred(WorldObject pRefObj, bool ascending = true)
        {
            m_refObj = pRefObj;
            m_ascending = ascending;
        }

        public int Compare(WorldObject pLeft, WorldObject pRight)
        {
            return (m_ascending ? m_refObj.GetDistanceOrder(pLeft, pRight) : !m_refObj.GetDistanceOrder(pLeft, pRight)) ? 1 : 0;
        }

        WorldObject m_refObj;
        bool m_ascending;
    }
}