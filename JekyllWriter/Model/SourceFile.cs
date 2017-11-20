using System;
using Foundation;

namespace JekyllWriter.Model
{
    public class SourceFile : NSObject
    {
        public SourceFile(string name, string path)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name)); 
            Path = path ?? throw new ArgumentNullException(nameof(path)); 
        }

        public string Name { get;  }
        public string Path { get;  }
    }
}
