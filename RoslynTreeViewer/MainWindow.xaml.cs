using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoslynTreeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string fileName = @"..\..\MainWindow.xaml.cs";

            var tree = SyntaxTree.ParseFile(fileName);

            CompilationUnitSyntax compilationUnit = (CompilationUnitSyntax)tree.GetRoot();
            AddNodeToTree(compilationUnit, treeControl);
        }

        private void AddNodeToTree(SyntaxNode codeNode, ItemsControl parent)
        {
            var newNode = new TreeViewItem();

            newNode.Header = codeNode.GetType().Name;
            newNode.ToolTip = codeNode.ToFullString();

            parent.Items.Add(newNode);

            foreach (var childCodeName in codeNode.ChildNodes())
            {
                AddNodeToTree(childCodeName, newNode);
            }
        }
    }
}