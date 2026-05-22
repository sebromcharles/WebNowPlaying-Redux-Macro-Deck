using System;
using System.Drawing;
using System.Windows.Forms;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace jbcarreon123.WebNowPlayingPlugin.Actions
{
    internal class SeekSecondsActionConfigControl : ActionConfigControl
    {
        public const int DefaultSeconds = 10;
        private const int MinimumSeconds = 1;
        private const int MaximumSeconds = 3600;

        private readonly PluginAction _macroDeckAction;
        private readonly NumericUpDown _secondsInput;

        public SeekSecondsActionConfigControl(PluginAction macroDeckAction, ActionConfigurator actionConfigurator)
        {
            _macroDeckAction = macroDeckAction;

            Dock = DockStyle.Fill;

            var label = new Label
            {
                AutoSize = true,
                Location = new Point(0, 0),
                Text = "Seconds"
            };

            _secondsInput = new NumericUpDown
            {
                Location = new Point(0, label.Bottom + 8),
                Minimum = MinimumSeconds,
                Maximum = MaximumSeconds,
                Value = GetSeconds(macroDeckAction.Configuration),
                Width = 120
            };

            Controls.Add(label);
            Controls.Add(_secondsInput);
        }

        public override bool OnActionSave()
        {
            int seconds = decimal.ToInt32(_secondsInput.Value);
            _macroDeckAction.Configuration = seconds.ToString();
            _macroDeckAction.ConfigurationSummary = $"{seconds} seconds";
            return true;
        }

        public static int GetSeconds(string configuration)
        {
            if (!int.TryParse(configuration, out int seconds))
            {
                return DefaultSeconds;
            }

            return Math.Min(Math.Max(seconds, MinimumSeconds), MaximumSeconds);
        }
    }
}
