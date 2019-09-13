using System;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace LightSwitch.Ide.VS
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(LightSwitchPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(LightSwitch.Ide.VS.Main))]
    public sealed class LightSwitchPackage : AsyncPackage
    {
         /// <summary>
        /// LightSwitch.Ide.VSPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "dd23b846-7c4a-4edf-8cd6-1a3e80b437f0";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            base.Initialize();
            await LightSwitch.Ide.VS.MainCommand.InitializeAsync(this);
        }
    }
}
