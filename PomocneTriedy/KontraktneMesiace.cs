namespace SpreadCalculator.PomocneTriedy
{
    public static class KontraktneMesiace
    {
        public static string Symbol { get; set; }
        public static string Nazov { get; set; }

        public static string GetPopis(string mesiac)
        {
            switch (mesiac)
            {
                case "F":
                    return "F  - January";
                case "G":
                    return "G  - February";
                case "H":
                    return "H  - March";
                case "J":
                    return "J  - April";
                case "K":
                    return "K  - May";
                case "M":
                    return "M  - June";
                case "N":
                    return "N  - July";
                case "Q":
                    return "Q  - August";
                case "U":
                    return "U  - September";
                case "V":
                    return "V  - October";
                case "X":
                    return "X  - November";
                case "Z":
                    return "Z  - December";
                default :
                    return null;
            }
        }
    }
}
