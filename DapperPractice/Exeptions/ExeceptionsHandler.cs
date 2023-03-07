namespace DapperPractice.Exeptions
{
    public abstract class ExeceptionsHandler : Exception
    { 
        protected ExeceptionsHandler(string message) : base(message) { }
    }
}
