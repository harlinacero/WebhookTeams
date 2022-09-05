using Siigo.ClientTeams.Domain.Models;
using System.ComponentModel;

namespace Siigo.ClientTeams.Domain.Strategy
{
    public abstract class ConnectionBase
    {

        protected readonly List<Fact> facts;
        protected readonly MessageBody messageBody;
        public ConnectionBase(MessageBody messageBody)
        {
            this.messageBody = messageBody;
            facts = messageBody.Sections[0].Facts != null ? messageBody.Sections[0].Facts.ToList() : new();
        }

        protected List<Fact> GetFacts(object jsonObject)
        {
            foreach (PropertyDescriptor field in TypeDescriptor.GetProperties(jsonObject))
            {
                if (field.Name.Equals("_Id", StringComparison.OrdinalIgnoreCase))
                    continue;

                var value = field.GetValue(jsonObject);
                var fact = new Fact() { Name = field.Name, Value = value != null ? value.ToString() : value };
                facts.Add(fact);
            }

            return facts;
        }
    }

}
