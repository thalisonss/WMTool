using System;
using System.Collections.Generic;
using WMTool.Reprocessing.Exceptions;

namespace WMTool.Reprocessing.Models
{
    public class MasterParameterSet
    {
        private readonly IReadOnlyDictionary<string, string> _values;

        public MasterParameterSet(IReadOnlyDictionary<string, string> values)
        {
            _values = values;
        }

        public bool TryGet(string parameterName, out string value)
        {
            return _values.TryGetValue(parameterName, out value);
        }

        public string GetRequired(string parameterName)
        {
            if (!_values.TryGetValue(parameterName, out string value))
            {
                throw new UnresolvedParameterException(parameterName);
            }

            return value;
        }

        public IDictionary<string, object> ToSqlParameterDictionary(IEnumerable<string> parameterNames)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (string name in parameterNames)
            {
                dictionary["@" + name] = (object)GetRequired(name) ?? DBNull.Value;
            }

            return dictionary;
        }
    }
}
