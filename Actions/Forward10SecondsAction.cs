using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using WNPReduxAdapterLibrary;

namespace jbcarreon123.WebNowPlayingPlugin.Actions
{
    public class Forward10SecondsAction : PluginAction
    {
        public override string Name => "Forward 10s";

        public override string Description => "Advance the active media by 10 seconds";

        public override bool CanConfigure => false;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (WNPRedux.MediaInfo.Controls.SupportsSetPosition)
            {
                WNPRedux.MediaInfo.Controls.TryForwardPositionSeconds(10);
            }
        }
    }
}
