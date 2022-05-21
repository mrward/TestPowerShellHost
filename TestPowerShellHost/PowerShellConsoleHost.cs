//
// PowerShellConsoleHost.cs
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

using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace MonoDevelop.PackageManagement.PowerShell.ConsoleHost
{
	class PowerShellConsoleHost
	{
		static PowerShellConsoleHost instance;

		Runspace runspace;
		PowerShellHost host;
		Pipeline currentPipeline;

		public static PowerShellConsoleHost Instance => instance;

		public PowerShellConsoleHost ()
		{
			instance = this;
		}

		public void Run ()
		{
			Logger.Log ("PowerShellConsoleHost starting...\n");

			host = new PowerShellHost ();

			var initialSessionState = CreateInitialSessionState ();
			runspace = RunspaceFactory.CreateRunspace (host, initialSessionState);
			runspace.Open ();

			Logger.Log ("PowerShellConsoleHost running...\n");

			Logger.Log ("Call Write-Error\n");

			InvokePowerShellInternal ("Write-Error 'test'\n");

			Logger.Log ("Run unknown command\n");

			InvokePowerShellInternal ("UnknownCommandTest");

			Logger.Log ("Enter commands:\n");

			while (true) {
				string line = Console.ReadLine ();
				if (line != null) {
					InvokePowerShellInternal (line);
				} else {
					return;
				}
			}
		}

		InitialSessionState CreateInitialSessionState ()
		{
			var initialSessionState = InitialSessionState.CreateDefault ();
			return initialSessionState;
		}

		void InvokePowerShellInternal (string line, params object[] input)
		{
			try {
				using (var pipeline = CreatePipeline (runspace, line)) {
					currentPipeline = pipeline;
					pipeline.Invoke (input);
					CheckPipelineState (pipeline);
				}
			} catch (Exception ex) {
				string errorMessage = ex.GetBaseException ().ToString ();
				Logger.Log ("ERROR: " + errorMessage + Environment.NewLine);
			} finally {
				currentPipeline = null;
			}
		}

		static Pipeline CreatePipeline (Runspace runspace, string command)
		{
			Pipeline pipeline = runspace.CreatePipeline ();
			pipeline.Commands.AddScript (command, false);
			//pipeline.Commands.Add ("out-default");
			pipeline.Commands.Add ("out-host"); // Ensures native command output goes through the HostUI.
			pipeline.Commands[0].MergeMyResults (PipelineResultTypes.Error, PipelineResultTypes.Output);
			return pipeline;
		}

		void CheckPipelineState (Pipeline pipeline)
		{
			switch (pipeline.PipelineStateInfo?.State) {
				case PipelineState.Completed:
				case PipelineState.Stopped:
				case PipelineState.Failed:
					if (pipeline.PipelineStateInfo.Reason != null) {
						ReportError (pipeline.PipelineStateInfo.Reason);
					}
					break;
			}
		}

		void ReportError (Exception exception)
		{
			exception = exception.GetBaseException ();
			Logger.Log (exception.Message + Environment.NewLine);
		}

		Collection<PSObject> InvokePowerShellNoOutput (string line, params object[] input)
		{
			try {
				using (Pipeline pipeline = runspace.CreatePipeline ()) {
					pipeline.Commands.AddScript (line, false);
					currentPipeline = pipeline;
					return pipeline.Invoke (input);
				}
			} catch (Exception ex) {
				string errorMessage = ex.GetBaseException().ToString ();
				Logger.Log (errorMessage);
			} finally {
				currentPipeline = null;
			}
			return null;
		}
	}
}