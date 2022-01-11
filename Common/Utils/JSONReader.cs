using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class JSONReader
{
    public static T ReadFromFile<T>(string filename)
    {
        var fileContent = File.ReadAllText("Content/" + filename);

        return JsonConvert.DeserializeObject<T>(fileContent);
    }
}
