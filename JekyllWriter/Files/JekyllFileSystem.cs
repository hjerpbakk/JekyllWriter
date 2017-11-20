using System;
using System.Linq;
using System.IO;
using JekyllWriter.Model;
using JekyllWriter.Parsers;

namespace JekyllWriter.Files
{
    public class JekyllFileSystem
    {
        readonly string postsPath;
        readonly string draftsPath;
        readonly PostParser parser;

        public JekyllFileSystem(string jekyllPath)
        {
            // TODO: if exsists and such
            postsPath = Path.Combine(jekyllPath, "_posts");
            draftsPath = Path.Combine(jekyllPath, "_drafts");
            parser = new PostParser();
        }

        public Folder GetPosts() => GetPostFiles(postsPath, "Posts");

        public Folder GetDrafts() => GetPostFiles(draftsPath, "Drafts");

        public Post ReadPost(Model.SourceFile file) => parser.Parse(System.IO.File.ReadAllText(file.Path), file);

        public void SavePost(Post post) => System.IO.File.WriteAllText(post.File.Path, post.ToString());
 
        // TODO: parse date and show properly, indicate filetype through icon. Sorting should stay the same
        Folder GetPostFiles(string path, string name) {
            var files = Directory.EnumerateFiles(path);
            var posts = from p in files
                        let fileName = Path.GetFileName(p)
                        where fileName != ".DS_Store"
                        orderby fileName descending
                        select new Model.SourceFile(fileName, Path.Combine(path, p));
            return new Folder(name, posts.ToArray());
        }
    }
}
