using System;
using System.ComponentModel.Design;
using System.Globalization;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VisualStudioExtensions.GenerateClass;

namespace VisualStudioExtensions
{

    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class GenerateClassCommand
    {

        #region <Generated>

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("78d10fc0-0a64-4c37-815f-f76d04846f4f");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateClassCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private GenerateClassCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static GenerateClassCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new GenerateClassCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>

        #endregion
         
        IGenerateClassInputDataProvider _inputDataProvider = new GenerateClassInputDataProvider();
        IClassGeneratorEngine _classGeneratorEngine = new ClassGeneratorEngine(new PropertyTypeResolver());

        private void MenuItemCallback(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var dte = (DTE2)ServiceProvider.GetService(typeof(DTE));
            Assumes.Present(dte);

            var document = dte.ActiveDocument;
            var textDocument = document.Object() as TextDocument;
            var selection = textDocument.Selection;
            var selectionText = selection.Text;

            var inputData = _inputDataProvider.GetInputData(selectionText);
            var code = _classGeneratorEngine.Generate(inputData.ClassName, inputData.PropertyNames);

            var joined = Environment.NewLine + string.Join(Environment.NewLine, code);

            var editPoint = textDocument.CreateEditPoint(selection.BottomPoint);
            editPoint.Insert(joined);
        }

    }
}
