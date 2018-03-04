using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiContainer.Controllers
{
    [Route("api/[controller]")]
    public class ItemContainer
    {
        Dictionary<string, string> _itemsCatelog = new Dictionary<string, string>();
        //Add caching support
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _itemsCatelog.Select((x, y) => $"Key;{x.Key}, Value :{x.Value}");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string value = string.Empty;
            if (!_itemsCatelog.TryGetValue(id, out value))
                throw new Exception("Key not found exception");
            return value;
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromQuery]string key, [FromQuery]string value)
        {
            if (!_itemsCatelog.TryAdd(key,  value))
                throw new Exception("failed to add new value");
            return true;
        }

        // PUT api/values/5
        [HttpPut("{key}")]
        public bool Put(string key, [FromQuery]string value)
        {
            if (!_itemsCatelog.ContainsKey(key))
                throw new Exception("Key doesnt exist");

            if (!_itemsCatelog.TryAdd(key,  value))
                throw new Exception("Key not found exception");

            return true;
        }

        // DELETE api/values/5
        [HttpDelete("{key}")]
        public void Delete(string key)
        {
            if (!_itemsCatelog.ContainsKey(key))
                throw new Exception("Key doesnt exist");

            _itemsCatelog.Remove(key);
        }
    }
}
