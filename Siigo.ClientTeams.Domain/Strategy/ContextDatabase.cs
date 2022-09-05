using Siigo.ClientTeams.Domain.Models;
using System.Collections.Generic;

namespace Siigo.ClientTeams.Domain.Strategy
{
    public class ContextDatabase
    {
        private IDatabaseStrategy _databaseStrategy;

        public ContextDatabase() { }

        public ContextDatabase(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
        }

        public void SetStrategy(IDatabaseStrategy databaseStrategy)
        {
            _databaseStrategy = databaseStrategy;
        }

        public IEnumerable<Fact> GetFacts()
        {
            return _databaseStrategy.BuildFacts();
        }
    }

}
