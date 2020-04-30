using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DesignApp.Tests
{
    public static class InMemoryHelpers
    {
        public static T Clone<T>(T entity)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(entity));
        }
    }
}
