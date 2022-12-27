using WMD.Game.Commands;

namespace WMD.Console.UI.Commands;

class LaunchNukesResultPrinter : CommandResultPrinter<LaunchNukesResult>
{
    private const string LaunchedNukesFormatString = "{0} launched {1:N0} nukes at {2}.";
    private const string NoNukesHitFormatString = "Not a single one of them worked, though! {0}'s henchmen escaped without a scratch.";
    private const string SomeNukesHitFormatString = "{0:N0} of the nukes worked as designed. {1} lost {2:N0} henchmen in the attack.";
    private const string AllNukesHitFormatString = "Every nuke successfully detonated on contact! {0} lost {1:N0} henchmen in the attack.";

    public override void PrintCommandResult(LaunchNukesResult result)
    {
        string launchedNukesFormattedString = string.Format(
            LaunchedNukesFormatString,
            RetrieveNameOfPlayerWhoActed(result),
            result.NukesLaunched,
            result.TargetPlayerName
        );
        System.Console.WriteLine(launchedNukesFormattedString);
        System.Console.WriteLine(createFormattedStringForHowManyNukesHit(result));
    }

    private static string createFormattedStringForHowManyNukesHit(LaunchNukesResult result) => result.SuccessfulNukeHits switch
    {
        int hits when hits == 0 => string.Format(NoNukesHitFormatString, result.TargetPlayerName),
        int hits when hits < result.NukesLaunched => string.Format(SomeNukesHitFormatString, result.SuccessfulNukeHits, result.TargetPlayerName, result.HenchmenDefenderLost),
        _ => string.Format(AllNukesHitFormatString, result.TargetPlayerName, result.HenchmenDefenderLost)
    };
}
