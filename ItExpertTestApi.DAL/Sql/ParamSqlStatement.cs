using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class ParamSqlStatement<T> : ISqlStatement
    {
        private Func<string, string> _statement;

        public ParamSqlStatement(
            string name,
            T? value,
            Func<string, string> statement,
            string? nullStatement = null)
        {
            Name = name;
            Value = value;
            NullStatement = nullStatement;
            _statement = statement;
        }

        public string Name { get; set; }

        public T? Value { get; set; }

        public string? NullStatement { get; set; }

        public void SetStatement(Func<string, string> statement)
        {
            _statement = statement;
        }

        public (string?, DynamicParameters?) Build()
        {
            if (Value == null)
            {
                return (NullStatement, null);
            }
            DynamicParameters parameters = new();
            string name = Name.StartsWith('@') ? Name : $"@{Name}";
            parameters.Add(name, Value);
            string statement = _statement.Invoke(name);
            return (statement, parameters);
        }
    }
}
