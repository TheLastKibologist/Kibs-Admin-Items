using CameraShaking;
using DrawableLine;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Interactables;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SillyAdminItems
{
    [CustomItem(ItemType.GunCOM18)]
    public class GravityGun : CustomWeapon
    {
        public static CoroutineHandle _coro;
        public override uint Id { get; set; } = 86;
        public override string Name { get; set; } = "Gravity Gun";
        public override float Damage { get; set; } = 0;
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }
        public static bool grabed = false;
        public static bool release = false;

        protected override void OnShot(ShotEventArgs ev)
        {
            base.OnShot(ev);
            if (grabed)
            {
                release= true;
            }
            else
            {
                if (ev.Target != null)
                {
                    grabed = true;
                    _coro = Timing.RunCoroutine(Coro(ev.Player, ev.Target));
                }
                ev.Firearm.AmmoDrain = 0;
            }


        }
        public static IEnumerator<float> Coro(Player owner, Player grabbed)
        {
            FpcRole role;
            while (grabed)
            {
                grabbed.IsGodModeEnabled = true;
                if (grabbed.IsAlive)
                {
                     role = (FpcRole)grabbed.Role;
                    role.Velocity = new Vector3(0, 0, 0);
                    role.Gravity = new Vector3(0, 0, 0);

                }
                else
                {
                    grabbed.IsGodModeEnabled = false;
                    grabed = false;
                    release = false;
                    break;
                }
                    Vector3 dir = (owner.CameraTransform.rotation * Vector3.forward) * 2;
                grabbed.Position = owner.CameraTransform.position + dir;
                try
                {
                }
                catch { }
                if (release || owner.IsDead)
                {
                    grabbed.IsGodModeEnabled = false;
                    role.Gravity = ((FpcRole)owner.Role).Gravity;
                    grabed = false;
                    release = false;
                    break;
                }

                yield return Timing.WaitForSeconds(0.01f);
            }
        }
    }
}