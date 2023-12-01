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
            bool notNull = true)
        {
            Name = name;
            Value = value;
            NotNull = notNull;
            _statement = statement;
        }

        public string Name { get; set; }

        public T? Value { get; set; }

        public bool NotNull { get; set; }

        public void SetStatement(Func<string, string> statement)
        {
            _statement = statement;
        }

        public (string?, DynamicParameters?) Build()
        {
            if (NotNull && Value == null)
            {
                return (null, null);
            }
            DynamicParameters parameters = new();
            string name = Name.StartsWith('@') ? Name : $"@{Name}";
            parameters.Add(name, Value);
            string statement = _statement.Invoke(name);
            return (statement, parameters);
        }
    }
}
