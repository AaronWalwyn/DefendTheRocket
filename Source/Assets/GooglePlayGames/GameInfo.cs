using System;

/// <summary>
/// File containing information about the game. This is automatically updated by running the
/// platform-appropriate setup commands in the Unity editor (which does a simple search / replace
/// on the IDs in the form "__ID__"). We can check whether any particular field has been updated
/// by checking whether it still retains its initial value - we prevent the constants from being
/// replaced in the aforementioned search/replace by stripping off the leading and trailing "__".
/// </summary>
namespace GooglePlayGames {
public static class GameInfo {

    private const string UnescapedApplicationId = "APPID";
    private const string UnescapedIosClientId = "CLIENTID";

    public const string ApplicationId = "232701583421"; // Filled in automatically
    public const string IosClientId = "232701583421-knq5t70anmgqtcaje9i5f5peksj52ac0.apps.googleusercontent.com"; // Filled in automatically

    public static bool ApplicationIdInitialized() {
        return !ApplicationId.Equals(ToEscapedToken(UnescapedApplicationId));
    }

    public static bool IosClientIdInitialized() {
        return !IosClientId.Equals(ToEscapedToken(UnescapedIosClientId));
    }

    /// <summary>
    /// Returns an escaped token (i.e. one flanked with "__") for the passed token
    /// </summary>
    /// <returns>The escaped token.</returns>
    /// <param name="token">The Token</param>
    private static string ToEscapedToken(string token) {
        return string.Format("__{0}__", token);
    }

}
}


