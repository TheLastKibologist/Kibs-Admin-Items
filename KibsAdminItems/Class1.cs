using Exiled;
using Exiled.API;
using Exiled.API.Features;
using Exiled.CustomItems;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events;
using Exiled.Events.Handlers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyAdminItems
{
    public class Class1 : Plugin<Config>
    {
        public static Class1 instance;

        public override string Name => "KibsSillyAdminItems";
        public override string Author => "Kibs";
        public override Version Version => new Version(1,0,0);
        public override void OnEnabled()
        {

            instance = this;
            CustomItem.RegisterItems(false);

            base.OnEnabled();
        }
        public override void OnDisabled()
        {

            instance = null;
            CustomItem.UnregisterItems();
            base.OnDisabled();
        }

        public override void OnReloaded()
        {
            instance = this;
            base.OnReloaded();
        }
    }
}
