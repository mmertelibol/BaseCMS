using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Common.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString Get(string key)
        {
            return _localizer[key];
        }

        public IEnumerable<LocalizedString> GetAll(CultureInfo cult)
        {
            var resourceSet = _localizer.WithCulture(cult).GetAllStrings();
            return resourceSet;
        }
    }
}