using System.Diagnostics;
using EkuraAltLauncher;
using Igor.Configuration;

ConfigurationManager<Config>.Initialize("config.txt");
ConfigurationManager<Config> instance = ConfigurationManager<Config>.Instance;
string cfgFile = instance.CurrentSettings.EkuraConfigFile;
string ekuraLauncher = instance.CurrentSettings.EkuraExe;

string[] lines = File.ReadAllLines(cfgFile);

if (args[0] == "kill") {
	KillLauncher();
	return 0;
}

int width = int.Parse(args[0]);
int height = int.Parse(args[1]);
string? account;

if (args.Length == 3) {
	account = args[2];
}
else {
	Console.WriteLine("Account:");
	Console.Write("> ");
	account = Console.ReadLine();
}

if (account == null) {
	return 1;
}

File.Delete(cfgFile);

lines = lines.Select(s => {
	string name = s.Split('\t')[0];
	return name switch {
		"WIDTH" => $"WIDTH\t{width}",
		"HEIGHT" => $"HEIGHT\t{height}",
		"LAST_ACCOUNT" => account == "" ? s : $"LAST_ACCOUNT\t{account}",
		_ => s
	};
}).ToArray();

File.WriteAllLines(cfgFile, lines);

KillLauncher();

Process.Start(ekuraLauncher);
return 0;

void KillLauncher() {
	Process[] launcher = Process.GetProcessesByName("ekura_launcher");
	if (launcher.Length > 0) {
		launcher[0].Kill();
		while (!launcher[0].HasExited) {
			Thread.Sleep(20);
		}
	}
}
