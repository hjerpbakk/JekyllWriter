using System;
using System.Linq;
using AppKit;
using JekyllWriter.Model;

namespace JekyllWriter.ViewControllers
{
    public class PreambleController
    {


        public PreambleController(NSForm form, Preamble preamble)
        {
            var propName = nameof(preamble.title);
            var cell = form.Cells.Where(c => c.Title == propName).Single(); 
        }
    }
}
