namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Metody statyczne służące do pracy na stringach
    /// </summary>
    public static class StringUtilities
    {
        /// <summary>
        /// Formatuje tekst do wyświetlenia jako dystans lub wysokość
        /// </summary>
        /// <param name="input">Opisywana wielkość</param>
        /// <returns>Sformatowany tekst</returns>
        public static string FormatToMeter(this float input)
            => input.ToString("F2").Replace(",", ".") + "m";
    }
}