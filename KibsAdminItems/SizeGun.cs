using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using InventorySystem.Items.Firearms.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hints;

namespace SillyAdminItems
{
    [CustomItem(ItemType.GunCOM15)]
    public class SizeGun : CustomWeapon
    {
        public static CoroutineHandle _FlashlightCheck;
        public override uint Id { get; set; } = 83;
        public override string Name { get; set; } = "Scale gun";
        public override float Damage { get; set; } = 0;
        public override string Description { get; set; } = "Doubles or halfs the size of a target";
        public override float Weight { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }

        public static Firearm firearm;

        public static Player owner;

        public static bool self;

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            base.OnAcquired(player, item, displayMessage);
            firearm = item as Firearm;
            owner = player;
            _FlashlightCheck = Timing.RunCoroutine(LightCheck());
            self = false;
        }
        protected override void OnDroppingItem(DroppingItemEventArgs ev)
        {
            base.OnDroppingItem(ev);

        }
        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            base.OnReloading(ev);
            self = !self;
            ev.IsAllowed = false;
        }
        protected override void OnShooting(ShootingEventArgs ev)
        {
            base.OnShooting(ev);
            ev.Firearm.AmmoDrain = 0;
            ev.Firearm.AddAttachment(AttachmentName.Flashlight);
            if (self)
            {
                if (ev.Firearm.FlashlightEnabled)
                {
                    ev.Player.Scale = ev.Player.Scale * 2;
                }
                else
                {
                    ev.Player.Scale = ev.Player.Scale / 2;
                }
            }
        }

        public static IEnumerator<float> LightCheck()
        {
            float delay = 0.1f;
            while (true)
            {
                try
                {
                    firearm.MagazineAmmo = 2;

                    owner.SetAmmo(AmmoType.Nato9, 1);

                    firearm.Inaccuracy = 0f;
                    firearm.AmmoDrain = 0;
                    firearm.AddAttachment(AttachmentName.Flashlight);
                }
                catch { }

                if (firearm.Owner.CurrentItem as Firearm == firearm)
                {
                    if (self)
                    {
                        if (firearm.FlashlightEnabled)
                        {
                            owner.ShowHint("Mode:Grow | Target: You", delay + 0.05f);
                        }
                        else
                        {
                            owner.ShowHint("Mode: Shrink | Target: You", delay + 0.05f);
                        }
                    }
                    else
                    {
                        if (firearm.FlashlightEnabled)
                        {
                            owner.ShowHint("Mode: Grow", delay + 0.05f);
                        }
                        else
                        {
                            owner.ShowHint("Mode: Shrink", delay+0.05f);
                        }
                    }

                }
                yield return Timing.WaitForSeconds(delay);
            }
        }
        protected override void OnShot(ShotEventArgs ev)
        {
            base.OnShot(ev);

            if (!self) 
            {
                if (ev.Firearm.FlashlightEnabled)
                {
                    ev.Target.Scale = ev.Target.Scale * 2;
                }
                else
                {
                    ev.Target.Scale = ev.Target.Scale / 2;
                }
            }
        }

        }

    }
