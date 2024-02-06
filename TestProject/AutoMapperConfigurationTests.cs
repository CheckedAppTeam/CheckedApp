using AutoMapper;
using CheckedAppProject.LOGIC.AutoMapperProfiles;

[TestFixture]
public class AutoMapperConfigurationTests
{
    [Test]
    public void AutoMapper_Configuration_IsValid()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ItemListMappingProfile());
            cfg.AddProfile(new UserMappingProfile());
        });

        mapperConfiguration.AssertConfigurationIsValid();
    }
}
