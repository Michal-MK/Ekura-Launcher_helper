using Igor.Configuration;

namespace EkuraAltLauncher; 

internal class Config : IConfiguration {

	public string EkuraConfigFile { get; set; }
	public string EkuraExe { get; set; }

	public string ConfigurationHeader() => "Configuration file for EkuraAltLauncher";
}