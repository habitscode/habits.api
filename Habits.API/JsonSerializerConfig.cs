using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Habits.API
{
    public class JsonSerializerConfig
    {
        public static readonly JsonSerializerSettings settings = new JsonSerializerSettings() {
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}
