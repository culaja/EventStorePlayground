namespace Common
{
    /// <summary>
    /// This class should be used instead of void for return types.
    /// </summary>
    public class Nothing
    {
        public static readonly Nothing NotAtAll = new Nothing();

        public override string ToString() => "Nothing";
    }
}