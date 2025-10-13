namespace YNQ.JumpyJoe
{
    public static class StringUtilities
    {
        public static string FormatToMeter(this float input)
            => input.ToString("F2").Replace(",", ".") + "m";
    }
}