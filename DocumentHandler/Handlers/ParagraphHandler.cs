using DocumentHandler.DTO;
using DocumentHandler.DTO.Paragraphs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocumentHandler.Handlers
{
    internal class ParagraphHandler
    {
        private static readonly List<ResumeParagraph> _paragraphs = new();

        public static void Register(ResumeParagraph element)
        {
            _paragraphs.Add(element);
        }

        public static IReadOnlyList<ResumeParagraph> GetAll() => _paragraphs.AsReadOnly();

        public static ResumeParagraph? GetById(int id) =>
            _paragraphs.FirstOrDefault(e => e.Id == id);

        public static IEnumerable<ResumeParagraph> GetByTag(string tag) =>
            _paragraphs.Where(e => e.ParagraphTag == tag);

        public static bool Remove(int id)
        {
            var element = GetById(id);
            return element != null && _paragraphs.Remove(element);
        }

        public static void Clear() => _paragraphs.Clear();

        public static int Count => _paragraphs.Count;
    }
}
