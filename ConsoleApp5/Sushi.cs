using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progect
{
    public class Item
    {
        [JsonProperty("Header")]
        public string Header { get; set; }

        [JsonProperty("items")]
        public List <Sushi> items { get; set; }
    }

    public class Sushi
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

    }
    
}

