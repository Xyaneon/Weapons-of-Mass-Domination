namespace WMD.Game.Planets
{
    /// <summary>
    /// Represents Earth as a playable planet.
    /// </summary>
    public class Earth : Planet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Earth"/> class.
        /// </summary>
        /// <remarks>
        /// This creates a relatively realistic depiction of Earth using
        /// figures from https://en.wikipedia.org/wiki/Earth .
        /// </remarks>
        public Earth() : base(148940000, 510072000, 361132000) { }
    }
}
