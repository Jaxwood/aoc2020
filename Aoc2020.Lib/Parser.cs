using System.IO;
using System.Linq;
using System.Collections;

public class Parser
{
  private readonly string path;

  public Parser(string path)
  {
    this.path = path;
  }

  public IEnumerable<T> Parse<T>(IParseFactory factory)
  {
    var result = new List<T>();
    var file = new StreamReader(this.path);  
    while((line = file.ReadLine()) != null)  
    {  
      result.Add(factory.Create(line));
    }  
    file.Close();  

    return result;
  }
}

public interface IParseFactory
{
  T Create<T>(string raw);
}