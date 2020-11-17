namespace SqlServer.Commands
{
    public interface ISqlCommandDefinition
    {
    }

    public interface ISqlCommandDefinition<TReturn> : ISqlCommandDefinition
    {
    }
}
