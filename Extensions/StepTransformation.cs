using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowOctoberBatchPOM.Extensions
{
    [Binding]
    public class StepTransformation
    {
        [StepArgumentTransformation]
        public Dictionary<string, string> withDictionary(Table table)
        {
            var expected = table.Rows
                .ToDictionary(key => key.Keys.FirstOrDefault(), 
                value => value.Values.FirstOrDefault());
            return expected;
        }

        [StepArgumentTransformation]
        public Dictionary<List<string>, List<string>> withDictionaryList(Table table)
        {
            var expected = table.Rows
                .ToDictionary(key => key.Keys.ToList(), 
                value => value.Values.ToList());
            return expected;
        }
    }
}
