namespace DapperPractice.Exeptions
{
    public sealed class ExeceptionNotFound : ExeceptionsHandler
    { //sealed is use when you dont need to do inheritance
        public ExeceptionNotFound(string message) : base(message)
        {
        }
    }
}
