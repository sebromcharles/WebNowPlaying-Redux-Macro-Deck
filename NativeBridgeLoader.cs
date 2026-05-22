using System;
using System.IO;
using System.Linq;
using System.Reflection;
using SuchByte.MacroDeck.Logging;

namespace jbcarreon123.WebNowPlayingPlugin
{
    internal static class NativeBridgeLoader
    {
        private const string BridgeAssemblyFile = "jbcarreon123.WebNowPlayingPlugin.NativeBridge.dll";
        private const string BridgeTypeName = "jbcarreon123.WebNowPlayingPlugin.WNPReduxNative";

        private static bool _startAttempted;

        public static void Start(int port, Main plugin)
        {
            if (_startAttempted)
            {
                return;
            }

            _startAttempted = true;

            try
            {
                string pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    ?? AppContext.BaseDirectory;
                string bridgePath = Path.Combine(pluginDirectory, BridgeAssemblyFile);

                if (!File.Exists(bridgePath))
                {
                    MacroDeckLogger.Warning(plugin, $"Native WebNowPlaying bridge was not found: {BridgeAssemblyFile}");
                    return;
                }

                Assembly bridgeAssembly = Assembly.LoadFrom(bridgePath);
                Type bridgeType = bridgeAssembly.GetType(BridgeTypeName, throwOnError: true);
                MethodInfo startMethod = bridgeType.GetMethod("Start", BindingFlags.Public | BindingFlags.Static)
                    ?? throw new MissingMethodException(BridgeTypeName, "Start");

                startMethod.Invoke(null, new object[] { port });
                MacroDeckLogger.Info(plugin, "Native WebNowPlaying bridge enabled.");
            }
            catch (TargetInvocationException ex)
            {
                MacroDeckLogger.Warning(plugin, $"Native WebNowPlaying bridge failed to start: {ex.InnerException ?? ex}");
            }
            catch (ReflectionTypeLoadException ex)
            {
                string loaderErrors = string.Join(Environment.NewLine,
                    ex.LoaderExceptions.Where(loaderException => loaderException != null));
                MacroDeckLogger.Warning(plugin, $"Native WebNowPlaying bridge failed to load:{Environment.NewLine}{loaderErrors}");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Warning(plugin, $"Native WebNowPlaying bridge is unavailable:{Environment.NewLine}{ex}");
            }
        }
    }
}
