//
// PowerShellHost.cs
//
// Author:
//       Matt Ward <matt.ward@microsoft.com>
//
// Copyright (c) 2019 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Threading;

namespace MonoDevelop.PackageManagement.PowerShell.ConsoleHost
{
	class PowerShellHost : PSHost
	{
		PowerShellUserInterfaceHost ui = new PowerShellUserInterfaceHost ();

		CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
		CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
		Guid instanceId = Guid.NewGuid ();

		PSObject privateData;

		public PowerShellHost ()
		{
			var powerShellPrivateData = new PowerShellHostPrivateData (ui);
			privateData = new PSObject (powerShellPrivateData);
		}

		public override CultureInfo CurrentCulture => currentCulture;
		public override CultureInfo CurrentUICulture => currentUICulture;
		public override Guid InstanceId => instanceId;
		public override string Name => "Package Manager Host";
		public override PSHostUserInterface UI => ui;
		public override Version Version => new Version (0, 1, 0, 0);

		/// <summary>
		/// Returning a non-null PrivateData object here prevents Write-Error
		/// from sending any error text to the PSHostUserInterface.
		/// </summary>
		public override PSObject PrivateData => privateData;

		public int MaxVisibleColumns {
			get { return ui.MaxVisibleColumns; }
			set { ui.MaxVisibleColumns = value; }
		}

		public override void EnterNestedPrompt ()
		{
		}

		public override void ExitNestedPrompt ()
		{
		}

		public override void NotifyBeginApplication ()
		{
		}

		public override void NotifyEndApplication ()
		{
		}

		public override void SetShouldExit (int exitCode)
		{
		}
	}
}