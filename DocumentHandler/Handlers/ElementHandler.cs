using DocumentHandler.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocumentHandler.Handlers
{
    internal class ElementHandler
    {
        private static readonly List<Element> _elements = new();

        public static void Register(Element element)
        {
            _elements.Add(element);
        }

        public static IReadOnlyList<Element> GetAll() => _elements.AsReadOnly();

        public static Element? GetById(int id) =>
            _elements.FirstOrDefault(e => e.Id == id);

        public static IEnumerable<Element> GetByTag(string tag) =>
            _elements.Where(e => e.Tag == tag);

        public static bool Remove(int id)
        {
            var element = GetById(id);
            return element != null && _elements.Remove(element);
        }

        public static void Clear() => _elements.Clear();

        public static int Count => _elements.Count;
    }
}
