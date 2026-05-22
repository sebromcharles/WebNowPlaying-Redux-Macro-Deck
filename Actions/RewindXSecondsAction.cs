using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using WNPReduxAdapterLibrary;

namespace jbcarreon123.WebNowPlayingPlugin.Actions
{
    public class RewindXSecondsAction : PluginAction
    {
        public override string Name => "Rewind";

        public override string Description => "Rewind the active media by a configured number of seconds";

        public override bool CanConfigure => true;

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SeekSecondsActionConfigControl(this, actionConfigurator);
        }

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (WNPRedux.MediaInfo.Controls.SupportsSetPosition)
            {
                WNPRedux.MediaInfo.Controls.TryRevertPositionSeconds(
                    SeekSecondsActionConfigControl.GetSeconds(Configuration));
            }
        }
    }
}
