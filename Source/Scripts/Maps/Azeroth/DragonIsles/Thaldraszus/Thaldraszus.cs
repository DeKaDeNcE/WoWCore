// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using Framework.Constants;
using Game.Entities;
using Game.Scripting;

namespace Scripts.Maps.Azeroth.DragonIsles.Thaldraszus;

struct ScenePackageIds
{
    public const uint DragonsFlyingValdrakken = 3452;
}

struct SceneIds
{
    public const uint DragonsFlyingValdrakken = 3024;
}

[Script]
class playerscript_thaldraszus_enter : PlayerScript
{
    public playerscript_thaldraszus_enter() : base("playerscript_thaldraszus_enter") { }

    public override void OnUpdateZone(Player player, uint newZone, uint newArea)
    {
        if (newZone == (uint)AreaIds.Thaldraszus || newZone == (uint)AreaIds.Valdrakken)
        {
            if (!player.GetSceneMgr().HasScene(ScenePackageIds.DragonsFlyingValdrakken))
                player.GetSceneMgr().PlaySceneByPackageId(ScenePackageIds.DragonsFlyingValdrakken, SceneFlags.None);
        }
        else if (player.GetSceneMgr().HasScene(ScenePackageIds.DragonsFlyingValdrakken))
            player.GetSceneMgr().CancelSceneByPackageId(ScenePackageIds.DragonsFlyingValdrakken);
    }
}