namespace EMS.ConfigurationProviders.Tests.Models
{
    public class SampleConfiguration
    {
        public string Name { get; set; }
        public NestedConfiguration Nested { get; set; }
    }

    public class NestedConfiguration
    {
        public string Name { get; set; }
    }
}