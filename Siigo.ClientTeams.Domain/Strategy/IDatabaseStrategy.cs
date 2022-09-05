using Siigo.ClientTeams.Domain.Models;
using System.Collections.Generic;

namespace Siigo.ClientTeams.Domain.Strategy
{
    public interface IDatabaseStrategy
    {
        public IEnumerable<Fact> BuildFacts();
    }

}
