using CameraShaking;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Interactables;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.ThrowableProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SillyAdminItems
{
    [CustomItem(ItemType.GrenadeHE)]
    public class ClusterBall : CustomGrenade
    {
        public override bool ExplodeOnCollision { get; set; } = true;
        public override float FuseTime { get; set; } = 10;
        public override uint Id { get; set; } = 85;
        public override string Name { get; set; } = "Cluster of balls";
        public override string Description { get; set; } = "spawns 10 ruber balls";
        public override float Weight { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }
        public override Vector3 Scale { get => base.Scale; set => base.Scale = base.Scale * 2; }

        protected override void OnExploding(ExplodingGrenadeEventArgs ev)
        {
            base.OnExploding(ev);
            for (int i = 0; i < 10; i++)
            {

                Throwable.Create(ItemType.SCP018, ev.Player).CreatePickup(ev.Position + new Vector3(0, 2, 0)).Spawn();
            }
            ev.IsAllowed = false;
        }
    }
}