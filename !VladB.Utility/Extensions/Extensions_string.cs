namespace VladB.Utility {
    public static partial class Extensions {
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
        public static bool IsHaveSomething(this string s) => !string.IsNullOrEmpty(s);
    }
}

