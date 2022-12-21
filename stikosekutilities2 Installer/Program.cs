using Pastel;
using System;
using System.Collections.Generic;

namespace stikosekutilities2_Installer
{
	public static class Program
	{

		public static void Main(string[] args)
		{

			WriteLine(@"
         __  _ __                   __         __  _ ___ __  _          
   _____/ /_(_) /______  ________  / /____  __/ /_(_) (_) /_(_)__  _____
  / ___/ __/ / //_/ __ \/ ___/ _ \/ //_/ / / / __/ / / / __/ / _ \/ ___/
 (__  ) /_/ / ,< / /_/ (__  )  __/ ,< / /_/ / /_/ / / / /_/ /  __(__  ) 
/____/\__/_/_/|_|\____/____/\___/_/|_|\__,_/\__/_/_/_/\__/_/\___/____/", ConsoleColor.DarkCyan);

			Console.CursorTop++;
			WriteLine("Installer made by JNNJ", ConsoleColor.Blue);

			List<string> versions = new()
			{ 
				"stikosekutilities  - \"Old but gold\"", 
				"stikosekutilities2 - Less features but newer" 
			};

			Console.CursorTop += 3;

			int index = InteractiveMenu("Select a Version: ", versions.ToArray());

			string suVersionName = versions[index].Substring(0, versions[index].LastIndexOf(" -")).Trim();

			string owner = index == 0 ? "stikosek" : "Dev-Packet";
			string repo = index == 0 ? "stikosekutilities" : "stikosekutilities2";
			string fileName = index == 0 ? "stikosekutilitites.dll" : "stikosekutilities2.dll";

			StikosekUtilities su = new()
			{
				FileName = fileName,
				Owner = owner,
				Repo = repo
			};

			string muckPath = SteamUtils.GetAppLocation(1625450, "Muck");

			if (muckPath == null)
			{
				Console.WriteLine("Muck folder was not found. Enter your Muck Path manually here.");
				Console.WriteLine($"({ "Steam -> Right click Muck -> Manage -> Browse local Files".Pastel(ConsoleColor.Yellow) })");

				muckPath = Console.ReadLine();

				if (muckPath.StartsWith("\""))
					muckPath = muckPath.Substring(index + 1);

				if (muckPath.EndsWith("\""))
					muckPath = muckPath.Substring(0, muckPath.Length - 1);
			}

			bool exit = false;
			while (!exit)
			{
				int initialCursorLeft = Console.CursorLeft;
				int initialCursorTop = Console.CursorTop;

				exit = MainMenu(muckPath, su, suVersionName);

				if (!exit)
				{
					Console.CursorTop++;
					Console.WriteLine("Press any key to go back...");
					Console.ReadKey(true);
				}

				int tempCursorTop = Console.CursorTop;

				for (int i = initialCursorTop; i < tempCursorTop; i++)
				{
					Console.CursorLeft = 0;
					Console.CursorTop = i;
					Console.Write(new string(' ', Console.WindowWidth));
				}

				Console.CursorTop = initialCursorTop;
				Console.CursorLeft = initialCursorLeft;
			}
			
		}

		private static bool MainMenu(string muckPath, StikosekUtilities su, string suVersionName)
		{
			List<string> actions = new();

			bool installed = su.GetInstalledVersion(muckPath) != null;
			bool updateAvailable = su.CheckForUpdate(muckPath);


			if (installed)
			{
				actions.Add("Show version");
				actions.Add("Uninstall");
			}
			else
				actions.Add("Install");

			if (installed && updateAvailable)
				actions.Add("Update");

			actions.Add("Exit");

			int action = InteractiveMenu("Select an action: ", actions.ToArray());

			if (actions[action].Equals("Show version"))
			{
				Console.WriteLine("Installed version: " + su.GetInstalledVersion(muckPath).ToString().Pastel(updateAvailable ? ConsoleColor.Red : ConsoleColor.Green));

				try
				{
					Version latest = su.GetLatestVersion();

					Console.WriteLine("Latest version:    " + latest.ToString().Pastel(ConsoleColor.Green));

				} catch(Exception)
				{
					WriteLine("Couldn't get latest version number from github. (So probably just ignore this...)", ConsoleColor.Red);
				}

				return false;
			}

			if (actions[action].Equals("Uninstall"))
			{
				Console.WriteLine($"Uninstalling {suVersionName}...");
				su.Uninstall(muckPath);
				Console.WriteLine($"{suVersionName.Pastel(ConsoleColor.DarkCyan)} has been uninstalled sucessfully.");

				return false;
			}

			if (actions[action].Equals("Install"))
			{
				Console.WriteLine($"Installing {suVersionName}...");
				string actionDone = su.Install(muckPath);
				Console.WriteLine($"{suVersionName.Pastel(ConsoleColor.DarkCyan)} has been {actionDone} sucessfully.");

				return false;
			}

			if (actions[action].Equals("Install"))
			{
				Console.WriteLine($"Installing {suVersionName}...");
				string actionDone = su.Install(muckPath);
				Console.WriteLine($"{suVersionName.Pastel(ConsoleColor.DarkCyan)} has been {actionDone} sucessfully.");

				return false;
			}

			if (actions[action].Equals("Update"))
			{
				Console.WriteLine($"Updating {suVersionName}...");

				string actionDone = su.Install(muckPath);
				Console.WriteLine($"{suVersionName.Pastel(ConsoleColor.DarkCyan)} has been {actionDone} sucessfully.");

				return false;
			}

			if (action == actions.Count - 1)
			{
				return true;
			}

			return false;
		}

		private static void DrawMenu(string[] options, int selectedIndex)
		{
			int initialCursorLeft = Console.CursorLeft;
			int initialCursorTop = Console.CursorTop;
			for (int i = 0; i < options.Length; i++)
			{
				Console.CursorLeft = 0;
				Console.CursorTop = initialCursorTop + i;

				string message = " " + options[i];

				if (i == selectedIndex)
				{
					message = (">" + message).Pastel(ConsoleColor.Green);
				}
				else
					message = " " + message;

				Console.WriteLine(message);

			}

			Console.CursorTop = initialCursorTop;
			Console.CursorLeft = initialCursorLeft;
		}

		private static int InteractiveMenu(string message, string[] options, int initialIndex = 0)
		{
			bool initialCursorVisible = Console.CursorVisible;
			int initialCursorLeft = Console.CursorLeft;
			int initialCursorTop = Console.CursorTop;

			Console.CursorVisible = false;
			Console.WriteLine(message);

			int index = initialIndex;
			bool menuOpen = true;

			ConsoleKeyInfo keyInfo;

			DrawMenu(options, index);

			while (menuOpen)
			{
				keyInfo = Console.ReadKey(true);

				switch (keyInfo.Key)
				{
					case ConsoleKey.DownArrow:
						index++;

						if (index >= options.Length)
							index = 0;

						break;

					case ConsoleKey.UpArrow:
						index--;

						if (index < 0)
							index = options.Length - 1;

						break;

					case ConsoleKey.Enter:
						menuOpen = false;
						break;
				}

				DrawMenu(options, index);

			}

			Console.CursorVisible = initialCursorVisible;
			Console.CursorTop = initialCursorTop + options.Length + 2;
			Console.CursorLeft = initialCursorLeft;

			return index;
		}

		private static void WriteLine(string message, ConsoleColor color)
		{
			Console.WriteLine(message.Pastel(color));
		}

	}
}
