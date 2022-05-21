// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


namespace MonoDevelop.PackageManagement.PowerShell.ConsoleHost
{
	public class Program
	{
		/// <summary>
		/// Managed entry point shim, which starts the actual program.
		/// </summary>
		public static int Main (string[] args)
		{
			var host = new PowerShellConsoleHost ();
			host.Run ();

			return 0;
		}
	}
}