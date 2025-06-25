using CameraShaking;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Interactables;
using InventorySystem.Items.Firearms.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SillyAdminItems
{
    [CustomItem(ItemType.GunFRMG0)]
    public class DeathsPaintbrush : CustomWeapon
    {
        public override uint Id { get; set; } = 84;
        public override string Name { get; set; } = "Deaths paintbrush";
        public override float Damage { get; set; } = 0;
        public override string Description { get; set; } = "...";
        public override float Weight { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }

        protected override void OnShooting(ShootingEventArgs ev)
        {
            base.OnShooting(ev);
            ev.Firearm.Inaccuracy = 0f;
            ev.Firearm.Recoil = new RecoilSettings(0,0,0,0,0);
            ev.Firearm.AmmoDrain = 0;

            Ray ray = new Ray(ev.Player.Position + (ev.Direction *3),ev.Direction);
            Physics.Raycast(ev.Player.Position + (ev.Direction * 3), ev.Direction, out RaycastHit hit, 10000);

            Map.Explode(hit.point, ProjectileType.FragGrenade, ev.Player);
        }


    }
}