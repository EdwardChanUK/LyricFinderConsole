namespace LyricFinderCore.Exceptions
{
    public class InvalidPartialNameException: Exception
    {
        public InvalidPartialNameException():base("The partial name is invalid.")
        {
        }
    }
}
