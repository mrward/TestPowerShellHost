//
// PowerShellHostPrivateData.cs
//
// Author:
//       Matt Ward <matt.ward@microsoft.com>
//
// Copyright (c) 2022 Microsoft
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

namespace MonoDevelop.PackageManagement.PowerShell.ConsoleHost
{
	public class PowerShellHostPrivateData
	{
        readonly PowerShellUserInterfaceHost ui;

        public PowerShellHostPrivateData (PowerShellUserInterfaceHost ui)
        {
            this.ui = ui;
        }

        public ConsoleColor FormatAccentColor {
            get {
                return ui.FormatAccentColor;
            }

            set {
                ui.FormatAccentColor = value;
            }
        }

        public ConsoleColor ErrorAccentColor {
            get {
                return ui.ErrorAccentColor;
            }

            set {
                ui.ErrorAccentColor = value;
            }
        }

        public ConsoleColor ErrorForegroundColor {
			get {
                return ui.ErrorForegroundColor;
            }

            set {
                ui.ErrorForegroundColor = value;
            }
        }

        public ConsoleColor ErrorBackgroundColor {
            get {
                return ui.ErrorBackgroundColor;
            }

            set {
                ui.ErrorBackgroundColor = value;
            }
        }

        public ConsoleColor WarningForegroundColor {
            get {
                return ui.WarningForegroundColor;
            }

            set {
                ui.WarningForegroundColor = value;
            }
        }

        public ConsoleColor WarningBackgroundColor {
            get {
                return ui.WarningBackgroundColor;
            }

            set {
                ui.WarningBackgroundColor = value;
            }
        }

        public ConsoleColor DebugForegroundColor {
            get {
                return ui.DebugForegroundColor;
            }

            set {
                ui.DebugForegroundColor = value;
            }
        }

        public ConsoleColor DebugBackgroundColor {
            get {
                return ui.DebugBackgroundColor;
            }

            set {
                ui.DebugBackgroundColor = value;
            }
        }

        public ConsoleColor VerboseForegroundColor {
             get {
                return ui.VerboseForegroundColor;
            }

            set {
                ui.VerboseForegroundColor = value;
            }
        }

        public ConsoleColor VerboseBackgroundColor {
            get {
                return ui.VerboseBackgroundColor;
            }

            set {
                ui.VerboseBackgroundColor = value;
            }
        }

        public ConsoleColor ProgressForegroundColor {
            get {
                return ui.ProgressForegroundColor;
            }

            set {
                ui.ProgressForegroundColor = value;
            }
        }

        public ConsoleColor ProgressBackgroundColor {
            get {
                return ui.ProgressBackgroundColor;
            }

            set {
                ui.ProgressBackgroundColor = value;
            }
        }
    }
}

